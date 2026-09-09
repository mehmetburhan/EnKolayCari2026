using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class CounterReferenceConfiguration : IEntityTypeConfiguration<CounterReference>
    {
        public void Configure(EntityTypeBuilder<CounterReference> entity)
        {
            entity.ToTable("CounterReference", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("CounterReference_pk");

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
                .IsRequired(true)
                .HasMaxLength(15)
                .HasColumnName("Type");

            entity.Property(e => e.CounterDefId)
                .IsRequired(true)
                .HasColumnName("CounterDefId");

            entity.Property(e => e.StoreId)
                .IsRequired(true)
                .HasColumnName("StoreId");

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
                  .WithMany(p => p.CounterReference)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_CounterReference_CompanyId");

            entity.HasOne(d => d.Store)
                  .WithMany(p => p.CounterReference)
                  .HasForeignKey(d => d.StoreId)
                  .HasConstraintName("FK_CounterReference_StoreId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CounterReference> entity);
    }
}
