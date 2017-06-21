using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SummerHouseApplication.Data;
using SummerHouseApplication.Models.Map;

namespace SummerHouseApplication.Data.Migrations
{
    [DbContext(typeof(SummerHouseDbContext))]
    [Migration("20170621202828_SharedSummerHouseFix")]
    partial class SharedSummerHouseFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.Map.FishingNet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SummerHouseId");

                    b.HasKey("Id");

                    b.HasIndex("SummerHouseId");

                    b.ToTable("FishingNets");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.Map.InfoWindow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentString");

                    b.HasKey("Id");

                    b.ToTable("InfoWindows");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.Map.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.Map.MapMarker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CoordinatesId");

                    b.Property<int>("FishType");

                    b.Property<int?>("FishingNetId");

                    b.Property<int>("InfoId");

                    b.Property<int>("MarkerType");

                    b.Property<int>("SummerHouseId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CoordinatesId");

                    b.HasIndex("FishingNetId");

                    b.HasIndex("InfoId");

                    b.HasIndex("SummerHouseId");

                    b.ToTable("Markers");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.SharedSummerHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SummerHouseId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SummerHouseId");

                    b.HasIndex("UserId");

                    b.ToTable("SharedSummerHouses");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.SummerHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<DateTime>("Created");

                    b.Property<bool>("HasBeach");

                    b.Property<bool>("HasElectricity");

                    b.Property<bool>("HasRunningWater");

                    b.Property<bool>("HasSauna");

                    b.Property<int?>("LocationOnMapId");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerId");

                    b.Property<string>("PostalCode");

                    b.HasKey("Id");

                    b.HasIndex("LocationOnMapId");

                    b.HasIndex("OwnerId");

                    b.ToTable("SummerHouses");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.SummerHouseUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SummerHouseApplication.Models.SummerHouseUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SummerHouseApplication.Models.SummerHouseUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SummerHouseApplication.Models.SummerHouseUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SummerHouseApplication.Models.Map.FishingNet", b =>
                {
                    b.HasOne("SummerHouseApplication.Models.SummerHouse", "SummerHouse")
                        .WithMany("FishingNets")
                        .HasForeignKey("SummerHouseId");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.Map.MapMarker", b =>
                {
                    b.HasOne("SummerHouseApplication.Models.Map.Location", "Coordinates")
                        .WithMany()
                        .HasForeignKey("CoordinatesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SummerHouseApplication.Models.Map.FishingNet", "FishingNet")
                        .WithMany("Markers")
                        .HasForeignKey("FishingNetId");

                    b.HasOne("SummerHouseApplication.Models.Map.InfoWindow", "Info")
                        .WithMany()
                        .HasForeignKey("InfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SummerHouseApplication.Models.SummerHouse", "SummerHouse")
                        .WithMany("FishMarkers")
                        .HasForeignKey("SummerHouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SummerHouseApplication.Models.SharedSummerHouse", b =>
                {
                    b.HasOne("SummerHouseApplication.Models.SummerHouse", "SummerHouse")
                        .WithMany("AuthorizedUsers")
                        .HasForeignKey("SummerHouseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SummerHouseApplication.Models.SummerHouseUser", "User")
                        .WithMany("SharedSummerHouses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SummerHouseApplication.Models.SummerHouse", b =>
                {
                    b.HasOne("SummerHouseApplication.Models.Map.Location", "LocationOnMap")
                        .WithMany()
                        .HasForeignKey("LocationOnMapId");

                    b.HasOne("SummerHouseApplication.Models.SummerHouseUser", "Owner")
                        .WithMany("SummerHouses")
                        .HasForeignKey("OwnerId");
                });
        }
    }
}
