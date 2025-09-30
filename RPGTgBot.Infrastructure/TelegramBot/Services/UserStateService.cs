using RPGTgBot.Infrastructure.TelegramBot.Menu;
using RPGTgBot.Infrastructure.TelegramBot.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTgBot.Infrastructure.TelegramBot.Services
{
    public class UserStateService
    {
        private ConcurrentDictionary<long, Stack<MenuState>> _userStates = new();
        private readonly Type _defailtTypeMenuState = typeof(MainMenu);
        public void PushState(long chatId, MenuState menuState)
        {
            var stack = _userStates.GetOrAdd(chatId, _ => new Stack<MenuState>());
            stack.Push(menuState);
        }

        public MenuState PopState(long chatId)
        {
            if(_userStates.TryGetValue(chatId,out var stack) && _userStates.Count > 0)
            {
                return stack.Pop();
            }
            return new MenuState(_defailtTypeMenuState);
        }

        public MenuState GetCurrentState(long chatId)
        {
            if(_userStates.TryGetValue(chatId,out var stack) && stack.Count > 0)
            {
                return stack.Peek();
            }
            return new MenuState(_defailtTypeMenuState);
        }

        public void Clear(long chatId)
        {
            if(_userStates.TryGetValue(chatId, out var stack) && stack.Count > 0)
            {
                stack.Clear();
            }
        }
    }
}
