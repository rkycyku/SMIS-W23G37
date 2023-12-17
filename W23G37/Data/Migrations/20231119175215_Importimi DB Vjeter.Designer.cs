﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using W23G37.Data;

#nullable disable

namespace W23G37.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231119175215_Importimi DB Vjeter")]
    partial class ImportimiDBVjeter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("W23G37.Models.Departamentet", b =>
                {
                    b.Property<int>("DepartamentiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartamentiID"), 1L, 1);

                    b.Property<string>("EmriDepartamentit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShkurtesaDepartamentit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartamentiID");

                    b.ToTable("Departamentet");
                });

            modelBuilder.Entity("W23G37.Models.Lendet", b =>
                {
                    b.Property<int>("LendaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LendaID"), 1L, 1);

                    b.Property<string>("EmriLendes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KategoriaLendes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KodiLendes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KreditELendes")
                        .HasColumnType("int");

                    b.Property<int?>("SemestriLigjerimit")
                        .HasColumnType("int");

                    b.Property<string>("ShkurtesaLendes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LendaID");

                    b.ToTable("Lendet");
                });

            modelBuilder.Entity("W23G37.Models.LendetDepartamentiProfesori", b =>
                {
                    b.Property<int>("LDPID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LDPID"), 1L, 1);

                    b.Property<int?>("DepartamentiID")
                        .HasColumnType("int");

                    b.Property<int>("LendaID")
                        .HasColumnType("int");

                    b.Property<int?>("ProfesoriID")
                        .HasColumnType("int");

                    b.HasKey("LDPID");

                    b.HasIndex("DepartamentiID");

                    b.HasIndex("LendaID");

                    b.HasIndex("ProfesoriID");

                    b.ToTable("LendetDepartamentiProfesori");
                });

            modelBuilder.Entity("W23G37.Models.LlogaritERejaTeKrijuara", b =>
                {
                    b.Property<int>("LlogariaEReID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LlogariaEReID"), 1L, 1);

                    b.Property<string>("AspNetUserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PerdoruesiID")
                        .HasColumnType("int");

                    b.HasKey("LlogariaEReID");

                    b.HasIndex("AspNetUserID");

                    b.HasIndex("PerdoruesiID");

                    b.ToTable("LlogaritERejaTeKrijuara");
                });

            modelBuilder.Entity("W23G37.Models.Lokacionet", b =>
                {
                    b.Property<int>("LokacioniID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LokacioniID"), 1L, 1);

                    b.Property<string>("AdresaLokacionit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmriLokacionit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QytetiLokacionit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShkurtesaLokacionit")
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("LokacioniID");

                    b.ToTable("Lokacionet");
                });

            modelBuilder.Entity("W23G37.Models.Perdoruesi", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("AspNetUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mbiemri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("AspNetUserId");

                    b.ToTable("Perdoruesit");
                });

            modelBuilder.Entity("W23G37.Models.Sallat", b =>
                {
                    b.Property<int>("SallaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SallaID"), 1L, 1);

                    b.Property<int>("KapacitetiSalles")
                        .HasColumnType("int");

                    b.Property<string>("KodiSalles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LokacioniID")
                        .HasColumnType("int");

                    b.HasKey("SallaID");

                    b.HasIndex("LokacioniID");

                    b.ToTable("Sallat");
                });

            modelBuilder.Entity("W23G37.Models.TeDhenatPerdoruesit", b =>
                {
                    b.Property<int>("TeDhenatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeDhenatID"), 1L, 1);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NrKontaktit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qyteti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Shteti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int?>("ZipKodi")
                        .HasColumnType("int");

                    b.HasKey("TeDhenatID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("TeDhenatPerdoruesit");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("W23G37.Models.LendetDepartamentiProfesori", b =>
                {
                    b.HasOne("W23G37.Models.Departamentet", "Departamentet")
                        .WithMany()
                        .HasForeignKey("DepartamentiID");

                    b.HasOne("W23G37.Models.Lendet", "Lendet")
                        .WithMany()
                        .HasForeignKey("LendaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("W23G37.Models.Perdoruesi", "Profesori")
                        .WithMany()
                        .HasForeignKey("ProfesoriID");

                    b.Navigation("Departamentet");

                    b.Navigation("Lendet");

                    b.Navigation("Profesori");
                });

            modelBuilder.Entity("W23G37.Models.LlogaritERejaTeKrijuara", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "AspNetUser")
                        .WithMany()
                        .HasForeignKey("AspNetUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("W23G37.Models.Perdoruesi", "Perdoruesi")
                        .WithMany()
                        .HasForeignKey("PerdoruesiID");

                    b.Navigation("AspNetUser");

                    b.Navigation("Perdoruesi");
                });

            modelBuilder.Entity("W23G37.Models.Perdoruesi", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "AspNetUser")
                        .WithMany()
                        .HasForeignKey("AspNetUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AspNetUser");
                });

            modelBuilder.Entity("W23G37.Models.Sallat", b =>
                {
                    b.HasOne("W23G37.Models.Lokacionet", "Lokacioni")
                        .WithMany()
                        .HasForeignKey("LokacioniID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lokacioni");
                });

            modelBuilder.Entity("W23G37.Models.TeDhenatPerdoruesit", b =>
                {
                    b.HasOne("W23G37.Models.Perdoruesi", "User")
                        .WithOne("TeDhenatPerdoruesit")
                        .HasForeignKey("W23G37.Models.TeDhenatPerdoruesit", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("W23G37.Models.Perdoruesi", b =>
                {
                    b.Navigation("TeDhenatPerdoruesit");
                });
#pragma warning restore 612, 618
        }
    }
}