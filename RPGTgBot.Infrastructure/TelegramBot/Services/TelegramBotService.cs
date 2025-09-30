using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    public class TelegramBotService(ITelegramBotClient botClient, IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory) : BackgroundService
    {
        private readonly ITelegramBotClient _botClient = botClient;
        private readonly IServiceScopeFactory _serviceScopeFactory = scopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("БОТ ЗАПУЩЕН");
            var ctsToken = new CancellationTokenSource().Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            using var scope = _serviceScopeFactory.CreateScope();
            var updateHandler = scope.ServiceProvider.GetRequiredService<IHandlerUpdate>();

            await _botClient.ReceiveAsync(updateHandler.HandleUpdateAsync, updateHandler.HandleErrorAsync,receiverOptions, stoppingToken);
        }
    }
}
