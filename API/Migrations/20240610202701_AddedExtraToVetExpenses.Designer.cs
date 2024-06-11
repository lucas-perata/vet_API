﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240610202701_AddedExtraToVetExpenses")]
    partial class AddedExtraToVetExpenses
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.Adoption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("Area")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeworm")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Province")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int>("StatusList")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Adoption", (string)null);
                });

            modelBuilder.Entity("API.Entities.AdoptionPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AdoptionId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdoptionId");

                    b.ToTable("Adoption-Photos");
                });

            modelBuilder.Entity("API.Entities.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("API.Entities.AppUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("API.Entities.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Motive")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<int?>("PetId")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("VetId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PetId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("VetId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("API.Entities.Connection", b =>
                {
                    b.Property<string>("ConnectionId")
                        .HasColumnType("text");

                    b.Property<string>("GroupName")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("ConnectionId");

                    b.HasIndex("GroupName");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("API.Entities.ExpensesVet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Extra")
                        .HasColumnType("boolean");

                    b.Property<string>("VetId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("VetId");

                    b.ToTable("ExpensesVet");
                });

            modelBuilder.Entity("API.Entities.Group", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("API.Entities.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("API.Entities.Identity.Owner", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("API.Entities.Identity.Vet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vets");
                });

            modelBuilder.Entity("API.Entities.Identity.VetService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsOffered")
                        .HasColumnType("boolean");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<string>("VetId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("VetId", "ServiceId")
                        .IsUnique();

                    b.ToTable("VetServices");
                });

            modelBuilder.Entity("API.Entities.MedicalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdoptionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("PetId")
                        .HasColumnType("integer");

                    b.Property<string>("Treatment")
                        .HasColumnType("text");

                    b.Property<string>("VetName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdoptionId");

                    b.HasIndex("PetId");

                    b.ToTable("MedicalHistory", (string)null);
                });

            modelBuilder.Entity("API.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateRead")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("MessageSent")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("RecipientDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("RecipientId")
                        .HasColumnType("text");

                    b.Property<string>("RecipientUsername")
                        .HasColumnType("text");

                    b.Property<bool>("SenderDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("SenderId")
                        .HasColumnType("text");

                    b.Property<string>("SenderUsername")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("API.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Breed")
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("ForAdoption")
                        .HasColumnType("boolean");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Pet", (string)null);
                });

            modelBuilder.Entity("API.Entities.PetPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<int>("PetId")
                        .HasColumnType("integer");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("Pet-Photos");
                });

            modelBuilder.Entity("API.Entities.PetVaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PetId")
                        .HasColumnType("integer");

                    b.Property<int>("VaccineId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.HasIndex("VaccineId");

                    b.ToTable("PetVaccines");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<int>("Stars")
                        .HasColumnType("integer");

                    b.Property<string>("VetId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("VetId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("API.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Service", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description 1",
                            Duration = 60,
                            Name = "Service 1",
                            Price = 100f
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description 2",
                            Duration = 120,
                            Name = "Service 2",
                            Price = 200f
                        },
                        new
                        {
                            Id = 3,
                            Description = "Description 3",
                            Duration = 180,
                            Name = "Service 3",
                            Price = 300f
                        });
                });

            modelBuilder.Entity("API.Entities.Spending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("OwnerId")
                        .HasColumnType("text");

                    b.Property<int?>("PetId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PetId");

                    b.ToTable("Spendings");
                });

            modelBuilder.Entity("API.Entities.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Required")
                        .HasColumnType("boolean");

                    b.Property<string>("SideEffects")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("API.Entities.Adoption", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("API.Entities.AdoptionPhoto", b =>
                {
                    b.HasOne("API.Entities.Adoption", "Adoption")
                        .WithMany("AdoptionPhotos")
                        .HasForeignKey("AdoptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adoption");
                });

            modelBuilder.Entity("API.Entities.AppUserRole", b =>
                {
                    b.HasOne("API.Entities.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.Appointment", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "Owner")
                        .WithMany("OwnerAppointments")
                        .HasForeignKey("OwnerId");

                    b.HasOne("API.Entities.Pet", "Pet")
                        .WithMany("Appointments")
                        .HasForeignKey("PetId");

                    b.HasOne("API.Entities.Service", "Service")
                        .WithMany("Appointments")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Identity.AppUser", "Vet")
                        .WithMany("VetAppointments")
                        .HasForeignKey("VetId");

                    b.Navigation("Owner");

                    b.Navigation("Pet");

                    b.Navigation("Service");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("API.Entities.Connection", b =>
                {
                    b.HasOne("API.Entities.Group", null)
                        .WithMany("Connections")
                        .HasForeignKey("GroupName");
                });

            modelBuilder.Entity("API.Entities.ExpensesVet", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "Vet")
                        .WithMany("ExpensesVet")
                        .HasForeignKey("VetId");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("API.Entities.Identity.Owner", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.Identity.Vet", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.Identity.VetService", b =>
                {
                    b.HasOne("API.Entities.Service", "Service")
                        .WithMany("VetServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Identity.AppUser", "Vet")
                        .WithMany("VetServices")
                        .HasForeignKey("VetId");

                    b.Navigation("Service");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("API.Entities.MedicalHistory", b =>
                {
                    b.HasOne("API.Entities.Adoption", null)
                        .WithMany("MedicalHistories")
                        .HasForeignKey("AdoptionId");

                    b.HasOne("API.Entities.Pet", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("API.Entities.Message", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API.Entities.Identity.AppUser", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("API.Entities.Pet", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("API.Entities.PetPhoto", b =>
                {
                    b.HasOne("API.Entities.Pet", "Pet")
                        .WithMany("PetPhotos")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("API.Entities.PetVaccine", b =>
                {
                    b.HasOne("API.Entities.Pet", "Pet")
                        .WithMany("PetVaccines")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Vaccine", "Vaccine")
                        .WithMany("PetVaccines")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("Vaccine");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "AppUser")
                        .WithMany("Photos")
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("API.Entities.Review", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "Owner")
                        .WithMany("OwnerReviews")
                        .HasForeignKey("OwnerId");

                    b.HasOne("API.Entities.Identity.AppUser", "Vet")
                        .WithMany("VetReviews")
                        .HasForeignKey("VetId");

                    b.Navigation("Owner");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("API.Entities.Spending", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", "Owner")
                        .WithMany("Spendings")
                        .HasForeignKey("OwnerId");

                    b.HasOne("API.Entities.Pet", "Pet")
                        .WithMany("Spendings")
                        .HasForeignKey("PetId");

                    b.Navigation("Owner");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("API.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("API.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Entities.Adoption", b =>
                {
                    b.Navigation("AdoptionPhotos");

                    b.Navigation("MedicalHistories");
                });

            modelBuilder.Entity("API.Entities.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("API.Entities.Group", b =>
                {
                    b.Navigation("Connections");
                });

            modelBuilder.Entity("API.Entities.Identity.AppUser", b =>
                {
                    b.Navigation("ExpensesVet");

                    b.Navigation("MessagesReceived");

                    b.Navigation("MessagesSent");

                    b.Navigation("OwnerAppointments");

                    b.Navigation("OwnerReviews");

                    b.Navigation("Pets");

                    b.Navigation("Photos");

                    b.Navigation("Spendings");

                    b.Navigation("UserRoles");

                    b.Navigation("VetAppointments");

                    b.Navigation("VetReviews");

                    b.Navigation("VetServices");
                });

            modelBuilder.Entity("API.Entities.Pet", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PetPhotos");

                    b.Navigation("PetVaccines");

                    b.Navigation("Spendings");
                });

            modelBuilder.Entity("API.Entities.Service", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("VetServices");
                });

            modelBuilder.Entity("API.Entities.Vaccine", b =>
                {
                    b.Navigation("PetVaccines");
                });
#pragma warning restore 612, 618
        }
    }
}
