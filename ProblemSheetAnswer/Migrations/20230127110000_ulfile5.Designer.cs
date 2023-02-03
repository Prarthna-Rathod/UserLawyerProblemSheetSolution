﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProblemSheetAnswer.Models;

#nullable disable

namespace ProblemSheetAnswer.Migrations
{
    [DbContext(typeof(UserLawyerDbContext))]
    [Migration("20230127110000_ulfile5")]
    partial class ulfile5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProblemSheetAnswer.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LawyerId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LawyerId");

                    b.ToTable("feedbacks");
                });

            modelBuilder.Entity("ProblemSheetAnswer.Models.Lawyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double?>("AvgRate")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lawyers");
                });

            modelBuilder.Entity("ProblemSheetAnswer.Models.OldConversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("QuesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuesId");

                    b.ToTable("oldConversations");
                });

            modelBuilder.Entity("ProblemSheetAnswer.Models.Questions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Assign")
                        .HasColumnType("bit");

                    b.Property<int?>("AssignTo")
                        .HasColumnType("int");

                    b.Property<int?>("AssignToLawyerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsUserSatisfied")
                        .HasColumnType("bit");

                    b.Property<int?>("LawyerId")
                        .HasColumnType("int");

                    b.Property<string>("MediaFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Picked")
                        .HasColumnType("bit");

                    b.Property<int?>("PickedBy")
                        .HasColumnType("int");

                    b.Property<int?>("PickedByLawyerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignToLawyerId");

                    b.HasIndex("LawyerId");

                    b.HasIndex("PickedByLawyerId");

                    b.HasIndex("UserId");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("ProblemSheetAnswer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Mobile")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProblemSheetAnswer.Models.Feedback", b =>
                {
                    b.HasOne("ProblemSheetAnswer.Models.Lawyer", "Lawyer")
                        .WithMany()
                        .HasForeignKey("LawyerId");

                    b.Navigation("Lawyer");
                });

            modelBuilder.Entity("ProblemSheetAnswer.Models.OldConversation", b =>
                {
                    b.HasOne("ProblemSheetAnswer.Models.Questions", "Question")
                        .WithMany()
                        .HasForeignKey("QuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ProblemSheetAnswer.Models.Questions", b =>
                {
                    b.HasOne("ProblemSheetAnswer.Models.Lawyer", "AssignToLawyer")
                        .WithMany()
                        .HasForeignKey("AssignToLawyerId");

                    b.HasOne("ProblemSheetAnswer.Models.Lawyer", "Lawyer")
                        .WithMany()
                        .HasForeignKey("LawyerId");

                    b.HasOne("ProblemSheetAnswer.Models.Lawyer", "PickedByLawyer")
                        .WithMany()
                        .HasForeignKey("PickedByLawyerId");

                    b.HasOne("ProblemSheetAnswer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignToLawyer");

                    b.Navigation("Lawyer");

                    b.Navigation("PickedByLawyer");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
