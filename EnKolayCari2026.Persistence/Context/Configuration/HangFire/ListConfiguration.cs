using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.HangFire;

namespace EnKolayCari2026.Persistence.Context.Configuration.HangFire
{
    public partial class ListConfiguration : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> entity)
        {
            entity.ToTable("List", "HangFire");

            // ✅ Primary Key: Id + Key
            entity.HasKey(e => new { e.Id, e.Key }).HasName("List_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.Key)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Key");

            entity.Property(e => e.Value)
                .IsRequired(false)
                .HasColumnName("Value");

            entity.Property(e => e.ExpireAt)
                .IsRequired(false)
                .HasColumnName("ExpireAt");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<List> entity);
    }
}
