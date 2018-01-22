namespace _02.SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=SocialNetwork;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User-Friends
            modelBuilder.Entity<UserFriend>()
                .HasKey(uf => new { uf.FirstUserId, uf.SecondUserId });

            modelBuilder.Entity<UserFriend>()
                .HasOne(uf => uf.FirstUser)
                .WithMany(fu => fu.Followers)
                .HasForeignKey(uf => uf.FirstUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFriend>()
                .HasOne(uf => uf.SecondUser)
                .WithMany(su => su.Following)
                .HasForeignKey(uf => uf.SecondUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Album-Picture
            modelBuilder.Entity<AlbumPicture>()
                .HasKey(ap => new {ap.AlbumId, ap.PictureId});

            modelBuilder.Entity<AlbumPicture>()
                .HasOne(ap => ap.Album)
                .WithMany(a => a.Pictures)
                .HasForeignKey(ap => ap.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AlbumPicture>()
                .HasOne(ap => ap.Picture)
                .WithMany(p => p.Albums)
                .HasForeignKey(ap => ap.PictureId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //Users-Albums
            modelBuilder.Entity<UserAlbum>()
                .HasKey(ua => new {ua.UserId, ua.AlbumId});

            modelBuilder.Entity<UserAlbum>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.Albums)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAlbum>()
                .HasOne(ua => ua.Album)
                .WithMany(u => u.AlbumsHolder)
                .HasForeignKey(ua => ua.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            //Albums-Tags
            modelBuilder.Entity<AlbumTag>()
                .HasKey(at => new {at.AlbumId, at.TagId});

            modelBuilder.Entity<AlbumTag>()
                .HasOne(at => at.Album)
                .WithMany(a => a.Tags)
                .HasForeignKey(at => at.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AlbumTag>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.Albums)
                .HasForeignKey(at => at.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
