using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class FileBlobConfiguration : IEntityTypeConfiguration<FileBlob>
    {
        public void Configure(EntityTypeBuilder<FileBlob> entity)
        {
            entity.ToTable("FileBlob", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("FileBlob_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.FileHeaderId)
                .IsRequired(true)
                .HasColumnName("FileHeaderId");

            entity.Property(e => e.Blob)
                .IsRequired(true)
                .HasColumnName("Blob");

            //Foreign Key
            entity.HasOne(d => d.FileHeader)
                  .WithMany(p => p.FileBlob)
                  .HasForeignKey(d => d.FileHeaderId)
                  .HasConstraintName("FileBlob_fk");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FileBlob> entity);
    }
}
