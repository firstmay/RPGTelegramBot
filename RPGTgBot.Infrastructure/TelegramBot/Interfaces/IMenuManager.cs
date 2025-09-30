namespace RPGTgBot.Infrastructure.TelegramBot.Interfaces
{
    public interface IMenuManager
    {
        Task SwitchToMenuAsync<TMenu>(long chatId) where TMenu : IMenu;
        Task SwitchToMenuAsync<TMenu, TContext>(long chatId, TContext context) where TMenu : IMenu<TContext> where TContext : IContext;
        Task ReturnToPrvious(long chatId);
        Task HandleCallback(string callbackData, long userId);
        Task HandleMessage(string message, long userId);
    }
}
