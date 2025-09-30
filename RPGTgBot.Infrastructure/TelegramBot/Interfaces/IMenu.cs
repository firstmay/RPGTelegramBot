using Telegram.Bot;

namespace RPGTgBot.Infrastructure.TelegramBot.Interfaces
{
    public interface IMenu
    {
        Task SendMenuAsync(long chatId, ITelegramBotClient bot);
    }
    public interface IMenu<TContext> where TContext : IContext
    {
        Task SendMenuAsync(long chatId, ITelegramBotClient bot, TContext context);
    }
}
