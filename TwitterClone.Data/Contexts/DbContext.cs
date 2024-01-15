using Microsoft.EntityFrameworkCore;
using System;
using TwitterClone.Entity;

namespace TwitterClone.Contexts
{
    public class TwitterCloneContext : DbContext
    {
        public TwitterCloneContext(DbContextOptions<TwitterCloneContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<Notification> Notifications { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<RePost> RePosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kullanıcıların takip listesi için çoktan çoğa ilişki
            modelBuilder.Entity<Follow>()
        .HasKey(f => new { f.FollowerId, f.FollowingId });

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Follower) // Takip eden kullanıcı
                .WithMany() // Burada ilişkiyi yalnızca belirtiyoruz, koleksiyonu belirtmiyoruz
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict); // Kendi kendini takip etmesini önle

            modelBuilder.Entity<Follow>()
                .HasOne(f => f.Following) // Takip edilen kullanıcı
                .WithMany() // Burada ilişkiyi yalnızca belirtiyoruz, koleksiyonu belirtmiyoruz
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Bir kullanıcının birden çok post'u olabilir
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            // Beğeniler
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserId, l.PostId });

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId);

            // Mesajlaşma
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.ReceiverId);

            // Media
            modelBuilder.Entity<Media>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            // Notification
            //modelBuilder.Entity<Notification>()
              //  .Property(n => n.Date)
                //.HasDefaultValueSql("CURRENT_TIMESTAMP");


            // Conversations
            modelBuilder.Entity<Conversation>()
                .HasKey(c => new { c.UserOneId, c.UserTwoId });

            // RePosts
            modelBuilder.Entity<RePost>()
                .HasOne(r => r.User)
                .WithMany(u => u.RePosts)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<RePost>()
                .HasOne(r => r.Post)
                .WithMany(p => p.RePosts)
                .HasForeignKey(r => r.PostId);

           // modelBuilder.Entity<Notification>()
             //   .Property(n => n.Date)
               // .HasDefaultValueSql("GETDATE()");
        }
    }
}
