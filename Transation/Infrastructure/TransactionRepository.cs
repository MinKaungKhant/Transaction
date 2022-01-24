using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;    

        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
                 
        }

        public void AddTransaction(List<Transaction> transaction)
        {
            _appDbContext.Transactions.AddRange(transaction);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _appDbContext.Transactions.ToList();
        }

        public IEnumerable<Transaction> GetTransactionsByCurrency(string currency)
        {
            return _appDbContext.Transactions.Where(x => x.CurrencyCode.Trim() == currency.Trim()).ToList();
        }

        public IEnumerable<Transaction> GetTransactionsByDateRange(DateTime startedDate, DateTime endedDate)
        {
            return _appDbContext.Transactions.Where(x => x.TransactionDate >= startedDate && x.TransactionDate <= endedDate).ToList();
        }

        public IEnumerable<Transaction> GetTransactionsByStatus(string status1, string status2)
        {
            return _appDbContext.Transactions.Where(x => x.Status.Trim() == status1.Trim() || x.Status.Trim() == status2.Trim()).ToList();
        }
    }
}
