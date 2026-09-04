using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> entity)
        {
            entity.ToTable("Token", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Token_pk");

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

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.ExpreDate)
                .IsRequired(true)
                .HasColumnName("ExpreDate");

            entity.Property(e => e.Application)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasColumnName("Application");

            entity.Property(e => e.UpdateDateTime)
                .IsRequired(true)
                .HasColumnName("UpdateDateTime");

            //Foreign Key
            entity.HasOne(d => d.Company)
                  .WithMany(p => p.Token)
                  .HasForeignKey(d => d.CompanyId)
                  .HasConstraintName("FK_Token_CompanyId");

            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.Token)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("FK_Token_PersonalId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Token> entity);
    }
}
