using Microsoft.EntityFrameworkCore;
using cwiczenia6.Entities;

namespace cwiczenia6.Data;

public class HospitalContext : DbContext
{
    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<Bed> Beds { get; set; }

    public virtual DbSet<BedAssignment> BedAssignments { get; set; }

    public virtual DbSet<BedType> BedTypes { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admission>(entity =>
        {
            entity.Property(e => e.AdmissionDate).HasColumnType("datetime");
            entity.Property(e => e.DischargeDate).HasColumnType("datetime");

            entity.HasOne(d => d.PatientPeselNavigation).WithMany(p => p.Admissions)
                .HasForeignKey(d => d.PatientPesel)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Ward).WithMany(p => p.Admissions)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Bed>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.BedType).WithMany(p => p.Beds)
                .HasForeignKey(d => d.BedTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Room).WithMany(p => p.Beds)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<BedAssignment>(entity =>
        {
            entity.Property(e => e.From).HasColumnType("datetime");
            entity.Property(e => e.To).HasColumnType("datetime");

            entity.HasOne(d => d.Bed).WithMany(p => p.BedAssignments)
                .HasForeignKey(d => d.BedId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PatientPeselNavigation).WithMany(p => p.BedAssignments)
                .HasForeignKey(d => d.PatientPesel)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Pesel);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasOne(d => d.Ward).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
