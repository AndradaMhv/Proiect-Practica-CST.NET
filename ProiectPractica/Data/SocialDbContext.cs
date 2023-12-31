﻿using Microsoft.EntityFrameworkCore;
using ProiectPractica.DataModels;

namespace ProiectPractica.Data
{
    public class SocialDbContext:DbContext
    {
        public SocialDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendRequest>(b =>
            {
                b.HasKey(x => new { x.SenderId, x.ReciverId });

                b.HasOne(x => x.Sender)
                .WithMany(x => x.FriendRequestsSend)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.Reciver)
                .WithMany(x => x.FriendRequestsRecived)
                .HasForeignKey(x => x.ReciverId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasKey(x => new { x.User1Id, x.User2Id });

                b.HasOne(x => x.User1)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.User2)
                .WithMany(x => x.FriendsOf)
                .HasForeignKey(b => b.User2Id)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }



    }
}
