using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.DataAccess.Layer.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Association> Associations { get; set; } = null!;
        public virtual DbSet<Crop> Crops { get; set; } = null!;
        public virtual DbSet<Cropregistry> Cropregistries { get; set; } = null!;
        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Farmer> Farmers { get; set; } = null!;
        public virtual DbSet<Farmtype> Farmtypes { get; set; } = null!;
        public virtual DbSet<Lga> Lgas { get; set; } = null!;
        public virtual DbSet<Livestock> Livestocks { get; set; } = null!;
        public virtual DbSet<Livestockregistry> Livestockregistries { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Notificationtarget> Notificationtargets { get; set; } = null!;
        public virtual DbSet<Profileactivity> Profileactivities { get; set; } = null!;
        public virtual DbSet<Profileactivityparent> Profileactivityparents { get; set; } = null!;
        public virtual DbSet<Profileadditionalactivity> Profileadditionalactivities { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<Synclog> Synclogs { get; set; } = null!;
        public virtual DbSet<Useraccount> Useraccounts { get; set; } = null!;
        public virtual DbSet<Userprofile> Userprofiles { get; set; } = null!;
        public virtual DbSet<Userregion> Userregions { get; set; } = null!;
        public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
        public virtual DbSet<AgroAlliedRegistry> AgroAlliedRegistries { get; set; } = null!;
        public virtual DbSet<BusinessType> BusinessTypes { get; set; } = null!;
        public virtual DbSet<PrimaryProduct> PrimaryProducts { get; set; } = null!;
        public virtual DbSet<DashboardMetrics> DashboardMetrics { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=OyoAgro;Username=postgres;Password=Admin");
                //optionsBuilder.UseNpgsql("Host=turntable.proxy.rlwy.net; Port=34939; Database=oyoagrodb; Username=postgres; Password=GsinVqwnnTiyadmTWlHipqxtTjrmsZQF");
                optionsBuilder.UseNpgsql("Host=ballast.proxy.rlwy.net;Port=57826;Database=oyoagrodb;Username=postgres;Password=TyRqhAJGkeKpIZljrCJGSCWcFKZZrvZu");
            }   
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("operation_type", new[] { "INSERT", "UPDATE", "DELETE" })
                .HasPostgresEnum("target_scope", new[] { "ALL", "REGION", "LGA", "USER" })
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");

                entity.HasIndex(e => e.Tempclientid, "addresses_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Addressid).HasColumnName("addressid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Farmerid).HasColumnName("farmerid");

                entity.Property(e => e.Farmid).HasColumnName("farmid");

                entity.Property(e => e.Latitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("latitude");

                entity.Property(e => e.Lgaid).HasColumnName("lgaid");

                entity.Property(e => e.Longitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("longitude");

                entity.Property(e => e.Postalcode)
                    .HasMaxLength(20)
                    .HasColumnName("postalcode");

                entity.Property(e => e.Streetaddress)
                    .HasMaxLength(255)
                    .HasColumnName("streetaddress");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Town)
                    .HasMaxLength(100)
                    .HasColumnName("town");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Lga)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.Lgaid)
                    .HasConstraintName("addresses_lgaid_fkey");
            });

            modelBuilder.Entity<Association>(entity =>
            {
                entity.ToTable("association");

                entity.HasIndex(e => e.Tempclientid, "association_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Associationid).HasColumnName("associationid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Registrationno)
                    .HasMaxLength(100)
                    .HasColumnName("registrationno");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");
            });


            modelBuilder.Entity<BusinessType>(entity =>
            {
                entity.HasKey(e => e.BusinessTypeId)
                    .HasName("businesstype_pkey");

                entity.ToTable("BusinessType");
                entity.Property(e => e.Updatedat)
                    .HasColumnName("Updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.BusinessTypeId).HasColumnName("BusinessTypeId");
                entity.Property(e => e.Createdat)
                    .HasColumnName("Createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("Deletedat");


                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("Name");
            });

             modelBuilder.Entity<PrimaryProduct>(entity =>
            {
                entity.HasKey(e => e.PrimaryProductTypeId)
                    .HasName("primaryproduct_pkey");

                entity.ToTable("PrimaryProduct");
                entity.Property(e => e.Updatedat)
                    .HasColumnName("Updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.PrimaryProductTypeId).HasColumnName("PrimaryProductTypeId");
                entity.Property(e => e.Createdat)
                    .HasColumnName("Createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("Deletedat");


                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("Name");
            });



            modelBuilder.Entity<Crop>(entity =>
            {
                entity.HasKey(e => e.Croptypeid)
                    .HasName("crop_pkey");

                entity.ToTable("crop");
                entity.Property(e => e.Updatedat)
                    .HasColumnName("Updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Croptypeid).HasColumnName("croptypeid");
                entity.Property(e => e.Createdat)
                    .HasColumnName("Createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("Deletedat");


                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Cropregistry>(entity =>
            {
                entity.ToTable("cropregistry");

                entity.HasIndex(e => e.Tempclientid, "cropregistry_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Cropregistryid).HasColumnName("cropregistryid");

                entity.Property(e => e.Areaharvested).HasColumnName("areaharvested");

                entity.Property(e => e.Areaplanted).HasColumnName("areaplanted");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Croptypeid).HasColumnName("croptypeid");

                entity.Property(e => e.Cropvariety)
                    .HasMaxLength(100)
                    .HasColumnName("cropvariety");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Farmid).HasColumnName("farmid");

                entity.Property(e => e.Harvestdate).HasColumnName("harvestdate");

                entity.Property(e => e.Plantedquantity).HasColumnName("plantedquantity");

                entity.Property(e => e.Plantingdate).HasColumnName("plantingdate");

                entity.Property(e => e.Seasonid).HasColumnName("seasonid");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Yieldquantity).HasColumnName("yieldquantity");

                entity.HasOne(d => d.Croptype)
                    .WithMany(p => p.Cropregistries)
                    .HasForeignKey(d => d.Croptypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cropregistry_croptypeid_fkey");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Cropregistries)
                    .HasForeignKey(d => d.Farmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cropregistry_farmid_fkey");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Cropregistries)
                    .HasForeignKey(d => d.Seasonid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cropregistry_seasonid_fkey");
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("farm");

                entity.HasIndex(e => e.Tempclientid, "farm_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Farmid).HasColumnName("farmid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Farmerid).HasColumnName("farmerid");

                entity.Property(e => e.Farmsize).HasColumnName("farmsize");

                entity.Property(e => e.Farmtypeid).HasColumnName("farmtypeid");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.Farms)
                    .HasForeignKey(d => d.Farmerid)
                    .HasConstraintName("farm_farmerid_fkey");

                entity.HasOne(d => d.Farmtype)
                    .WithMany(p => p.Farms)
                    .HasForeignKey(d => d.Farmtypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("farm_farmtypeid_fkey");
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.ToTable("farmer");

                entity.HasIndex(e => e.Tempclientid, "farmer_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Farmerid).HasColumnName("farmerid");
                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.Property(e => e.Associationid).HasColumnName("associationid");

                entity.Property(e => e.Availablelabor).HasColumnName("availablelabor");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .HasColumnName("gender");

                entity.Property(e => e.Householdsize).HasColumnName("householdsize");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(100)
                    .HasColumnName("lastname");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(100)
                    .HasColumnName("middlename");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(20)
                    .HasColumnName("phonenumber");

                entity.Property(e => e.Photourl).HasColumnName("photourl");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Association)
                    .WithMany(p => p.Farmers)
                    .HasForeignKey(d => d.Associationid)
                    .HasConstraintName("farmer_associationid_fkey");
            });

            modelBuilder.Entity<Farmtype>(entity =>
            {
                entity.ToTable("farmtype");

                entity.Property(e => e.Farmtypeid).HasColumnName("farmtypeid");

                entity.Property(e => e.Typename)
                    .HasMaxLength(50)
                    .HasColumnName("typename");
                entity.Property(e => e.Updatedat)
                 .HasColumnName("Updatedat")
                 .HasDefaultValueSql("now()");
                entity.Property(e => e.Createdat)
                 .HasColumnName("Createdat")
                 .HasDefaultValueSql("now()");
                entity.Property(e => e.Deletedat).HasColumnName("Deletedat");


            });

            modelBuilder.Entity<Lga>(entity =>
            {
                entity.ToTable("lga");

                entity.HasIndex(e => e.Tempclientid, "lga_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Lgaid).HasColumnName("lgaid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Lganame)
                    .HasMaxLength(100)
                    .HasColumnName("lganame");

                entity.Property(e => e.Regionid).HasColumnName("regionid");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Lgas)
                    .HasForeignKey(d => d.Regionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lga_regionid_fkey");
            });

            modelBuilder.Entity<Livestock>(entity =>
            {
                entity.HasKey(e => e.Livestocktypeid)
                    .HasName("livestock_pkey");

                entity.ToTable("livestock");

                entity.Property(e => e.Livestocktypeid).HasColumnName("livestocktypeid");
                entity.Property(e => e.Updatedat)
              .HasColumnName("Updatedat")
              .HasDefaultValueSql("now()");
                entity.Property(e => e.Createdat)
                 .HasColumnName("Createdat")
                 .HasDefaultValueSql("now()");
                entity.Property(e => e.Deletedat).HasColumnName("Deletedat");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Livestockregistry>(entity =>
            {
                entity.ToTable("livestockregistry");

                entity.HasIndex(e => e.Tempclientid, "livestockregistry_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Livestockregistryid).HasColumnName("livestockregistryid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Enddate).HasColumnName("enddate");

                entity.Property(e => e.Farmid).HasColumnName("farmid");

                entity.Property(e => e.Livestocktypeid).HasColumnName("livestocktypeid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Seasonid).HasColumnName("seasonid");

                entity.Property(e => e.Startdate).HasColumnName("startdate");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Livestockregistries)
                    .HasForeignKey(d => d.Farmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("livestockregistry_farmid_fkey");

                entity.HasOne(d => d.Livestocktype)
                    .WithMany(p => p.Livestockregistries)
                    .HasForeignKey(d => d.Livestocktypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("livestockregistry_livestocktypeid_fkey");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Livestockregistries)
                    .HasForeignKey(d => d.Seasonid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("livestockregistry_seasonid_fkey");
            });

            modelBuilder.Entity<AgroAlliedRegistry>(entity =>
            {
                entity.ToTable("AgroAlliedRegistry");

                entity.HasIndex(e => e.Tempclientid, "livestockregistry_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.AgroAlliedRegistryid).HasColumnName("AgroAlliedRegistryid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("Createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("Deletedat");
                entity.Property(e => e.Farmid).HasColumnName("Farmid");
                entity.Property(e => e.BusinessTypeId).HasColumnName("BusinessTypeId");
                entity.Property(e => e.PrimaryProductTypeId).HasColumnName("PrimaryProductTypeId");
                entity.Property(e => e.Seasonid).HasColumnName("Seasonid");
                entity.Property(e => e.ProductionCapacity).HasColumnName("ProductionCapacity");
                entity.Property(e => e.Tempclientid).HasColumnName("Tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("Updatedat")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.AgroAlliedRegistries)
                    .HasForeignKey(d => d.Farmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgroAlliedRegistry_farmid_fkey");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.AgroAlliedRegistries)
                    .HasForeignKey(d => d.Seasonid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgroAlliedRegistry_seasonid_fkey");

                entity.HasOne(d => d.BusinessType)
                    .WithMany(p => p.AgroAlliedRegistries)
                    .HasForeignKey(d => d.BusinessTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgroAlliedRegistry_businesstypeid_fkey");

                entity.HasOne(d => d.PrimaryProduct)
                    .WithMany(p => p.AgroAlliedRegistries)
                    .HasForeignKey(d => d.PrimaryProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgroAlliedRegistry_primaryproducttypeId_fkey");
            });



            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.HasIndex(e => e.Tempclientid, "notification_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Notificationid).HasColumnName("notificationid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Createdby).HasColumnName("createdby");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Isread)
                    .HasColumnName("isread")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.CreatedbyNavigation)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.Createdby)
                    .HasConstraintName("notification_createdby_fkey");
            });

            modelBuilder.Entity<Notificationtarget>(entity =>
            {
                entity.HasKey(e => e.Targetid)
                    .HasName("notificationtarget_pkey");

                entity.ToTable("notificationtarget");

                entity.Property(e => e.Targetid).HasColumnName("targetid");

                entity.Property(e => e.Lgaid).HasColumnName("lgaid");

                entity.Property(e => e.Notificationid).HasColumnName("notificationid");

                entity.Property(e => e.Regionid).HasColumnName("regionid");

                entity.Property(e => e.Userid).HasColumnName("userid");
                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");
                entity.Property(e => e.Createdat).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Deletedat).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Lga)
                    .WithMany(p => p.Notificationtargets)
                    .HasForeignKey(d => d.Lgaid)
                    .HasConstraintName("notificationtarget_lgaid_fkey");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.Notificationtargets)
                    .HasForeignKey(d => d.Notificationid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("notificationtarget_notificationid_fkey");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Notificationtargets)
                    .HasForeignKey(d => d.Regionid)
                    .HasConstraintName("notificationtarget_regionid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notificationtargets)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("notificationtarget_userid_fkey");
            });

            modelBuilder.Entity<Profileactivity>(entity =>
            {
                entity.HasKey(e => e.Activityid)
                    .HasName("profileactivity_pkey");

                entity.ToTable("profileactivity");

                entity.Property(e => e.Activityid).HasColumnName("activityid");

                entity.Property(e => e.Activityname).HasColumnName("activityname");

                entity.Property(e => e.Activityparentid).HasColumnName("activityparentid");

                entity.Property(e => e.Createdat).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Deletedat).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Updatedat).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Activityparent)
                    .WithMany(p => p.Profileactivities)
                    .HasForeignKey(d => d.Activityparentid)
                    .HasConstraintName("profileactivity_activityparentid_fkey");
            });

            modelBuilder.Entity<Profileactivityparent>(entity =>
            {
                entity.HasKey(e => e.Activityparentid)
                    .HasName("profileactivityparent_pkey");

                entity.ToTable("profileactivityparent");

                entity.HasIndex(e => e.Activityparentid, "idx_activity_parent");

                entity.Property(e => e.Activityparentid).HasColumnName("activityparentid");

                entity.Property(e => e.Activityparentname).HasColumnName("activityparentname");

                entity.Property(e => e.Createdat)
                    .HasColumnName("Createdat")
                    .HasDefaultValueSql("now()");

               
                entity.Property(e => e.Updatedat)
                    .HasColumnName("Updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat)
                    .HasColumnName("Deletedat")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Profileadditionalactivity>(entity =>
            {
                entity.HasKey(e => e.Additionalactivityid)
                    .HasName("profileadditionalactivity_pkey");

                entity.ToTable("profileadditionalactivity");

                entity.HasIndex(e => e.Activityid, "idx_additionalactivity_activity");

                entity.HasIndex(e => e.Userid, "idx_additionalactivity_user");

                entity.Property(e => e.Additionalactivityid).HasColumnName("additionalactivityid");

                entity.Property(e => e.Activityid).HasColumnName("activityid");

                entity.Property(e => e.Canadd).HasColumnName("canadd");

                entity.Property(e => e.Canapprove).HasColumnName("canapprove");

                entity.Property(e => e.Candelete).HasColumnName("candelete");

                entity.Property(e => e.Canedit).HasColumnName("canedit");

                entity.Property(e => e.Canview).HasColumnName("canview");

                entity.Property(e => e.Deletedat).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Expireon).HasColumnName("expireon");

                entity.Property(e => e.Updatedat).HasColumnType("timestamp without time zone");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Profileadditionalactivities)
                    .HasForeignKey(d => d.Activityid)
                    .HasConstraintName("profileadditionalactivity_activityid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Profileadditionalactivities)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("profileadditionalactivity_userid_fkey");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");

                entity.HasIndex(e => e.Tempclientid, "region_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Regionid).HasColumnName("regionid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Regionname)
                    .HasMaxLength(100)
                    .HasColumnName("regionname");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasIndex(e => e.Rolename, "role_rolename_key")
                    .IsUnique();

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .HasColumnName("rolename");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("season");

                entity.HasIndex(e => e.Tempclientid, "season_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Seasonid).HasColumnName("seasonid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Enddate).HasColumnName("enddate");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Startdate).HasColumnName("startdate");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Synclog>(entity =>
            {
                entity.ToTable("synclog");

                entity.Property(e => e.Synclogid).HasColumnName("synclogid");

                entity.Property(e => e.Changedat)
                    .HasColumnName("changedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Clientid)
                    .HasMaxLength(100)
                    .HasColumnName("clientid");

                entity.Property(e => e.Processed)
                    .HasColumnName("processed")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Serverid).HasColumnName("serverid");

                entity.Property(e => e.Tablename)
                    .HasMaxLength(100)
                    .HasColumnName("tablename");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");
            });

            modelBuilder.Entity<Useraccount>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("useraccount_pkey");

                entity.ToTable("useraccount");

                entity.HasIndex(e => e.Tempclientid, "useraccount_tempclientid_key")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "useraccount_username_key")
                    .IsUnique();

                entity.Property(e => e.Userid).HasColumnName("userid");
                entity.Property(e => e.Apitoken).HasColumnName("apitoken");

                entity.Property(e => e.PasswordResetToken).HasColumnName("PasswordResetToken");
              

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");
                entity.Property(e => e.PasswordResetTokenExpires)
                  .HasColumnName("PasswordResetTokenExpires");

                entity.Property(e => e.LastPasswordReset)
                  .HasColumnName("LastPasswordReset");

                entity.Property(e => e.Deactivateddate).HasColumnName("deactivateddate");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Failedloginattempt).HasColumnName("failedloginattempt");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Islocked).HasColumnName("islocked");

                entity.Property(e => e.Lastlogindate).HasColumnName("lastlogindate");

                entity.Property(e => e.Logincount).HasColumnName("logincount");

                entity.Property(e => e.Password).HasMaxLength(10);

                entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");

                entity.Property(e => e.Salt).HasColumnName("salt");

                entity.Property(e => e.Securityanswer).HasColumnName("securityanswer");

                entity.Property(e => e.Securityquestion).HasColumnName("securityquestion");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Userprofile>(entity =>
            {
                entity.ToTable("userprofile");

                entity.HasIndex(e => e.Tempclientid, "userprofile_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Userprofileid).HasColumnName("userprofileid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Designation)
                    .HasMaxLength(100)
                    .HasColumnName("designation");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(100)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .HasColumnName("gender");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(100)
                    .HasColumnName("lastname");

                entity.Property(e => e.Lgaid).HasColumnName("lgaid");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(100)
                    .HasColumnName("middlename");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(20)
                    .HasColumnName("phonenumber");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userprofiles)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("userprofile_userid_fkey");
            });

            modelBuilder.Entity<Userregion>(entity =>
            {
                entity.ToTable("userregion");

                entity.HasIndex(e => e.Tempclientid, "userregion_tempclientid_key")
                    .IsUnique();

                entity.Property(e => e.Userregionid).HasColumnName("userregionid");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.Property(e => e.Regionid).HasColumnName("regionid");

                entity.Property(e => e.Tempclientid).HasColumnName("tempclientid");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Userregions)
                    .HasForeignKey(d => d.Regionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userregion_regionid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userregions)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userregion_userid_fkey");
            });

            //modelBuilder.Entity<DashboardMetrics>(entity =>
            //{
            //    entity.ToTable("dashboardmetrics");

            //    entity.HasKey(e => e.DashboardMetricsId)
            //        .HasName("dashboardmetrics_pkey");

            //    entity.Property(e => e.DashboardMetricsId).HasColumnName("dashboardmetricsid");
            //    entity.Property(e => e.EntityType).HasColumnName("entitytype").HasMaxLength(50);
            //    entity.Property(e => e.EntityId).HasColumnName("entityid");

            //    entity.Property(e => e.FarmerCount).HasColumnName("farmercount");
            //    entity.Property(e => e.FarmCount).HasColumnName("farmcount");
            //    entity.Property(e => e.CropRegistryCount).HasColumnName("cropregistrycount");
            //    entity.Property(e => e.LivestockRegistryCount).HasColumnName("livestockregistrycount");
            //    entity.Property(e => e.TotalAreaPlanted).HasColumnName("totalareaplanted");
            //    entity.Property(e => e.TotalAreaHarvested).HasColumnName("totalareaharvested");
            //    entity.Property(e => e.TotalYieldQuantity).HasColumnName("totalyieldquantity");
            //    entity.Property(e => e.TotalPlantedQuantity).HasColumnName("totalplantedquantity");
            //    entity.Property(e => e.TotalLivestockQuantity).HasColumnName("totallivestockquantity");
            //    entity.Property(e => e.LastCalculated).HasColumnName("lastcalculated");
            //    entity.Property(e => e.LastUpdated).HasColumnName("lastupdated");
            //    entity.Property(e => e.Notes).HasColumnName("notes");

            //    entity.Property(e => e.Createdat)
            //        .HasColumnName("createdat")
            //        .HasDefaultValueSql("now()");

            //    entity.Property(e => e.Updatedat)
            //        .HasColumnName("updatedat")
            //        .HasDefaultValueSql("now()");

            //    entity.Property(e => e.Deletedat).HasColumnName("deletedat");

            //    // Optional foreign key relationships
            //    entity.HasOne(d => d.User)
            //        .WithMany()
            //        .HasForeignKey(d => d.EntityId)
            //        .HasPrincipalKey(d => d.Userid)
            //        .HasConstraintName("dashboardmetrics_userid_fkey")
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .IsRequired(false);

            //    entity.HasOne(d => d.Farmer)
            //        .WithMany()
            //        .HasForeignKey(d => d.EntityId)
            //        .HasPrincipalKey(d => d.Farmerid)
            //        .HasConstraintName("dashboardmetrics_farmerid_fkey")
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .IsRequired(false);

            //    entity.HasOne(d => d.Farm)
            //        .WithMany()
            //        .HasForeignKey(d => d.EntityId)
            //        .HasPrincipalKey(d => d.Farmid)
            //        .HasConstraintName("dashboardmetrics_farmid_fkey")
            //        .OnDelete(DeleteBehavior.Cascade)
            //        .IsRequired(false);

            //    // Index for efficient queries
            //    entity.HasIndex(e => new { e.EntityType, e.EntityId })
            //        .HasDatabaseName("idx_dashboardmetrics_entity");
            //});

            modelBuilder.Entity<DashboardMetrics>(entity =>
            {
                entity.ToTable("dashboardmetrics");
                entity.HasKey(e => e._MetricId);
                entity.Property(e => e._MetricId).HasColumnName("Metric_Id");
                entity.Property(e => e._MetricName).HasColumnName("Metric_Name").HasConversion<int>();
                entity.Property(e => e._MetricValue).HasColumnName("Metric_Value");
                //entity.Property(e => e._MetricMonthlyPeriod).HasColumnName("Metric_MonthlyPeriod");
                //entity.Property(e => e._MetricWeeklyPeriod).HasColumnName("Metric_WeeklyPeriod");
                //entity.Property(e => e._MetricDailyPeriod).HasColumnName("Metric_DailyPeriod");
                entity.Property(e => e._MetricCreateDate).HasColumnName("Metric_CreateDate").HasDefaultValueSql("now()");
                entity.Property(e => e._MetricUpdatedDate).HasColumnName("Metric_UpdatedDate").HasDefaultValueSql("now()");
                entity.Property(e => e._SysNumber).HasColumnName("SysNumber");
                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserId");
                entity.Property(e => e.RelatedFarmerId).HasColumnName("RelatedFarmerId");
                entity.Property(e => e.RelatedFarmId).HasColumnName("RelatedFarmId");
                entity.Property(e => e.EntityId).HasColumnName("EntityId");
                entity.Property(e => e.Createdat).HasColumnName("createdat").HasDefaultValueSql("now()");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat").HasDefaultValueSql("now()");
                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("fk_dashboardmetrics_user");

                entity.HasOne(d => d.RelatedFarmer)
                    .WithMany()
                    .HasForeignKey(d => d.RelatedFarmerId)
                    .HasConstraintName("fk_dashboardmetrics_farmer");

                entity.HasOne(d => d.RelatedFarm)
                    .WithMany()
                    .HasForeignKey(d => d.RelatedFarmId)
                    .HasConstraintName("fk_dashboardmetrics_farm");

                // Indexes for performance
                entity.HasIndex(e => e._MetricName)
                    .HasDatabaseName("idx_dashboardmetrics_name");
                entity.HasIndex(e => e.CreatedByUserId)
                    .HasDatabaseName("idx_dashboardmetrics_user");
                entity.HasIndex(e => e.RelatedFarmerId)
                    .HasDatabaseName("idx_dashboardmetrics_farmer");
                entity.HasIndex(e => e.RelatedFarmId)
                    .HasDatabaseName("idx_dashboardmetrics_farm");
                //entity.HasIndex(e => e._MetricDailyPeriod)
                //    .HasDatabaseName("idx_dashboardmetrics_daily");
                //entity.HasIndex(e => e._MetricWeeklyPeriod)
                //    .HasDatabaseName("idx_dashboardmetrics_weekly");
                //entity.HasIndex(e => e._MetricMonthlyPeriod)
                //    .HasDatabaseName("idx_dashboardmetrics_monthly");
                entity.HasIndex(e => e._MetricCreateDate)
                    .HasDatabaseName("idx_dashboardmetrics_createdate");
            });

            modelBuilder.Entity<PasswordResetToken>(entity =>
            {
                entity.ToTable("passwordresettokens");

                entity.HasKey(e => e.Id)
                    .HasName("passwordresettokens_pkey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.Property(e => e.ExpiresAt)
                    .IsRequired()
                    .HasColumnName("expiresat");

                entity.Property(e => e.IsUsed)
                    .IsRequired()
                    .HasDefaultValue(false)
                    .HasColumnName("isused");

                entity.Property(e => e.UsedAt).HasColumnName("usedat");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(45)
                    .HasColumnName("ipaddress");

                entity.Property(e => e.UserAgent)
                    .HasMaxLength(500)
                    .HasColumnName("useragent");

                entity.Property(e => e.Createdat)
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Updatedat)
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deletedat).HasColumnName("deletedat");

                // Foreign key relationship
                entity.HasOne(d => d.User)
                    .WithMany(p => p.PasswordResetTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_passwordresettokens_userid");

                // Indexes for performance
                entity.HasIndex(e => e.Token)
                    .HasDatabaseName("idx_passwordresettokens_token");

                entity.HasIndex(e => e.UserId)
                    .HasDatabaseName("idx_passwordresettokens_userid");

                entity.HasIndex(e => e.ExpiresAt)
                    .HasDatabaseName("idx_passwordresettokens_expiresat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
