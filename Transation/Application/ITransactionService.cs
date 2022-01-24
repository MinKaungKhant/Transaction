using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Application
{
    public interface ITransactionService
    {
        public IEnumerable<Transaction> GetTransactions();

        public void AddTransaction(List<Transaction> transactions);

        public IEnumerable<Transaction> GetTransactionsByStatus(string currency);

        public IEnumerable<Transaction> GetTransactionsByCurrency(string status);

        public IEnumerable<Transaction> GetTransactionsByDateRange(string startedDate, string endedDate);
    }
}
