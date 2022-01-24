using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Infrastructure.Configrations
{
    public  class TransationConfigration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.TransactionId);
            builder.Property(e => e.TransactionId).HasMaxLength(50).IsRequired();

            builder.Property(e => e.Amount).IsRequired();

            builder.Property(e => e.CurrencyCode).HasMaxLength(3).IsRequired();
            builder.Property(e => e.TransactionDate).IsRequired();
            builder.Property(e => e.Status).IsRequired();
        }
    }
}
