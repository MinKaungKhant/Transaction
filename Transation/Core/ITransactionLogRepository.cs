using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ITransactionLogRepository
    {
        public void AddTransactionLog(TransactionLog transactionlog);
    }
}
