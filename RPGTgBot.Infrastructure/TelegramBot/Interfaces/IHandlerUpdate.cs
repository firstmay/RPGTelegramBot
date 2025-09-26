using Telegram.Bot;
using Telegram.Bot.Types;

namespace RPGTgBot.Infrastructure.TelegramBot.Interfaces
{
    public interface IHandlerUpdate
    {
        Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token);
        Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token);
    }
}
