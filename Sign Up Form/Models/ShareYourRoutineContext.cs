using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Sign_Up_Form.Models;
using Sign_Up_Form.Models.ViewModel;

namespace Sign_Up_Form
{
    public partial class ShareYourRoutineContext : IdentityDbContext<SignUpUser>
    {
        public ShareYourRoutineContext()
        {
        }

        public ShareYourRoutineContext(DbContextOptions<ShareYourRoutineContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<UserDatum> UserData { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\Users\\grengo\\Desktop\\ShareyourRoutine\\ShareYourRoutine\\db\\ShareYourRoutine.mdf;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.MsgId).HasColumnName("msgId");

                entity.HasOne(d => d.Msg)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MsgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_ToTable");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_ToTable_1");
            });
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.GeneratorTime).HasColumnType("datetime");

                entity.HasOne(d => d.GeneratorUser)
                    .WithMany(p => p.NotificationGeneratorUsers)
                    .HasForeignKey(d => d.GeneratorUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_ToTable_2");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_ToTable");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NotificationUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_ToTable_1");
            });
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.Property(e => e.Msg)
                    .IsUnicode(false)
                    .HasColumnName("msg");

                entity.HasOne(d => d.Reciever)
                    .WithMany(p => p.MessageRecievers)
                    .HasForeignKey(d => d.RecieverId)
                    .HasConstraintName("FK_message_ToTable_1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK_message_ToTable");
            });
            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Login_ToTable");
            });
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Table_ToTableofLike");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Table_ToTableofLike1");
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.PostCategory).IsUnicode(false);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.PostMessage).IsUnicode(false);
            });

            modelBuilder.Entity<UserDatum>(entity =>
            {
                entity.Property(e => e.About).IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.JoiningDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override int SaveChanges()
        {
            var tracker = ChangeTracker;

            foreach(var item in tracker.Entries().Where(e=> e.Entity is RecInfo))
            {

                var entity=item.Entity as RecInfo;
                switch (item.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entity.ModifiedDate = DateTime.Now;
                        break;
                    default:
                        break;

                }

            }

                //foreach (var entity in tracker.Entries())//tracker.enteries has all enteries which are going to do some change in db
                //{
                //    if (entity.Entity is RecInfo)
                //    {
                //        var refEntity = entity.Entity as Like;
                //        switch (entity.State)
                //        {
                //            case EntityState.Added:
                //                refEntity.CreatedDate = DateTime.Now;
                //                break;
                //            case EntityState.Modified:
                //                refEntity.ModifiedDate = DateTime.Now;
                //                break;
                //            default:
                //                break;

                //        }
                //    }
                //}
                return base.SaveChanges();
        }
    }
}
