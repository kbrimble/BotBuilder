using Autofac;
using KB.InjectionBot.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace KB.InjectionBot {
    public class HelloModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HelloService>().Keyed<IHelloService>(FiberModule.Key_DoNotSerialize).As<IHelloService>();
            builder.RegisterType<RootDialog>().As<IDialog<object>>();
        }
    }
}