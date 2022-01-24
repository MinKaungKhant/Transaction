using Application;
using Core;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml;
using System;

namespace WebMvc.Controllers
{
    public class UploadController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionLogService _transactionLogService;

        public UploadController(ITransactionService transactionService, ITransactionLogService transactionLogService)
        {
            _transactionService = transactionService;
            _transactionLogService = transactionLogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                @TempData["error"] = "Please choose CSV or XML File to upload.";
                return View();
            }
            else
            {
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(fileName).Substring(1).ToLower();
                if (fileExtension == "csv" || fileExtension == "xml")
                {
                    if (file.Length <= 1000000)
                    {
                        if (fileExtension == "csv")
                        {
                           var result = CsvRead(file);
                            @TempData["error"] = result;
                            TransactionLog(fileName,result);
                            return result == "Successfully Imported." ?  View() : View();
                        }
                        else
                        {
                            var result = XmlRead(file);
                            @TempData["error"] = result;
                            TransactionLog(fileName, result);
                            return result == "Successfully Imported." ? View() : View();
                        }
                    }
                    else
                    {
                        @TempData["error"] = "File Size is over 1 MB. Please rechoose another file.";
                        return View(file);
                    }
                    //return View(file);
                }
                else
                {
                    @TempData["error"] = "Unkown Format. Please choose CSV or XML File to upload.";
                    return View(file);
                }
            }
        }

        private void TransactionLog(string filename, string result)
        {
            TransactionLog transactionLog = new TransactionLog
            {
                filename = filename,
                process = result == "Successfully Imported." ? "Approved" : "Failed",
                time = DateTime.Now
            };
            _transactionLogService.AddTransactionLog(transactionLog);
        }

        private  String CsvRead(IFormFile file)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
            try
            {
                using (var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                     file.CopyToAsync(fileStream);
                }

                using(var streamReader = new StreamReader(Path.Combine(path, filename)))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var dbTransaction = _transactionService.GetTransactions();
                        
                        List<Transaction> transactions = new List<Transaction>();
                        List<Transaction> addtransactions = new List<Transaction>();
                        String errorMsg = "";
                        while (csvReader.Read())
                        {
                            Transaction transaction = new Transaction { 
                                TransactionId = csvReader.GetField(0).Replace("“", "").Replace("”", "").Replace("\"", "").Trim(),
                                Amount = Convert.ToDecimal(csvReader.GetField(1).Replace("“", "").Replace("”", "").Replace("\"", "").Trim()),
                                CurrencyCode = csvReader.GetField(2).Replace("“", "").Replace("”", "").Replace("\"", "").Trim(),
                                TransactionDate = DateTime.Parse(csvReader.GetField(3).Replace("“", "").Replace("”", "").Replace("\"", "").Trim()),
                                Status = csvReader.GetField(4).Replace("“", "").Replace("”", "").Replace("\"", "").Trim()
                            };

                            var checkid = dbTransaction.Where(x => x.TransactionId == transaction.TransactionId).ToList();
                            if (checkid.Count != 0)
                            {
                                errorMsg += ($"{transaction.TransactionId} already exists.\n");
                            }
                            transactions.Add(transaction);
                        }
                        if (!String.IsNullOrWhiteSpace(errorMsg))
                        {
                            return errorMsg;
                        }
                        addtransactions.AddRange(transactions);
                        _transactionService.AddTransaction(addtransactions);
                        return "Successfully Imported.";
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            finally
            {
                System.IO.File.Delete(Path.Combine(path, filename));
            }
        }

        private String XmlRead(IFormFile file)
        {
            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
            try
            {
                using (var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }

                var dbTransaction = _transactionService.GetTransactions();

                List<Transaction> transactions = new List<Transaction>();
                List<Transaction> addtransactions = new List<Transaction>();
                String errorMsg = "";

                using (var xmlReader = new XmlTextReader(Path.Combine(path, filename)))
                {
                    xmlReader.ReadToFollowing("Transaction");

                    do
                    {
                        xmlReader.MoveToFirstAttribute();
                        string id = xmlReader.Value;
                        xmlReader.ReadToFollowing("TransactionDate");
                        DateTime date = Convert.ToDateTime(xmlReader.ReadElementContentAsString().Replace("T", " "));

                        xmlReader.ReadToFollowing("Amount");
                        decimal amount = Convert.ToDecimal(xmlReader.ReadElementContentAsString());

                        xmlReader.ReadToFollowing("CurrencyCode");
                        string currency = xmlReader.ReadElementContentAsString();

                        xmlReader.ReadToFollowing("Status");
                        string status = xmlReader.ReadElementContentAsString();

                        Transaction transaction = new Transaction
                        {
                            TransactionId = id,
                            Amount = amount,
                            CurrencyCode = currency,
                            TransactionDate = date,
                            Status = status
                        };
                        var checkid = dbTransaction.Where(x => x.TransactionId == transaction.TransactionId).ToList();
                        if (checkid.Count != 0)
                        {
                            errorMsg += ($"{transaction.TransactionId} already exists.\n");
                        }
                        transactions.Add(transaction);
                    } while (xmlReader.ReadToFollowing("Transaction"));

                    if (!String.IsNullOrWhiteSpace(errorMsg))
                    {
                        return errorMsg;
                    }
                    addtransactions.AddRange(transactions);
                    _transactionService.AddTransaction(addtransactions);
                    return "Successfully Imported.";
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            finally
            {
               System.IO.File.Delete(Path.Combine(path, filename));
            }
        }
    }
}
