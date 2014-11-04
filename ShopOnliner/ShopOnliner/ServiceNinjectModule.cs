using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using ShopOnliner.Services;

namespace ShopOnliner
{
    public class ServiceNinjectModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<IService>().To<ItemService>();
        }
    }
}