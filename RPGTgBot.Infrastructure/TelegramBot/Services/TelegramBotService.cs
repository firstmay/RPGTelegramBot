using Microsoft.Extensions.Hosting;
using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    public class TelegramBotService(ITelegramBotClient botClient, IServiceProvider serviceProvider, IHandlerUpdate handlerUpdate) : BackgroundService
    {
        private readonly ITelegramBotClient _botClient = botClient;
        private readonly IHandlerUpdate _handlerUpdate = handlerUpdate;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("БОТ ЗАПУЩЕН");
            var ctsToken = new CancellationTokenSource().Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            await _botClient.ReceiveAsync(HandleUpdateAsync,HandleErrorAsync,receiverOptions, stoppingToken);
        }


        public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var error = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(error);
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await botClient.SendMessage(update.Message!.Chat.Id, update.Message.Text!);
                    break;
            }
        }
    }
}
