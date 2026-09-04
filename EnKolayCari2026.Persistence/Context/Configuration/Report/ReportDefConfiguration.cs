using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Report;

namespace EnKolayCari2026.Persistence.Context.Configuration.Report
{
    public partial class ReportDefConfiguration : IEntityTypeConfiguration<ReportDef>
    {
        public void Configure(EntityTypeBuilder<ReportDef> entity)
        {
            entity.ToTable("ReportDef", "report");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("ReportDef_pk");

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

            entity.Property(e => e.ReportType)
                .IsRequired(true)
                .HasMaxLength(30)
                .HasColumnName("ReportType");

            entity.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnName("Description");

            entity.Property(e => e.BinData)
                .IsRequired(true)
                .HasColumnName("BinData");

            entity.Property(e => e.InsertDateTime)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("InsertDateTime");

            entity.Property(e => e.InsertUser)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("InsertUser");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(false)
                .HasColumnName("UpdateDateTime");

            entity.Property(e => e.UpdateUser)
                .IsRequired(false)
                .HasColumnName("UpdateUser");

            entity.Property(e => e.DeleteDateTime)
                .IsRequired(false)
                .HasColumnName("DeleteDateTime");

            entity.Property(e => e.DeleteUser)
                .IsRequired(false)
                .HasColumnName("DeleteUser");

            entity.Property(e => e.RecordDateTime)
                .IsRequired(false)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("RecordDateTime");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.ReportDef)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_ReportDef_CompanyId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ReportDef> entity);
    }
}
