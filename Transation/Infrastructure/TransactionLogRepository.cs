using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
    public class TransactionLogRepository : ITransactionLogRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionLogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void AddTransactionLog(TransactionLog transactionlog)
        {
            _appDbContext.TransactionLog.Add(transactionlog);
            _appDbContext.SaveChanges();
        }

    }
}
