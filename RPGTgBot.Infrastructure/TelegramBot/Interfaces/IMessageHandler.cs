using Telegram.Bot;
using Telegram.Bot.Types;

namespace RPGTgBot.Infrastructure.TelegramBot.Interfaces
{
    public interface IMessageHandler
    {
        Task HandleAsync(ITelegramBotClient client, Message update, CancellationToken token);
    }
}
