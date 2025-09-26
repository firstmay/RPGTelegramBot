using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    
    public class HandlerUpdate : IHandlerUpdate
    {
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
