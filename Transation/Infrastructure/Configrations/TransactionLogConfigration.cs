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
    public class TransactionLogConfigration : IEntityTypeConfiguration<TransactionLog>
    {
        public void Configure(EntityTypeBuilder<TransactionLog> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(e=>e.id).IsRequired();
            builder.Property(e => e.filename).IsRequired();
            builder.Property(e => e.process).IsRequired();
            builder.Property(e => e.time).IsRequired();
        }
    }
}
