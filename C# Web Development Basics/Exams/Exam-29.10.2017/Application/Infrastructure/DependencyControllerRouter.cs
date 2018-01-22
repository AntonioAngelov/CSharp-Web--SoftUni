
namespace Application.Infrastructure
{
    using Data;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Routers;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;
    using System;
    using Services;
    using Services.Contracts;

    public class DependencyControllerRouter : ControllerRouter
    {
        private readonly Container container;

        public DependencyControllerRouter()
        {
            this.container = new Container();
            this.container.Options.DefaultScopedLifestyle
                = new AsyncScopedLifestyle();
        }

        public Container Container => this.container;

        public static DependencyControllerRouter Get()
        {
            var router = new DependencyControllerRouter();

            var container = router.Container;

            //add like that
            container.Register<IUserService, UserService>();
            container.Register<ISubmissionService, SubmissionService>();
            container.Register<IContestService, ContestService>();
            container.Register<JudgeDbContext>(Lifestyle.Scoped);

            container.Verify();

            return router;
        }

        protected override Controller CreateController(Type controllerType)
        {
            AsyncScopedLifestyle.BeginScope(this.Container);
            return (Controller)this.Container.GetInstance(controllerType);
        }
    }
}
