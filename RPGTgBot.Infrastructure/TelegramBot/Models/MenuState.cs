using RPGTgBot.Infrastructure.TelegramBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTgBot.Infrastructure.TelegramBot.Models
{
    public class MenuState
    {
        public Type MenuType { get; private set; }
        public IContext? Context { get; private set; }

        public MenuState(Type menuType, IContext? context)
        {
            MenuType = menuType;
            Context = context;
        }

        public MenuState(Type menuType) : this(menuType, null)
        {
        }
    }
}
