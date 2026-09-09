using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.Common;

namespace EnKolayCari.Persistence.Context.Configuration.Common
{
    public partial class FileHeaderConfiguration : IEntityTypeConfiguration<FileHeader>
    {
        public void Configure(EntityTypeBuilder<FileHeader> entity)
        {
            entity.ToTable("FileHeader", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("FileHeader_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.TableName)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasColumnName("TableName");

            entity.Property(e => e.TableId)
                .IsRequired(false)
                .HasColumnName("TableId");

            entity.Property(e => e.FileName)
                .IsRequired(false)
                .HasMaxLength(500)
                .HasColumnName("FileName");

            entity.Property(e => e.FileOrder)
                .IsRequired(true)
                .HasColumnName("FileOrder");

            entity.Property(e => e.FileType)
                .IsRequired(false)
                .HasColumnName("FileType");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FileHeader> entity);
    }
}
