using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    internal class MedicalInfoConfiguration : IEntityTypeConfiguration<MedicalInfo>
    {
        public void Configure(EntityTypeBuilder<MedicalInfo> builder)
        {

        }
    }
}
