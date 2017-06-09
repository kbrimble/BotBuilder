using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace KB.InjectionBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private readonly IHelloService _helloService;

        public RootDialog(IHelloService helloService)
        {
            _helloService = helloService;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"{_helloService.GetMessage()}. You sent {activity.Text} which was {length} characters");

            await context.Forward(new SecondDialog(_helloService), Resume, new Activity(text: "seconds pls"));
        }

        private async Task Resume(IDialogContext context, IAwaitable<DialogResult> result)
        {
            var dr = await result;
            await context.PostAsync($"Got result {dr}");
            context.Wait(MessageReceivedAsync);
        }
    }
}