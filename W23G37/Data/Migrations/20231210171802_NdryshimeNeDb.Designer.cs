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
    [Migration("20231210171802_NdryshimeNeDb")]
    partial class NdryshimeNeDb
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

            modelBuilder.Entity("W23G37.Models.AfatiParaqitjesSemestrit", b =>
                {
                    b.Property<int>("APSID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("APSID"), 1L, 1);

                    b.Property<DateTime?>("DataFillimitAfatit")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataMbarimitAfatit")
                        .HasColumnType("datetime2");

                    b.Property<string>("LlojiSemestrit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NiveliStudimeveID")
                        .HasColumnType("int");

                    b.Property<string>("VitiAkademik")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("APSID");

                    b.HasIndex("NiveliStudimeveID");

                    b.ToTable("AfatiParaqitjesSemestrit");
                });

            modelBuilder.Entity("W23G37.Models.AplikimetEReja", b =>
                {
                    b.Property<int>("AplikimiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AplikimiID"), 1L, 1);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataLindjes")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataRegjistrimit")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartamentiID")
                        .HasColumnType("int");

                    b.Property<string>("EmailPersonal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmriPrindit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gjinia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KodiFinanciar")
                        .HasColumnType("int");

                    b.Property<string>("LlojiRegjistrimit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mbiemri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NiveliStudimitID")
                        .HasColumnType("int");

                    b.Property<string>("NrKontaktit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NrPersonal")
                        .HasColumnType("int");

                    b.Property<string>("Qyteti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Shteti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VitiAkademikRegjistrim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZipKodi")
                        .HasColumnType("int");

                    b.HasKey("AplikimiID");

                    b.HasIndex("DepartamentiID");

                    b.HasIndex("NiveliStudimitID");

                    b.ToTable("AplikimetEReja");
                });

            modelBuilder.Entity("W23G37.Models.Departamentet", b =>
                {
                    b.Property<int>("DepartamentiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartamentiID"), 1L, 1);

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartamentiID")
                        .HasColumnType("int");

                    b.Property<int>("LendaID")
                        .HasColumnType("int");

                    b.Property<string>("Pozita")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LokacioniID");

                    b.ToTable("Lokacionet");
                });

            modelBuilder.Entity("W23G37.Models.LokacioniDepartamenti", b =>
                {
                    b.Property<int>("LokacioniDepartamentiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LokacioniDepartamentiID"), 1L, 1);

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartamentiID")
                        .HasColumnType("int");

                    b.Property<int?>("LokacioniID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("LokacioniDepartamentiID");

                    b.HasIndex("DepartamentiID")
                        .IsUnique()
                        .HasFilter("[DepartamentiID] IS NOT NULL");

                    b.HasIndex("LokacioniID");

                    b.ToTable("LokacionetDepartamenti");
                });

            modelBuilder.Entity("W23G37.Models.NiveliStudimeve", b =>
                {
                    b.Property<int>("NiveliStudimeveID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NiveliStudimeveID"), 1L, 1);

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmriNivelitStudimeve")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShkurtesaEmritNivelitStudimeve")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NiveliStudimeveID");

                    b.ToTable("NiveliStudimeve");
                });

            modelBuilder.Entity("W23G37.Models.NiveliStudimitDepartamenti", b =>
                {
                    b.Property<int>("NiveliStudimitDepartamentiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NiveliStudimitDepartamentiID"), 1L, 1);

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartamentiID")
                        .HasColumnType("int");

                    b.Property<int?>("NiveliStudimitID")
                        .HasColumnType("int");

                    b.HasKey("NiveliStudimitDepartamentiID");

                    b.HasIndex("DepartamentiID");

                    b.HasIndex("NiveliStudimitID");

                    b.ToTable("NiveliStudimitDepartamenti");
                });

            modelBuilder.Entity("W23G37.Models.ParaqitjaSemestrit", b =>
                {
                    b.Property<int>("ParaqitjaSemestritID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParaqitjaSemestritID"), 1L, 1);

                    b.Property<int?>("APSID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataParaqitjes")
                        .HasColumnType("datetime2");

                    b.Property<string>("NderrimiOrarit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SemestriID")
                        .HasColumnType("int");

                    b.Property<int?>("StudentiID")
                        .HasColumnType("int");

                    b.HasKey("ParaqitjaSemestritID");

                    b.HasIndex("APSID");

                    b.HasIndex("SemestriID");

                    b.HasIndex("StudentiID");

                    b.ToTable("PataqitjaSemestrit");
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

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("W23G37.Models.Semestri", b =>
                {
                    b.Property<int>("SemestriID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SemestriID"), 1L, 1);

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NiveliStudimeveID")
                        .HasColumnType("int");

                    b.Property<int?>("NrSemestrit")
                        .HasColumnType("int");

                    b.HasKey("SemestriID");

                    b.HasIndex("NiveliStudimeveID");

                    b.ToTable("Semestri");
                });

            modelBuilder.Entity("W23G37.Models.SpecializimetPerDepartament", b =>
                {
                    b.Property<int>("SpecializimiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecializimiID"), 1L, 1);

                    b.Property<DateTime?>("DataKrijimt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartamentiID")
                        .HasColumnType("int");

                    b.Property<string>("EmriSpecializimit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecializimiID");

                    b.HasIndex("DepartamentiID");

                    b.ToTable("SpecializimetPerDepartament");
                });

            modelBuilder.Entity("W23G37.Models.TeDhenatPerdoruesit", b =>
                {
                    b.Property<int>("TeDhenatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeDhenatID"), 1L, 1);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataKrijimit")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataLindjes")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailPersonal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmriPrindit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gjinia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NrKontaktit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NrPersonal")
                        .HasColumnType("int");

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

            modelBuilder.Entity("W23G37.Models.TeDhenatRegjistrimitStudentit", b =>
                {
                    b.Property<int>("TDhRSID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TDhRSID"), 1L, 1);

                    b.Property<DateTime?>("DataRegjistrimit")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartamentiID")
                        .HasColumnType("int");

                    b.Property<int?>("KodiFinanciar")
                        .HasColumnType("int");

                    b.Property<string>("LlojiRegjistrimit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NiveliStudimitID")
                        .HasColumnType("int");

                    b.Property<int?>("SpecializimiID")
                        .HasColumnType("int");

                    b.Property<string>("VitiAkademikRegjistrim")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TDhRSID");

                    b.HasIndex("DepartamentiID");

                    b.HasIndex("NiveliStudimitID");

                    b.HasIndex("SpecializimiID");

                    b.ToTable("TeDhenatRegjistrimitStudentit");
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

            modelBuilder.Entity("W23G37.Models.AfatiParaqitjesSemestrit", b =>
                {
                    b.HasOne("W23G37.Models.NiveliStudimeve", "NiveliStudimeve")
                        .WithMany()
                        .HasForeignKey("NiveliStudimeveID");

                    b.Navigation("NiveliStudimeve");
                });

            modelBuilder.Entity("W23G37.Models.AplikimetEReja", b =>
                {
                    b.HasOne("W23G37.Models.Departamentet", "Departamentet")
                        .WithMany()
                        .HasForeignKey("DepartamentiID");

                    b.HasOne("W23G37.Models.NiveliStudimeve", "NiveliStudimeve")
                        .WithMany()
                        .HasForeignKey("NiveliStudimitID");

                    b.Navigation("Departamentet");

                    b.Navigation("NiveliStudimeve");
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

            modelBuilder.Entity("W23G37.Models.LokacioniDepartamenti", b =>
                {
                    b.HasOne("W23G37.Models.Departamentet", "Departamentet")
                        .WithOne("LokacionetDepartamenti")
                        .HasForeignKey("W23G37.Models.LokacioniDepartamenti", "DepartamentiID");

                    b.HasOne("W23G37.Models.Lokacionet", "Lokacioni")
                        .WithMany()
                        .HasForeignKey("LokacioniID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamentet");

                    b.Navigation("Lokacioni");
                });

            modelBuilder.Entity("W23G37.Models.NiveliStudimitDepartamenti", b =>
                {
                    b.HasOne("W23G37.Models.Departamentet", "Departamentet")
                        .WithMany()
                        .HasForeignKey("DepartamentiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("W23G37.Models.NiveliStudimeve", "NiveliStudimeve")
                        .WithMany()
                        .HasForeignKey("NiveliStudimitID");

                    b.Navigation("Departamentet");

                    b.Navigation("NiveliStudimeve");
                });

            modelBuilder.Entity("W23G37.Models.ParaqitjaSemestrit", b =>
                {
                    b.HasOne("W23G37.Models.AfatiParaqitjesSemestrit", "AfatiParaqitjesSemestrit")
                        .WithMany()
                        .HasForeignKey("APSID");

                    b.HasOne("W23G37.Models.Semestri", "Semestri")
                        .WithMany()
                        .HasForeignKey("SemestriID");

                    b.HasOne("W23G37.Models.Perdoruesi", "Studenti")
                        .WithMany()
                        .HasForeignKey("StudentiID");

                    b.Navigation("AfatiParaqitjesSemestrit");

                    b.Navigation("Semestri");

                    b.Navigation("Studenti");
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

            modelBuilder.Entity("W23G37.Models.Semestri", b =>
                {
                    b.HasOne("W23G37.Models.NiveliStudimeve", "NiveliStudimeve")
                        .WithMany()
                        .HasForeignKey("NiveliStudimeveID");

                    b.Navigation("NiveliStudimeve");
                });

            modelBuilder.Entity("W23G37.Models.SpecializimetPerDepartament", b =>
                {
                    b.HasOne("W23G37.Models.Departamentet", "Departamenti")
                        .WithMany()
                        .HasForeignKey("DepartamentiID");

                    b.Navigation("Departamenti");
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

            modelBuilder.Entity("W23G37.Models.TeDhenatRegjistrimitStudentit", b =>
                {
                    b.HasOne("W23G37.Models.Departamentet", "Departamentet")
                        .WithMany()
                        .HasForeignKey("DepartamentiID");

                    b.HasOne("W23G37.Models.NiveliStudimeve", "NiveliStudimeve")
                        .WithMany()
                        .HasForeignKey("NiveliStudimitID");

                    b.HasOne("W23G37.Models.SpecializimetPerDepartament", "Specializimi")
                        .WithMany()
                        .HasForeignKey("SpecializimiID");

                    b.Navigation("Departamentet");

                    b.Navigation("NiveliStudimeve");

                    b.Navigation("Specializimi");
                });

            modelBuilder.Entity("W23G37.Models.Departamentet", b =>
                {
                    b.Navigation("LokacionetDepartamenti");
                });

            modelBuilder.Entity("W23G37.Models.Perdoruesi", b =>
                {
                    b.Navigation("TeDhenatPerdoruesit");
                });
#pragma warning restore 612, 618
        }
    }
}
