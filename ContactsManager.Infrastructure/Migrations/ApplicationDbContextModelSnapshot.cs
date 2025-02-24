﻿// <auto-generated />
using System;
using ContactsManager.Infrastructure.MyDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactsManager.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactsManager.Core.Domain.IdentityEntities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("ContactsManager.Core.Domain.IdentityEntities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<string>("PersonName")
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

            modelBuilder.Entity("ContactsManager.Core.Entities.Country", b =>
                {
                    b.Property<Guid>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            CountryId = new Guid("e7b3c506-8c8b-4b9a-bbf3-1a2e2e4e5f12"),
                            CountryName = "France"
                        },
                        new
                        {
                            CountryId = new Guid("d234f91e-6b8a-45e9-bf13-89a41e2d5d78"),
                            CountryName = "Germany"
                        },
                        new
                        {
                            CountryId = new Guid("a9c4d7f3-1c8e-42b5-92f0-82e1e3c9e1f7"),
                            CountryName = "United States"
                        },
                        new
                        {
                            CountryId = new Guid("c5a1e2f4-9d2a-41c7-ae45-34f2b4e5c6d9"),
                            CountryName = "Canada"
                        },
                        new
                        {
                            CountryId = new Guid("b1e9f5d3-4c7a-4821-983a-91f2c3e1d5a6"),
                            CountryName = "Japan"
                        });
                });

            modelBuilder.Entity("ContactsManager.Core.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("04 rue su vieux marché aux vins 67 000 strasbourg");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Persons", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("dda22ac9-9692-4bf5-8813-bce5d29b8c2d"),
                            CountryId = new Guid("e7b3c506-8c8b-4b9a-bbf3-1a2e2e4e5f12"),
                            DateOfBirth = new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alice.dupont@example.com",
                            Name = "Alice Dupont",
                            PhoneNumber = "+33123456789"
                        },
                        new
                        {
                            Id = new Guid("fcc71eb9-bb72-4b3d-ab6f-de12b8fa1212"),
                            CountryId = new Guid("e7b3c506-8c8b-4b9a-bbf3-1a2e2e4e5f12"),
                            DateOfBirth = new DateTime(1985, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jean.martin@example.com",
                            Name = "Jean Martin",
                            PhoneNumber = "+33698765432"
                        },
                        new
                        {
                            Id = new Guid("682cada4-b283-40a7-92a8-b49643c24a4b"),
                            CountryId = new Guid("e7b3c506-8c8b-4b9a-bbf3-1a2e2e4e5f12"),
                            DateOfBirth = new DateTime(1995, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "maria.gonzalez@example.com",
                            Name = "Maria Gonzalez",
                            PhoneNumber = "+34612345678"
                        },
                        new
                        {
                            Id = new Guid("7743f1c4-678a-4e8d-a21f-b283be167a89"),
                            CountryId = new Guid("d234f91e-6b8a-45e9-bf13-89a41e2d5d78"),
                            DateOfBirth = new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.doe@example.com",
                            Name = "John Doe",
                            PhoneNumber = "+15551234567"
                        },
                        new
                        {
                            Id = new Guid("2bb7c2e8-3966-4401-8b6a-37169ac85bb9"),
                            CountryId = new Guid("d234f91e-6b8a-45e9-bf13-89a41e2d5d78"),
                            DateOfBirth = new DateTime(1998, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "emma.brown@example.com",
                            Name = "Emma Brown",
                            PhoneNumber = "+447712345678"
                        },
                        new
                        {
                            Id = new Guid("c3ff80f7-d876-46b5-87f1-82fdb12433e6"),
                            CountryId = new Guid("a9c4d7f3-1c8e-42b5-92f0-82e1e3c9e1f7"),
                            DateOfBirth = new DateTime(1987, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "robert.wilson@example.com",
                            Name = "Robert Wilson",
                            PhoneNumber = "+613987654321"
                        },
                        new
                        {
                            Id = new Guid("2f959b24-88db-44c8-bfae-36d9dc051035"),
                            CountryId = new Guid("a9c4d7f3-1c8e-42b5-92f0-82e1e3c9e1f7"),
                            DateOfBirth = new DateTime(1993, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "sophie.laurent@example.com",
                            Name = "Sophie Laurent",
                            PhoneNumber = "+33123459876"
                        },
                        new
                        {
                            Id = new Guid("0a473cdb-9f51-48b6-8aa4-ba2de0c112b7"),
                            CountryId = new Guid("c5a1e2f4-9d2a-41c7-ae45-34f2b4e5c6d9"),
                            DateOfBirth = new DateTime(1991, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "liam.johnson@example.com",
                            Name = "Liam Johnson",
                            PhoneNumber = "+12125556789"
                        },
                        new
                        {
                            Id = new Guid("7b9be6a7-36dd-49d7-ada2-8424204ff085"),
                            CountryId = new Guid("c5a1e2f4-9d2a-41c7-ae45-34f2b4e5c6d9"),
                            DateOfBirth = new DateTime(1996, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "isabella.rossi@example.com",
                            Name = "Isabella Rossi",
                            PhoneNumber = "+393391234567"
                        },
                        new
                        {
                            Id = new Guid("585a0625-45ef-45f4-ae37-33b17d9a7641"),
                            CountryId = new Guid("b1e9f5d3-4c7a-4821-983a-91f2c3e1d5a6"),
                            DateOfBirth = new DateTime(1984, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "david.schmidt@example.com",
                            Name = "David Schmidt",
                            PhoneNumber = "+4917623456789"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ContactsManager.Core.Entities.Person", b =>
                {
                    b.HasOne("ContactsManager.Core.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ContactsManager.Core.Domain.IdentityEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("ContactsManager.Core.Domain.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("ContactsManager.Core.Domain.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ContactsManager.Core.Domain.IdentityEntities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ContactsManager.Core.Domain.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("ContactsManager.Core.Domain.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
