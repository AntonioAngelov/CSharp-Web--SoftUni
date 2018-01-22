namespace SoftUniGameStore.Application
{
    using System;
    using System.Globalization;
    using Controllers;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using ViewModels.Account;
    using ViewModels.Admin;

    public class GameStoreApp : IApplication
    {
        public void InitializeDb()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AnonymousPaths.Add("/home/all");
            appRouteConfig.AnonymousPaths.Add("/account/register");
            appRouteConfig.AnonymousPaths.Add("/account/login");

            appRouteConfig
                .Get("/home/all",
                req => new HomeController(req).Index());

            appRouteConfig
                .Get(
                "/account/register",
                req => new AccountController(req).Register());

            appRouteConfig
                .Post(
                    "/account/register",
                    req => new AccountController(req).Register(new ReisterViewModel
                    {
                        Email = req.FormData["e-mail"],
                        ConfirmPassword = req.FormData["confirm-password"],
                        Name = req.FormData["name"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get(
                "/account/logout",
                req => new AccountController(req).Logout());

            appRouteConfig
                .Get(
                "/account/login",
                req => new AccountController(req).Login());

            appRouteConfig
                .Post(
                    "/account/login",
                    req => new AccountController(req).Login(new LoginViewModel
                    {
                        Email = req.FormData["email"],
                        Password = req.FormData["password"]
                    }));

            appRouteConfig
                .Get(
                "/admin/games/add",
                req => new AdminController(req).Add());

            appRouteConfig
                .Post(
                    "/admin/games/add",
                    req => new AdminController(req).Add(new AddGameViewModel
                    {
                        Description = req.FormData["description"],
                        Price = decimal.Parse(req.FormData["price"]),
                        ReleaseDate = DateTime.ParseExact(
                            req.FormData["release-date"],
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture),
                        Size = double.Parse(req.FormData["size"]),
                        Thumbnail = req.FormData["thumbnail"],
                        Title = req.FormData["title"],
                        VideoId = req.FormData["video-id"]
                    }));

            appRouteConfig
                .Get(
                "/admin/games/list",
                req => new AdminController(req).List());

            appRouteConfig
                .Get(
                "/admin/games/edit/{(?<id>[0-9]+)}",
                req => new AdminController(req).Edit(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(
                    "/admin/games/edit/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Edit(new FullGameInfoViewModel()
                    {
                        Description = req.FormData["description"],
                        Price = decimal.Parse(req.FormData["price"]),
                        ReleaseDate = DateTime.ParseExact(
                            req.FormData["release-date"],
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture),
                        Size = double.Parse(req.FormData["size"]),
                        Thumbnail = req.FormData["thumbnail"],
                        Title = req.FormData["title"],
                        VideoId = req.FormData["video-id"]
                    }));

            appRouteConfig
                .Get(
                    "/admin/games/delete/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Delete(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(
                    "/admin/games/delete/{(?<id>[0-9]+)}",
                    req => new AdminController(req).Delete());

            appRouteConfig
                .Get("/games/details/{(?<id>[0-9]+)}",
                req => new GamesController(req).Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get(
                    "/shopping/add/{(?<id>[0-9]+)}",
                    req => new ShoppingController(req).AddToCart());

            appRouteConfig
                .Get(
                    "/cart",
                    req => new ShoppingController(req).ShowCart());

            appRouteConfig
                .Get(
                    "/shopping/remove/{(?<id>[0-9]+)}",
                    req => new ShoppingController(req).RemoveFromCart(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Post(
                    "/shopping/finish-order",
                    req => new ShoppingController(req).FinishOrder());
        }
    }
}
