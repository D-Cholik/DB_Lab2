using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Entities;

public partial class Lab2Context : DbContext
{
    public Lab2Context()
    {
    }

    public Lab2Context(DbContextOptions<Lab2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Journey> Journeys { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5433;database=Lab2;user id=postgres;password=pass12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("Clients_pkey");

            entity.Property(e => e.ClientId).HasColumnName("Client_ID");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("Last_Name");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("Flights_pkey");

            entity.Property(e => e.FlightId).HasColumnName("Flight_ID");
            entity.Property(e => e.Departure).HasColumnType("timestamp without time zone");
            entity.Property(e => e.FlightNumber).HasColumnName("Flight_Number");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("Hotels_pkey");

            entity.Property(e => e.HotelId).HasColumnName("Hotel_ID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.HotelName)
                .HasMaxLength(100)
                .HasColumnName("Hotel_Name");
            entity.Property(e => e.JourneyId).HasColumnName("Journey_ID");
            entity.Property(e => e.StarRating).HasColumnName("Star_Rating");

            entity.HasOne(d => d.Journey).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.JourneyId)
                .HasConstraintName("Hotels_Journey_ID_fkey");

            entity.HasMany(d => d.Flights).WithMany(p => p.Hotels)
                .UsingEntity<Dictionary<string, object>>(
                    "HotelFlight",
                    r => r.HasOne<Flight>().WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Hotel-Flights_Flight_ID_fkey"),
                    l => l.HasOne<Hotel>().WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Hotel-Flights_Hotel_ID_fkey"),
                    j =>
                    {
                        j.HasKey("HotelId", "FlightId").HasName("Hotel-Flights_pkey");
                        j.ToTable("Hotel-Flights");
                        j.IndexerProperty<long>("HotelId").HasColumnName("Hotel_ID");
                        j.IndexerProperty<long>("FlightId").HasColumnName("Flight_ID");
                    });
        });

        modelBuilder.Entity<Journey>(entity =>
        {
            entity.HasKey(e => e.JourneyId).HasName("Journeys_pkey");

            entity.Property(e => e.JourneyId).HasColumnName("Journey_ID");
            entity.Property(e => e.ClientId).HasColumnName("Client_ID");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.DepartureDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("Departure_Date");
            entity.Property(e => e.JourneyName)
                .HasMaxLength(100)
                .HasColumnName("Journey_Name");

            entity.HasOne(d => d.Client).WithMany(p => p.Journeys)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Journeys_Client_ID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
