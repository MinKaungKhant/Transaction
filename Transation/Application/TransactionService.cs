using System;
using System.Collections.Generic;
using Core;

namespace Application
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public void AddTransaction(List<Transaction> transactions)
        {
             _transactionRepository.AddTransaction(transactions);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactionRepository.GetTransactions();
        }

        public IEnumerable<Transaction> GetTransactionsByCurrency(string currency)
        {
           return _transactionRepository.GetTransactionsByCurrency(currency);
        }

        public IEnumerable<Transaction> GetTransactionsByDateRange(string startedDate, string endedDate)
        {
            DateTime started = Convert.ToDateTime(startedDate);
            DateTime ended = Convert.ToDateTime(endedDate);
            return _transactionRepository.GetTransactionsByDateRange(started, ended);
        }

        public IEnumerable<Transaction> GetTransactionsByStatus(string status)
        {
            if (status.Trim() == "Approved" || status.Trim() == "A")
            {
                return _transactionRepository.GetTransactionsByStatus("Approved","Approved");
            }
            else if(status.Trim() == "Rejected" || status.Trim() == "Failed" || status.Trim() == "R")
            {
                return _transactionRepository.GetTransactionsByStatus("Rejected", "Failed");
            }
            else if(status.Trim() == "Finished" || status.Trim() == "Done" || status.Trim() == "D")
            {
                return _transactionRepository.GetTransactionsByStatus("Finished","Done");
            }
            else
            {
                return _transactionRepository.GetTransactionsByStatus(status, status);
            }
            
        }
    }
}
