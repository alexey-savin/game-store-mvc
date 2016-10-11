using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.Domain.Concrete;
using System.Configuration;

namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            /*
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games)
                .Returns(new List<Game>
                {
                    new Game { Name = "Sym City", Price = 1499 },
                    new Game { Name = "TITANFALL", Price = 2299 },
                    new Game { Name = "Battlefield 4", Price = 899.9M }
                });

            kernel.Bind<IGameRepository>().ToConstant(mock.Object);
            */

            kernel.Bind<IGameRepository>().To<EFGameRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }

        public object GetService(Type serviceType) 
            => kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType)
            => kernel.GetAll(serviceType);
    }
}