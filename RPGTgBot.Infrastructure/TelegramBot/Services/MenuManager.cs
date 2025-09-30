using Microsoft.Extensions.DependencyInjection;
using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using RPGTgBot.Infrastructure.TelegramBot.Models;
using System.Reflection;
using Telegram.Bot;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    public class MenuManager(
        UserStateService userState,
        IServiceScopeFactory serviceScopeFactory,
        ITelegramBotClient bot
        ) : IMenuManager
    {
        private readonly UserStateService _userState = userState;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly ITelegramBotClient _bot = bot;

        public async Task ReturnToPrvious(long chatId)
        {
            var removedState = _userState.PopState(chatId);
            var previousState = _userState.GetCurrentState(chatId);

            using var scope = _serviceScopeFactory.CreateScope();

            if (previousState.MenuType.GetInterfaces().Contains(typeof(IMenu)))
            {
                IMenu previousMenu = (IMenu)scope.ServiceProvider.GetRequiredService(previousState.MenuType);
                await previousMenu.SendMenuAsync(chatId, _bot);
                return;
            }

            var menuInterface = previousState.MenuType.GetInterfaces().FirstOrDefault(x => x.GetGenericTypeDefinition() == typeof(IMenu<>));
            if (menuInterface != null && menuInterface.GenericTypeArguments.Any(x => x.GetInterfaces().Contains(typeof(IContext))))
            {
                Type contextType = menuInterface.GetGenericArguments()[0];

                object menuInstance = scope.ServiceProvider.GetRequiredService(previousState.MenuType);
                MethodInfo sendMethod = menuInterface.GetMethod("SendMenu", new[] { typeof(long), typeof(ITelegramBotClient), contextType })!;
                await (Task)sendMethod.
                    Invoke(
                        menuInstance,
                        new object[] { chatId, _bot, previousState.Context! })!;
            }
        }

        public async Task SwitchToMenuAsync<TMenu>(long chatId) where TMenu : IMenu
        {
            var currentState = _userState.GetCurrentState(chatId);
            MenuState menuState = new(typeof(TMenu));
            _userState.PushState(chatId, menuState);

            using var scope = _serviceScopeFactory.CreateScope();
            var newMenu = scope.ServiceProvider.GetRequiredService<TMenu>();
            await newMenu.SendMenuAsync(chatId, _bot);
        }

        public async Task SwitchToMenuAsync<TMenu, TContext>(long chatId, TContext context)
            where TMenu : IMenu<TContext>
            where TContext : IContext
        {
            MenuState menuState = new(typeof(TMenu), context);
            _userState.PushState(chatId, menuState);

            using var scope = _serviceScopeFactory.CreateScope();
            var newMenu = scope.ServiceProvider.GetRequiredService<TMenu>();
            Console.WriteLine(chatId + " переключается на " + newMenu);
            await newMenu.SendMenuAsync(chatId, _bot, context);
        }

        public async Task HandleCallback(string callbackData, long chatId)
        {
            MenuState state = _userState.GetCurrentState(chatId);
            if (state.MenuType.GetInterfaces().Contains(typeof(ICallbackHandledMenu)))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                ICallbackHandledMenu menu = (ICallbackHandledMenu)scope.ServiceProvider.GetRequiredService(state.MenuType);
                await menu.HandleCallback(chatId, callbackData, _bot);
            }
        }

        public async Task HandleMessage(string message, long chatId)
        {
            MenuState state = _userState.GetCurrentState(chatId);
            if (state.MenuType.GetInterfaces().Contains(typeof(IMessageHandledMenu)))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                IMessageHandledMenu menu = (IMessageHandledMenu)scope.ServiceProvider.GetRequiredService(state.MenuType);
                await menu.HandleMessage(chatId, message, _bot);
            }
        }
    }
}
