﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationWithAuth.Data;

namespace WebApplicationWithAuth.Data.MigrationsLKA
{
    [DbContext(typeof(LKADbContext))]
    [Migration("20220410081249_ApplicationExamsChangesLKA")]
    partial class ApplicationExamsChangesLKA
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplicationWithAuth.Models.ApplicationsExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompetitionsExamId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("EnrolleeId")
                        .HasColumnType("int");

                    b.Property<int>("EnrolleesDocId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("SubjectsSubstitutorId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ApplicationsExams");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Budget")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speciality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.CompetitionsExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompetitionId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("MaxScore")
                        .HasColumnType("int");

                    b.Property<int>("MinScore")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<bool>("achievementIsFeasible")
                        .HasColumnType("bit");

                    b.Property<bool>("egeIsFeasible")
                        .HasColumnType("bit");

                    b.Property<bool>("examIsFeasible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CompetitionsExams");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.DocType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanBeDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocTypes");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.Enrollee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.ToTable("Enrollees");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.EnrolleesCompetition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompetitionId")
                        .HasColumnType("int");

                    b.Property<int>("EnrolleeID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("EnrollesCompetitions");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.EnrolleesDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Authority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocTypeId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("EnrolleeID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Series")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EnrollesDocs");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.EnrolleesFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnrolleeID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("EnrolleesDocID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EnrollesFiles");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.Relative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EnrolleeID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Relatives");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasMaxLength(10)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebApplicationWithAuth.Models.SubjectSubstitutor", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<int>("SubjectsSubstitutorId")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.ToTable("SubjectSubstitutors");
                });
#pragma warning restore 612, 618
        }
    }
}
