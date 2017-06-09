using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace KB.InjectionBot.Dialogs {
    [Serializable]
    public class SecondDialog : IDialog<DialogResult>
    {
        private readonly IHelloService _helloService;

        public SecondDialog(IHelloService helloService)
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
            await result;

            // return our reply to the user
            await context.PostAsync($"{_helloService.GetMessage()} This is the second dialog");

            context.Done(DialogResult.Good);
        }
    }
}