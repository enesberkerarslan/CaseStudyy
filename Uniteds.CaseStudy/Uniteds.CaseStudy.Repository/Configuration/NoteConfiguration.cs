using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uniteds.CaseStudy.Domain.Models;

namespace Uniteds.CaseStudy.Repository.Configuration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title).HasMaxLength(100).IsRequired();
            builder.Property(n => n.Description).HasMaxLength(500);
            builder.Property(n => n.Content).IsRequired();

            builder.HasOne(n => n.ParentNote)
                .WithMany(n => n.Children)
                .HasForeignKey(n => n.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
