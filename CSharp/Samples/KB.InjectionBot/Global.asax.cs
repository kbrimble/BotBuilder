using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Bot.Builder.Dialogs;

namespace KB.InjectionBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            void UpdateContainer(ContainerBuilder builder)
            {
                builder.RegisterModule<HelloModule>();
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            }

            Conversation.UpdateContainer(UpdateContainer);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
