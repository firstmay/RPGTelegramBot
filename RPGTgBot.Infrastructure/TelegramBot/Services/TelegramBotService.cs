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

            await _botClient.ReceiveAsync(_handlerUpdate.HandleUpdateAsync, _handlerUpdate.HandleErrorAsync,receiverOptions, stoppingToken);
        }
    }
}
