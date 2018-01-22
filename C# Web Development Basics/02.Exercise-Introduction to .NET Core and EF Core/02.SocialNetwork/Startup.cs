namespace _02.SocialNetwork
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Text;
    using Models.Enums;
    using Utility;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new SocialNetworkDbContext())
            {
                context.Database.Migrate();
                SeedDbIfEmpty(context);

                //PrintUsersWithFriendsCount(context);
                //PrintActiveUsersWithMoreThanFiveFriends(context);

                //SeedDBWithAlbums(context);

                //ListAlbumsWithOwnerAndCountOFPictures(context);
                //ListPicturesIncludedInMoreThanTwoAlbums(context);
                //ListAlbumsOfUser(3, context);

                //AddTagsToAlbums(context);
                //RegisterUserTags(context);

                //ListAlbumsWithTag("#TestTag1", context);
                //ListUsersWithAlbumsWithMoreThanThreeTags(context);


                //ListUsersAndAlbumViewers(context);
                //ListSharedAlbums(context);
                //ListAlbumsSharedWithUser(context);
                
                //ListAllAlbumsWithUser(context);
                //ListUserAlbumInformation(context);
                //ListPublicAlbumViewers(context);
            }
        }

        private static void ListPublicAlbumViewers(SocialNetworkDbContext db)
        {
            var users = db.Users
                .Where(u => u.Albums.Any(us => us.UserRole == Role.Viewer))
                .Select(u => new
                {
                    u.Username,
                    PublicAlbumsShared = u.Albums.Where(a => a.UserRole == Role.Viewer && a.Album.IsPublic == true).Count()
                }).ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username} is an album viewer and has {user.PublicAlbumsShared} PUBLIC albums shared with him.");
            }
        }

        private static void ListUserAlbumInformation(SocialNetworkDbContext db)
        {
            var testUsername = "Gosho";

            var userAlbumInfo = db.Users
                .Where(u => u.Username == testUsername)
                .Select(u => new
                {
                    OwnerOf = u.Albums.Where(ah => ah.UserRole == Role.Owner).Count(),
                    ViewerOf = u.Albums.Where(ah => ah.UserRole == Role.Viewer).Count()
                })
                .First();

            Console.WriteLine($"User {testUsername} is the owner of {userAlbumInfo.OwnerOf} albums and the viewer of {userAlbumInfo.ViewerOf}.");
        }

        private static void ListAllAlbumsWithUser(SocialNetworkDbContext db)
        {
            var albums = db.Albums.
                Select(a => new
                {
                    AlbumName = a.Name,
                    Users = a.AlbumsHolder.Select(ah => new
                    {
                        Username = ah.User.Username,
                        Role = ah.UserRole
                    }).OrderBy(u => u.Role).ThenBy(u => u.Username)
                })
                .ToList()
                .OrderBy(a => a.Users.First().Username)
                .ThenByDescending(a => a.Users.Where(u => u.Role == Role.Viewer).Count())
                .ToList();

            foreach (var alb in albums)
            {
                Console.WriteLine($"Album: {alb.AlbumName}");
                foreach (var user in alb.Users)
                {
                    Console.WriteLine($"--User: {user.Username}, role = {user.Role}");
                }
            }
        }

        private static void ListAlbumsSharedWithUser(SocialNetworkDbContext db)
        {
            var testUsername = "Pesho";

            var sharedAlbums = db.Albums
                .Where(ah =>
                    ah.AlbumsHolder
                        .Any(a => a.User.Username == testUsername && a.UserRole == Role.Viewer)
                )
                .Select(a => new
                {
                    AlbumName = a.Name,
                    PictureCount = a.Pictures.Count
                })
                .OrderByDescending(a => a.PictureCount)
                .ThenBy(a => a.AlbumName)
                .ToList();

            Console.WriteLine($"Albums shared with: {testUsername}");
            foreach (var alb in sharedAlbums)
            {
                Console.WriteLine($"-Album: {alb.AlbumName}");
                Console.WriteLine($"-Pictures: {alb.PictureCount}");
            }
        }

        private static void ListSharedAlbums(SocialNetworkDbContext db)
        {
            var albums = db.Albums
                .Where(a => a.AlbumsHolder.Where(ah => ah.UserRole == Role.Viewer).Count() > 2)
                .Select(a => new
                {
                    AlbumName = a.Name,
                    SharedCount = a.AlbumsHolder.Where(ah => ah.UserRole == Role.Viewer).Count(),
                    IsPublic = a.IsPublic
                })
                .OrderByDescending(a => a.SharedCount)
                .ThenBy(a => a.AlbumName)
                .ToList();

            foreach (var alb in albums)
            {
                Console.WriteLine($"Album: {alb.AlbumName}");
                Console.WriteLine($"-Shared with: {alb.SharedCount} people");
                Console.WriteLine($"-Access is {(alb.IsPublic ? "public" : "private")}");
            }
        }

        private static void ListUsersAndAlbumViewers(SocialNetworkDbContext db)
        {
            var usrs = db.Users
                .Select(u => new
                {
                    u.Username,
                    FollowerIds = u.Followers.Select(f => f.SecondUserId),
                    AlbumsOwnedIds = u.Albums
                                        .Where(a => a.UserRole == Role.Owner)
                                        .Select(a => a.AlbumId)
                })
                .OrderBy(u => u.Username)
                .ToList();

            foreach (var us in usrs)
            {
                var sb = new StringBuilder();

                bool userNameAppended = false;

                foreach (var fol in us.FollowerIds)
                {
                    var follower = db.Users
                        .Include(u => u.Albums)
                        .First(f => f.Id == fol);

                    foreach (var alb in follower.Albums)
                    {
                        db.Entry(alb).Reference(u => u.Album).Load();
                    }

                    var albumNames = follower.Albums
                        .Where(a => us.AlbumsOwnedIds.Contains(a.AlbumId))
                        .Select(a => a.Album.Name)
                        .ToList();
                    if (albumNames.Count > 0)
                    {
                        if (userNameAppended == false)
                        {
                            sb.AppendLine($"User: { us.Username}");
                            userNameAppended = true;
                        }

                        sb.AppendLine($"-Follower: {follower.Username}");

                        string result = string.Join(", ", albumNames);

                        sb.AppendLine($"--Albums shared: {result}");
                    }
                }

                if (sb.Length > 0)
                {
                    Console.WriteLine(sb.ToString().Trim());
                }
            }
        }

        private static void ListUsersWithAlbumsWithMoreThanThreeTags(SocialNetworkDbContext context)
        {
            var users = context.Users
                .Where(u => u.Albums.Any(a => a.Album.Tags.Count > 3))
                .Select(u => new
                {
                    u.Username,
                    Albums = u.Albums
                        .Select(a => new
                        {
                            a.Album.Name,
                            Tags = a.Album.Tags
                                .Select(t => t.Tag.Title)
                        })
                })
                .ToList()
                .OrderByDescending(u => u.Albums.Count())
                .ThenByDescending(u => u.Albums.Sum(at => at.Tags.Count()))
                .ThenBy(u => u.Username);
            

            if (users.Any())
            {
                Console.WriteLine($"Users that have albums with more than 3 tags: ");

                foreach (var user in users)
                {
                    Console.WriteLine($"User: {user.Username}");
                    Console.WriteLine("Albums: ");

                    foreach (var album in user.Albums)
                    {
                        Console.WriteLine("-------------------");
                        Console.WriteLine($"{album.Name} -> [{string.Join(", ", album.Tags)}]");
                    }

                    Console.WriteLine("==========================");
                }
            }
            else
            {
                Console.WriteLine($"Users that have albums with more than 3 tags: None");
            }
        }

        private static void ListAlbumsWithTag(string tag, SocialNetworkDbContext context)
        {
            var albums = context.Albums
                .Where(a => a.Tags.Any(t => t.Tag.Title == tag))
                .Select(a => new
                {
                    AlbumName = a.Name,
                    OwnerName = a.AlbumsHolder
                    .FirstOrDefault(ah => ah.UserRole == Role.Owner)
                    .User
                    .Username,
                    NumberOfTags = a.Tags.Count
                })
                .OrderByDescending(a => a.NumberOfTags)
                .ThenBy(a => a.AlbumName)
                .ToList();

            if (albums.Any())
            {
                Console.WriteLine($"Albums tagged {tag}:");
            }
            else
            {
                Console.WriteLine($"Albums tagged {tag}: None");
            }

            foreach (var album in albums)
            {
                Console.WriteLine("----------------");
                Console.WriteLine($"Album title: {album.AlbumName}");
                Console.WriteLine($"Owner: {album.OwnerName}");
            }
        }

        private static void AddTagsToAlbums(SocialNetworkDbContext context)
        {
            if (!context.Tags.Any())
            {
                string[] sampleTags = new string[] { "#TestTag1", "#TestTag2", "#TestTag3", "#TestTag4", "#TestTag5" };

                foreach (var tag in sampleTags)
                {
                    if (!context.Tags.Any(t => t.Title == tag))
                    {
                        context.Tags.Add(new Tag
                        {
                            Title = tag
                        });
                    }
                }

                context.SaveChanges();

                Random rnd = new Random();

                var albums = context.Albums
                    .ToList();
                var tagsIds = context.Tags
                    .Select(t => t.Id)
                    .ToList();

                foreach (var album in albums)
                {
                    album.Tags.Add(new AlbumTag()
                    {
                        TagId = tagsIds[rnd.Next(0, tagsIds.Count - 1)]
                    });
                }

                context.SaveChanges();
            }
        }

        private static void RegisterUserTags(SocialNetworkDbContext context)
        {
            while (true)
            {
                Console.Write("Enter tag name or press ENTER to exit: ");
                string tagValue = Console.ReadLine();

                if (string.IsNullOrEmpty(tagValue) || string.IsNullOrWhiteSpace(tagValue))
                {
                    break;
                }
                else
                {
                    var tag = new Tag
                    {
                        Title = TagTransformer.Transform(tagValue)
                    };

                    if (context.Tags.Any(t => t.Title == tagValue))
                    {
                        Console.WriteLine($"Database already contains such a tag: {tagValue}");
                    }
                    else
                    {
                        context.Tags.Add(tag);

                        context.SaveChanges();

                        Console.WriteLine($"Tag: {tag.Title} was added to the database.");
                    }
                }
            }
        }


        private static void ListAlbumsOfUser(int userId, SocialNetworkDbContext context)
        {
            var userInfo = context.Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    Name = u.Username,
                    Albums = u.Albums
                        .Select(a => new
                        {
                            a.Album.Name,
                            a.Album.IsPublic,
                            Pictures = a.Album.Pictures
                                .Select(p => new
                                {
                                    p.Picture.Title,
                                    p.Picture.Path
                                })
                        })
                        .OrderBy(a => a.Name)
                })
                .FirstOrDefault();

            Console.WriteLine($"Username: {userInfo.Name}");

            if (userInfo.Albums.Any())
            {
                Console.WriteLine("Abums:");
            }
            else
            {
                Console.WriteLine("Abums: None");
            }

            foreach (var album in userInfo.Albums)
            {
                Console.WriteLine("------------------");
                Console.WriteLine($"Album title: {album.Name}");

                if (album.IsPublic)
                {
                    if (album.Pictures.Any())
                    {
                        Console.WriteLine("Pictures in album:");
                    }
                    else
                    {
                        Console.WriteLine("Pictures in album: None");
                    }

                    foreach (var picture in album.Pictures)
                    {
                        Console.WriteLine($"{picture.Title}--{picture.Path}");
                    }
                }
                else
                {
                    Console.WriteLine("Private content!");
                }
            }
        }

        private static void ListPicturesIncludedInMoreThanTwoAlbums(SocialNetworkDbContext context)
        {
            var pictures = context.Pictures
                .Where(p => p.Albums.Count > 2)
                .Select(p => new
                {
                    p.Title,
                    Albums = p.Albums
                        .Select(a => new
                        {
                            AlbumName = a.Album.Name,
                            OwnerName = a.Album
                            .AlbumsHolder
                            .FirstOrDefault(ah => ah.UserRole == Role.Owner)
                            .User
                            .Username
                        })
                })
                .OrderByDescending(p => p.Albums.Count())
                .ThenBy(p => p.Title)
                .ToList();

            foreach (var picture in pictures)
            {
                Console.WriteLine($"Picture Title: {picture.Title}");
                Console.WriteLine("++++++++++++++++++++++");
                Console.WriteLine($"Album----Owner");
                foreach (var album in picture.Albums)
                {
                    Console.WriteLine($"{album.AlbumName}----{album.OwnerName}");
                }

                Console.WriteLine("========================");
            }
        }

        private static void ListAlbumsWithOwnerAndCountOFPictures(SocialNetworkDbContext context)
        {
            var albums = context.Albums
                .Select(a => new
                {
                    a.Name,
                    OwnerName = a
                        .AlbumsHolder
                        .FirstOrDefault(ah => ah.UserRole == Role.Owner)
                        .User
                        .Username,
                    PicturesCount = a.Pictures.Count
                })
                .OrderByDescending(a => a.PicturesCount)
                .ThenBy(a => a.OwnerName)
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album name: {album.Name}");
                Console.WriteLine($"Owner: {album.OwnerName}");
                Console.WriteLine($"Pictures: {album.PicturesCount}");
                Console.WriteLine("============================");
            }
        }

        private static void PrintActiveUsersWithMoreThanFiveFriends(SocialNetworkDbContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    u.Username,
                    u.IsDeleted,
                    FriendsCount = u.Followers.Count,
                    u.RegisteredOn
                })
                .Where(u => (u.IsDeleted == false || u.IsDeleted == null) && u.FriendsCount > 5)
                .OrderBy(u => u.RegisteredOn)
                .ThenByDescending(u => u.FriendsCount)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Friends: {user.FriendsCount}");
                if (user.RegisteredOn == null)
                {
                    Console.WriteLine($"Time in network: Unknown");
                }
                else
                {
                    Console.WriteLine($"Time in network: {Math.Round(DateTime.Now.Subtract(user.RegisteredOn ?? DateTime.Now).TotalDays)}");
                }
            }
        }

        private static void PrintUsersWithFriendsCount(SocialNetworkDbContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    u.Username,
                    FrindsCount = u.Followers.Count,
                    u.IsDeleted
                })
                .OrderByDescending(u => u.FrindsCount)
                .ThenBy(u => u.FrindsCount)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Friends: {user.FrindsCount}");
                if (user.IsDeleted == true || user.IsDeleted == null)
                {
                    Console.WriteLine($"Status: Active");
                }
                else
                {
                    Console.WriteLine($"Status: Inactive");
                }

                Console.WriteLine("==========================");
            }
        }

        private static void SeedDBWithAlbums(SocialNetworkDbContext context)
        {
            var HasNoAlbums = context.Albums.Count() == 0;

            if (HasNoAlbums)
            {
                Console.WriteLine("Seeding albums and pictures...");

                var usersIds = context.Users
                    .Select(u => u.Id)
                    .ToList();
                var rnd = new Random();

                var album1 = new Album
                {
                    Name = "AlbumLoL",
                    BackgroundColor = "Brown",
                    IsPublic = true,
                    OwnerId = usersIds[rnd.Next(0, usersIds.Count() - 1)]
                };

                var album2 = new Album
                {
                    Name = "AlbumSmok",
                    BackgroundColor = "Black",
                    IsPublic = false,
                    OwnerId = usersIds[rnd.Next(0, usersIds.Count() - 1)]
                };

                var album3 = new Album
                {
                    Name = "AlbumBok",
                    BackgroundColor = "Purple",
                    IsPublic = true,
                    OwnerId = usersIds[rnd.Next(0, usersIds.Count() - 1)]
                };

                var album4 = new Album
                {
                    Name = "AlbumShliok",
                    BackgroundColor = "Rose",
                    IsPublic = true,
                    OwnerId = usersIds[rnd.Next(0, usersIds.Count() - 1)]
                };

                var album5 = new Album
                {
                    Name = "AlbumTrop",
                    BackgroundColor = "Pink",
                    IsPublic = false,
                    OwnerId = usersIds[rnd.Next(0, usersIds.Count() - 1)]
                };

                var pic1 = new Picture
                {
                    Title = "DSLR1.JPG",
                    Caption = "LoremIpsum1",
                    Path = @"C:\Lorem\Ipsum"
                };
                var pic2 = new Picture
                {
                    Title = "DSLR2.JPG",
                    Caption = "LoremIpsum2",
                    Path = @"C:\Lorem\Ipsum"
                };
                var pic3 = new Picture
                {
                    Title = "DSLR3.JPG",
                    Caption = "LoremIpsum3",
                    Path = @"C:\Lorem\Ipsum"
                };
                var pic4 = new Picture
                {
                    Title = "DSLR4.PNG",
                    Caption = "LoremIpsum4",
                    Path = @"C:\Lorem\Ipsum"
                };
                var pic5 = new Picture
                {
                    Title = "DSLR5.PNG",
                    Caption = "LoremIpsum5",
                    Path = @"C:\Lorem\Ipsum"
                };

                context.Albums.Add(album1);
                context.Albums.Add(album2);
                context.Albums.Add(album3);
                context.Albums.Add(album4);
                context.Albums.Add(album5);

                context.Pictures.Add(pic1);
                context.Pictures.Add(pic2);
                context.Pictures.Add(pic3);
                context.Pictures.Add(pic4);
                context.Pictures.Add(pic5);

                context.SaveChanges();

                //album1.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user1.Id,
                //    Role = Role.Owner
                //});

                //album1.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user2.Id,
                //    Role = Role.Viewer
                //});

                //album1.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user3.Id,
                //    Role = Role.Viewer
                //});

                //album1.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user4.Id,
                //    Role = Role.Viewer
                //});

                //album1.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user5.Id,
                //    Role = Role.Viewer
                //});

                //album1.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user6.Id,
                //    Role = Role.Viewer
                //});

                //album2.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user1.Id,
                //    Role = Role.Owner
                //});
                //album2.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user3.Id,
                //    Role = Role.Viewer
                //});

                //album3.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user1.Id,
                //    Role = Role.Owner
                //});
                //album3.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user3.Id,
                //    Role = Role.Viewer
                //});

                //album4.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user2.Id,
                //    Role = Role.Owner
                //});
                //album4.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user1.Id,
                //    Role = Role.Viewer
                //});

                //album5.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user3.Id,
                //    Role = Role.Owner
                //});
                //album5.AlbumHolders.Add(new UserAlbum
                //{
                //    UserId = user1.Id,
                //    Role = Role.Viewer
                //});

                //context.SaveChanges();

                album1.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic1.Id
                });

                album1.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic2.Id
                });

                album1.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic3.Id
                });

                album1.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic4.Id
                });

                album1.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic5.Id
                });

                album4.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic1.Id
                });

                album4.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic3.Id
                });

                album2.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic3.Id
                });

                album3.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic3.Id
                });

                album5.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic1.Id
                });

                album5.Pictures.Add(new AlbumPicture
                {
                    PictureId = pic5.Id
                });

                context.SaveChanges();
            }
        }

        private static void SeedDbIfEmpty(SocialNetworkDbContext context)
        {
            if (!context.Users.Any())
            {
                var user1 = new User
                {
                    Username = "Gosho",
                    Password = "123@Gog",
                    Email = "dadsd.goshov@abv.bg",
                    RegisteredOn = new DateTime(2015, 10, 20),
                    Age = 20
                };

                var user2 = new User
                {
                    Username = "Pesho",
                    Password = "123@Gog",
                    Email = "Goppppsho.goshov@abv.bg",
                    RegisteredOn = new DateTime(2015, 9, 19),
                    Age = 30
                };

                var user3 = new User
                {
                    Username = "Minko",
                    Password = "123@Gog",
                    Email = "popopo.goshov@abv.bg",
                    RegisteredOn = new DateTime(2015, 3, 12),
                    Age = 50
                };

                var user4 = new User
                {
                    Username = "Dinko",
                    Password = "123@Gog",
                    Email = "dofofofo.goshov@abv.bg",
                    RegisteredOn = new DateTime(2015, 5, 16),
                    Age = 10
                };

                var user5 = new User
                {
                    Username = "Bot5",
                    Password = "bot5@Gog",
                    Email = "55555.goshov@abv.bg",
                    RegisteredOn = new DateTime(200, 1, 16),
                    Age = 1
                };

                var user6 = new User
                {
                    Username = "Bot6",
                    Password = "bot6@Gog",
                    Email = "66666.goshov@abv.bg",
                    RegisteredOn = new DateTime(2000, 2, 16),
                    Age = 2
                };

                var user7 = new User
                {
                    Username = "Bot7",
                    Password = "bot7@Gog",
                    Email = "77777.goshov@abv.bg",
                    RegisteredOn = new DateTime(2000, 3, 16),
                    Age = 3
                };

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);
                context.Users.Add(user4);
                context.Users.Add(user5);
                context.Users.Add(user6);
                context.Users.Add(user7);

                context.SaveChanges();

                user1.Followers.Add(new UserFriend
                {
                    FirstUserId = user1.Id,
                    SecondUserId = user2.Id
                });

                user1.Followers.Add(new UserFriend
                {
                    FirstUserId = user1.Id,
                    SecondUserId = user3.Id
                });

                user1.Followers.Add(new UserFriend
                {
                    FirstUserId = user1.Id,
                    SecondUserId = user4.Id
                });

                user1.Followers.Add(new UserFriend
                {
                    FirstUserId = user1.Id,
                    SecondUserId = user5.Id
                });

                user1.Followers.Add(new UserFriend
                {
                    FirstUserId = user1.Id,
                    SecondUserId = user6.Id
                });

                user1.Followers.Add(new UserFriend
                {
                    FirstUserId = user1.Id,
                    SecondUserId = user7.Id
                });

                user2.Followers.Add(new UserFriend
                {
                    FirstUserId = user2.Id,
                    SecondUserId = user3.Id
                });

                user2.Followers.Add(new UserFriend
                {
                    FirstUserId = user2.Id,
                    SecondUserId = user4.Id
                });

                user3.Followers.Add(new UserFriend
                {
                    FirstUserId = user3.Id,
                    SecondUserId = user4.Id
                });

                context.SaveChanges();
                
            }
        }
    }
}
