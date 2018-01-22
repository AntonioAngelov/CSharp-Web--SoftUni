namespace SoftUniGameStore.Application.Controllers
{
    using System;
    using System.Linq;
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Services;
    using Services.Contracts;
    using ViewModels;

    public class ShoppingController : Controller
    {
        private const string CartView = @"/cart";

        private readonly IGameService games;
        private readonly IUserService users;

        public ShoppingController(IHttpRequest request)
            : base(request)
        {
            this.games = new GameService();
            this.users = new UserService();
        }

        public IHttpResponse AddToCart()
        {
            if (!this.Authentication.IsAuthenticated)
            {
                return this.RedirectResponse(Controller.HomePath);
            }

            var gameId = int.Parse(this.Request.UrlParameters["id"]);

            var gameExists = this.games
                .Exists(gameId);

            if (!gameExists)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = this.Request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            var userEmail = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);

            if (!this.users.OwnsGame(userEmail, gameId) &&
                !shoppingCart.GamesIds.Contains(gameId))
            {
                shoppingCart.GamesIds.Add(gameId);
            }

            return this.RedirectResponse(Controller.HomePath);
        }

        public IHttpResponse ShowCart()
        {
            if (!this.Authentication.IsAuthenticated)
            {
                return this.RedirectResponse(Controller.HomePath);
            }

            var shoppingCart = this.Request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.GamesIds.Any())
            {
                this.ViewData["dispayCartItems"] = "none";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var gamesInCart = this.games
                    .FindGamesInCart(shoppingCart.GamesIds);

                var gamesHtml = gamesInCart
                    .Select(g => $@"<div class=""list-group-item"">
                                        <div class=""media"">
                                           <a class=""btn btn-outline-danger btn-lg align-self-center mr-3"" href=""/shopping/remove/{g.Id}"">X</a>
                                           <img class=""d-flex mr-4 align-self-center img-thumbnail"" height=""127"" src=""{g.Thumbnail}""
                                           width=""227"" alt=""Generic placeholder image"">
                                           <div class=""media-body align-self-center"">
                                               <a href = ""/games/details/{g.Id}"" >
                                               <h4 class=""mb-1 list-group-item-heading""> {g.Title} </h4>
                                               </a>
                                               <p>
                                               {g.Description}
                                               </p>
                                          </div>
                                          <div class=""col-md-2 text-center align-self-center mr-auto"">
                                              <h2> {g.Price}&euro; </h2>
                                          </div>
                                      </div>
                                   </div>");

                var totalPrice = gamesInCart
                    .Sum(pr => pr.Price);

                this.ViewData["dispayCartItems"] = "flex";
                this.ViewData["gamesInCart"] = string.Join(string.Empty, gamesHtml);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse RemoveFromCart(int gameId)
        {
            if (!this.Authentication.IsAuthenticated)
            {
                return this.RedirectResponse(Controller.HomePath);
            }

            var gameExists = this.games
                .Exists(gameId);

            var shoppingCart = this.Request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!gameExists)
            {
                return new NotFoundResponse();
            }

            if (!shoppingCart.GamesIds.Contains(gameId))
            {
                return this.RedirectResponse(CartView);
            }

            shoppingCart.GamesIds.Remove(gameId);

            return this.RedirectResponse(CartView);
        }

        public IHttpResponse FinishOrder()
        {
            var userEmail = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppingCart = this.Request.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            var userId = this.users.GetUserId(userEmail);

            if (userId == null)
            {
                throw new InvalidOperationException($"User with email {userEmail} does not exist.");
            }

            var gamesIds = shoppingCart.GamesIds;

            if (!gamesIds.Any())
            {
                return new RedirectResponse(HomePath);
            }

            this.users.AddGames(userId.Value, gamesIds);

            shoppingCart.GamesIds.Clear();

            return new RedirectResponse("/home/all?filter=Owned");
        }
    }
}