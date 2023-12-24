﻿// <auto-generated />
using System;
using Databaser_Labb3_V2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Databaser_Labb3_V2.Migrations
{
    [DbContext(typeof(EdugradeHighSchoolContext))]
    [Migration("20231220103613_Exempel")]
    partial class Exempel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Avdelning", b =>
                {
                    b.Property<int>("AvdelningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvdelningId"));

                    b.Property<string>("AvdelningNamn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AvdelningId");

                    b.ToTable("Avdelning", (string)null);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Betyg", b =>
                {
                    b.Property<int>("BetygId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BetygId"));

                    b.Property<int?>("AvdelningId")
                        .HasColumnType("int");

                    b.Property<string>("Betyg1")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("Betyg");

                    b.Property<DateOnly>("BetygDatum")
                        .HasColumnType("date");

                    b.Property<int?>("FkAvdelningId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonalId")
                        .HasColumnType("int")
                        .HasColumnName("FK_PersonalId");

                    b.Property<int>("FkStudentId")
                        .HasColumnType("int")
                        .HasColumnName("FK_StudentId");

                    b.Property<int>("FkÄmneId")
                        .HasColumnType("int")
                        .HasColumnName("FK_ÄmneId");

                    b.HasKey("BetygId");

                    b.HasIndex("AvdelningId");

                    b.HasIndex("FkPersonalId");

                    b.HasIndex("FkStudentId");

                    b.HasIndex("FkÄmneId");

                    b.ToTable("Betyg", (string)null);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.KlassList", b =>
                {
                    b.Property<int>("FkKlassId")
                        .HasColumnType("int")
                        .HasColumnName("FK_KlassId");

                    b.Property<int>("FkStudentId")
                        .HasColumnType("int")
                        .HasColumnName("FK_StudentId");

                    b.HasKey("FkKlassId", "FkStudentId");

                    b.HasIndex("FkStudentId");

                    b.ToTable("KlassList", (string)null);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Klasser", b =>
                {
                    b.Property<int>("KlassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KlassId"));

                    b.Property<string>("KlassNamn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("KlassId");

                    b.ToTable("Klasser", (string)null);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Personal", b =>
                {
                    b.Property<int>("PersonalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonalId"));

                    b.Property<int?>("FkAvdelningId")
                        .HasColumnType("int")
                        .HasColumnName("FK_AvdelningId");

                    b.Property<byte>("PersonalBefattning")
                        .HasColumnType("tinyint");

                    b.Property<string>("PersonalEfternamn")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("PersonalFörnamn")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("PersonalKön")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<decimal?>("PersonalLön")
                        .HasColumnType("money");

                    b.Property<string>("PersonalNamn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PersonalSsn")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("PersonalSSN");

                    b.Property<DateOnly?>("PersonalStartDatum")
                        .HasColumnType("date");

                    b.Property<byte?>("PersonalÅlder")
                        .HasColumnType("tinyint");

                    b.HasKey("PersonalId");

                    b.HasIndex("FkAvdelningId");

                    b.ToTable("Personal", null, t =>
                        {
                            t.HasTrigger("PersonalAgeGenderName");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Studenter", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("StudentEfternamn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("StudentFödelsedag")
                        .HasColumnType("datetime");

                    b.Property<string>("StudentFörnamn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StudentKön")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("StudentNamn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StudentSsn")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("StudentSSN");

                    b.Property<DateOnly?>("StudentStartDatum")
                        .HasColumnType("date");

                    b.Property<byte?>("StudentÅlder")
                        .HasColumnType("tinyint");

                    b.HasKey("StudentId")
                        .HasName("PK_Students");

                    b.ToTable("Studenter", null, t =>
                        {
                            t.HasTrigger("StudentAgeGenderName");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.View_GetGradesFromLastMonth", b =>
                {
                    b.Property<string>("Betyg")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<DateOnly>("Datum")
                        .HasColumnType("date");

                    b.Property<string>("Lärare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Student")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ämne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);

                    b.ToView("View_GetGradesFromLastMonth", (string)null);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Ämnen", b =>
                {
                    b.Property<int>("ÄmneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ÄmneId"));

                    b.Property<string>("ÄmneAktivt")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("ÄmneNamn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ÄmneId")
                        .HasName("PK_Ämne");

                    b.ToTable("Ämnen", (string)null);
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Betyg", b =>
                {
                    b.HasOne("Databaser_Labb3_V2.Models.Avdelning", "Avdelning")
                        .WithMany()
                        .HasForeignKey("AvdelningId");

                    b.HasOne("Databaser_Labb3_V2.Models.Personal", "FkPersonal")
                        .WithMany("Betygs")
                        .HasForeignKey("FkPersonalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Betyg_PersonalId");

                    b.HasOne("Databaser_Labb3_V2.Models.Studenter", "FkStudent")
                        .WithMany("Betygs")
                        .HasForeignKey("FkStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Betyg_StudentId");

                    b.HasOne("Databaser_Labb3_V2.Models.Ämnen", "FkÄmne")
                        .WithMany("Betygs")
                        .HasForeignKey("FkÄmneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Betyg_ÄmneId");

                    b.Navigation("Avdelning");

                    b.Navigation("FkPersonal");

                    b.Navigation("FkStudent");

                    b.Navigation("FkÄmne");
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.KlassList", b =>
                {
                    b.HasOne("Databaser_Labb3_V2.Models.Klasser", "FkKlass")
                        .WithMany()
                        .HasForeignKey("FkKlassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_KlassList_Klasser");

                    b.HasOne("Databaser_Labb3_V2.Models.Studenter", "FkStudent")
                        .WithMany()
                        .HasForeignKey("FkStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_KlassList_StudentId");

                    b.Navigation("FkKlass");

                    b.Navigation("FkStudent");
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Personal", b =>
                {
                    b.HasOne("Databaser_Labb3_V2.Models.Avdelning", "FkAvdelning")
                        .WithMany("Personals")
                        .HasForeignKey("FkAvdelningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Personal_Avdelning");

                    b.Navigation("FkAvdelning");
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Avdelning", b =>
                {
                    b.Navigation("Personals");
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Personal", b =>
                {
                    b.Navigation("Betygs");
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Studenter", b =>
                {
                    b.Navigation("Betygs");
                });

            modelBuilder.Entity("Databaser_Labb3_V2.Models.Ämnen", b =>
                {
                    b.Navigation("Betygs");
                });
#pragma warning restore 612, 618
        }
    }
}
