using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using System.Configuration;

using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;

namespace SushiStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Mock<ISushiRepository> mock = new Mock<ISushiRepository>();
            mock.Setup(m => m.Sushis).Returns(new List<Sushi> {
                new Sushi { Name = "Filadelfia", Price = 1499 },
                new Sushi { Name = "SimCity", Price = 1299 },
                new Sushi { Name = "Mexico", Price = 1099 }
                });
            kernel.Bind<ISushiRepository>().ToConstant(mock.Object);
        }
    }
}