using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Application
{
    public interface ITransactionLogService
    {
        public void AddTransactionLog(TransactionLog transactionlog);
    }
}
