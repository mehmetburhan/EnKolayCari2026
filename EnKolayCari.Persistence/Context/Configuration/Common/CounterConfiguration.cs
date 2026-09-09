using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class CounterConfiguration : IEntityTypeConfiguration<Counter>
    {
        public void Configure(EntityTypeBuilder<Counter> entity)
        {
            entity.ToTable("Counter", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Counter_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.CompanyId)
                .IsRequired(true)
                .HasColumnName("CompanyId");

            entity.Property(e => e.Type)
                .IsRequired(false)
                .HasMaxLength(15)
                .HasColumnName("Type");

            entity.Property(e => e.SerialCode)
                .IsRequired(false)
                .HasMaxLength(10)
                .HasColumnName("SerialCode");

            entity.Property(e => e.StartNumber)
                .IsRequired(true)
                .HasColumnName("StartNumber");

            entity.Property(e => e.EndNumber)
                .IsRequired(true)
                .HasColumnName("EndNumber");

            entity.Property(e => e.NextNumber)
                .IsRequired(true)
                .HasColumnName("NextNumber");

            entity.Property(e => e.InsertUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            entity.Property(e => e.RecordDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.Counter)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_Counter_CompanyId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Counter> entity);
    }
}
