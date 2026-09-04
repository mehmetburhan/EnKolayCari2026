using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari2026.Domain.Model.Common;

namespace EnKolayCari2026.Persistence.Context.Configuration.Common
{
    public partial class PersonalWidgetConfiguration : IEntityTypeConfiguration<PersonalWidget>
    {
        public void Configure(EntityTypeBuilder<PersonalWidget> entity)
        {
            entity.ToTable("PersonalWidget", "common");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("PersonalWidget_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnName("Id");

            entity.Property(e => e.GId)
                .IsRequired(true)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("GId");

            entity.Property(e => e.Stat)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Stat");

            entity.Property(e => e.PersonalId)
                .IsRequired(true)
                .HasColumnName("PersonalId");

            entity.Property(e => e.WidgetId)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("WidgetId");

            entity.Property(e => e.With)
                .IsRequired(false)
                .HasMaxLength(30)
                .HasDefaultValueSql("((1))")
                .HasColumnName("With");

            entity.Property(e => e.IsVisible)
                .IsRequired(true)
                .HasDefaultValueSql("((1))")
                .HasColumnName("IsVisible");

            entity.Property(e => e.Position)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Position");

            entity.Property(e => e.RowIndex)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("RowIndex");

            entity.Property(e => e.CreatedDate)
                .IsRequired(true)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedDate");

            entity.Property(e => e.CreatedUser)
                .IsRequired(true)
                .HasColumnName("CreatedUser");

            entity.Property(e => e.ModifedDate)
                .IsRequired(false)
                .HasColumnName("ModifedDate");

            entity.Property(e => e.ModifedUser)
                .IsRequired(false)
                .HasColumnName("ModifedUser");

            entity.Property(e => e.DeletedDate)
                .IsRequired(false)
                .HasColumnName("DeletedDate");

            entity.Property(e => e.DeletedUser)
                .IsRequired(false)
                .HasColumnName("DeletedUser");

            entity.Property(e => e.IsDelete)
                .IsRequired(true)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IsDelete");

            //Foreign Key
            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.PersonalWidget)
                  .HasForeignKey(d => d.PersonalId)
                  .HasConstraintName("FK_PersonalWidget_Personal");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PersonalWidget> entity);
    }
}
