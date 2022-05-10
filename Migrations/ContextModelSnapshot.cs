﻿// <auto-generated />
using System;
using CompanyAgreement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyAgreement.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CompanyAgreement.Models.AdminLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("AdminLogins");
                });

            modelBuilder.Entity("CompanyAgreement.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyInformationId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MeetingDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PublicPrivate")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CompanyInformationId")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CompanyAgreement.Models.CompanyAuthority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ContractDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SGKNO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CompanyAuthorities");
                });

            modelBuilder.Entity("CompanyAgreement.Models.CompanyDepartment", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("DepartmentId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyDepartments");
                });

            modelBuilder.Entity("CompanyAgreement.Models.CompanyInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GSM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.Property<string>("Surname")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("CompanyInformation");
                });

            modelBuilder.Entity("CompanyAgreement.Models.CompanyLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Companyid")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("CompanyLogin");
                });

            modelBuilder.Entity("CompanyAgreement.Models.ContractInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(300)
                        .HasColumnType("Varchar(300)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("District")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.Property<string>("GSM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasMaxLength(20)
                        .HasColumnType("Varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("ContractInformation");
                });

            modelBuilder.Entity("CompanyAgreement.Models.ContractSituation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompanyAuthorityId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("Varchar(50)");

                    b.Property<string>("Situation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyAuthorityId");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("ContractSituation");
                });

            modelBuilder.Entity("CompanyAgreement.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName")
                        .HasMaxLength(50)
                        .HasColumnType("Varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CompanyAgreement.Models.Company", b =>
                {
                    b.HasOne("CompanyAgreement.Models.CompanyInformation", "CompanyInformation")
                        .WithOne("Company")
                        .HasForeignKey("CompanyAgreement.Models.Company", "CompanyInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyInformation");
                });

            modelBuilder.Entity("CompanyAgreement.Models.CompanyDepartment", b =>
                {
                    b.HasOne("CompanyAgreement.Models.Company", "Company")
                        .WithMany("CompanyDepartments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyAgreement.Models.Department", "Department")
                        .WithMany("CompanyDepartments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CompanyAgreement.Models.ContractInformation", b =>
                {
                    b.HasOne("CompanyAgreement.Models.Company", "Company")
                        .WithMany("ContractInformations")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyAgreement.Models.ContractSituation", b =>
                {
                    b.HasOne("CompanyAgreement.Models.CompanyAuthority", "CompanyAuthority")
                        .WithMany()
                        .HasForeignKey("CompanyAuthorityId");

                    b.HasOne("CompanyAgreement.Models.Company", "Company")
                        .WithOne("ContractSituation")
                        .HasForeignKey("CompanyAgreement.Models.ContractSituation", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("CompanyAuthority");
                });

            modelBuilder.Entity("CompanyAgreement.Models.Company", b =>
                {
                    b.Navigation("CompanyDepartments");

                    b.Navigation("ContractInformations");

                    b.Navigation("ContractSituation");
                });

            modelBuilder.Entity("CompanyAgreement.Models.CompanyInformation", b =>
                {
                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyAgreement.Models.Department", b =>
                {
                    b.Navigation("CompanyDepartments");
                });
#pragma warning restore 612, 618
        }
    }
}
