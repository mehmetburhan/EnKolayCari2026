using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Inventory;

namespace EnKolayCari.Persistence.Context.Configuration.Inventory
{
    public partial class StorePersonalConfiguration : IEntityTypeConfiguration<StorePersonal>
    {
        public void Configure(EntityTypeBuilder<StorePersonal> entity)
        {
            entity.ToTable("StorePersonal", "inventory");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("StorePersonal_pk");

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

            entity.Property(e => e.StoreId)
                .IsRequired(true)
                .HasColumnName("StoreId");

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.StorePersonal)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_StorePersonal_CompanyId");

            entity.HasOne(d => d.Store)
                  .WithMany(p => p.StorePersonal)
                  .HasForeignKey(d => d.StoreId)
                  .HasConstraintName("FK_StorePersonal_StoreId");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.StorePersonal)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("FK_StorePersonal_PersonalId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StorePersonal> entity);
    }
}
