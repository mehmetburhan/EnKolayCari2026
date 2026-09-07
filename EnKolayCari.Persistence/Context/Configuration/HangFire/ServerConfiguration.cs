using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnKolayCari.Domain.Model.HangFire;

namespace EnKolayCari.Persistence.Context.Configuration.HangFire
{
    public partial class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> entity)
        {
            entity.ToTable("Server", "HangFire");

            // ✅ Primary Key: Id
            entity.HasKey(e => new { e.Id }).HasName("Server_pk");

            entity.Property(e => e.Id)
                .IsRequired(true)
                .HasMaxLength(200)
                .HasColumnName("Id");

            entity.Property(e => e.Data)
                .IsRequired(false)
                .HasColumnName("Data");

            entity.Property(e => e.LastHeartbeat)
                .IsRequired(true)
                .HasColumnName("LastHeartbeat");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Server> entity);
    }
}
