using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Castle.DynamicProxy;
using tmdb.Core.Utilities.Interceptors;
using tmdb.DataAccess.Abstract;
using tmdb.DataAccess.Concrete.EntityFramework;
using tmdb.Service.Abstract;
using tmdb.Service.Concrete;
using tmdb.Service.ExternalServices;
using tmdb.Service.Integrations.GmailSmtp;
using tmdb.Service.Integrations.TheMovieDb;
using tmdb.Service.Integrations.Wikipedia;
using tmdb.Service.Mapping.Profiles;

namespace tmdb.Service.DependencyResolvers.Autofac
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WatchListService>().As<IWatchListService>();
            builder.RegisterType<WatchListDal>().As<IWatchListDal>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<TheMovieDbAdapter>().As<IMovieDbService>();
            builder.RegisterType<WikipediaAdapter>().As<IWikipediaService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppMapperProfile>();
            });

            builder.Register(c => config.CreateMapper()).As<IMapper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
