using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ITransactionRepository
    {
        public IEnumerable<Transaction> GetTransactions();

        public void AddTransaction(List<Transaction> transaction);

        public IEnumerable<Transaction> GetTransactionsByStatus(string status1, string status2);

        public IEnumerable<Transaction> GetTransactionsByCurrency(string currency);

        public IEnumerable<Transaction> GetTransactionsByDateRange(DateTime startedDate, DateTime endedDate);
    }
}
