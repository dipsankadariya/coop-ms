using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bms.Models;

public partial class BmsDbContext : DbContext
{
    public BmsDbContext()
    {
    }

    public BmsDbContext(DbContextOptions<BmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberShare> MemberShares { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=bms;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5A6150E3749");

            entity.Property(e => e.AccountType).HasMaxLength(50);
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Member).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Accounts_Members");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loans__4F5AD4575FD823C3");

            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LoanType).HasMaxLength(50);
            entity.Property(e => e.PrincipalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Member).WithMany(p => p.Loans)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Loans_Members");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B181D71BA11");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<MemberShare>(entity =>
        {
            entity.HasKey(e => e.ShareId).HasName("PK__MemberSh__D32A3FEE4B16AEF5");

            entity.ToTable("MemberShare");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ContributionDate).HasColumnType("datetime");
            entity.Property(e => e.ShareType).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.MemberShares)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_MemberShare_Members");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6BD0F1E398");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Accounts");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CC9CB7A04");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4AB82A892").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348F21BED7").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
