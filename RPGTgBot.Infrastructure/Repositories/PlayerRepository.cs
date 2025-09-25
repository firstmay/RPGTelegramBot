using Microsoft.EntityFrameworkCore;
using RPGTgBot.Application.Interfaces;
using RPGTgBot.Domain;
using RPGTgBot.Domain.Entities;
using RPGTgBot.Infrastructure.DataBaseContext;
using RPGTgBot.Infrastructure.Entities;

namespace RPGTgBot.Infrastructure.Repositories
{
    public class PlayerRepository(RPGDbContext context) : IPlayerRepo
    {
        private readonly RPGDbContext db = context;

        public async Task<Result> AddAsync(Player player)
        {
            User user = User.ConvertFrom(player);
            
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            
            return Result.Ok();
        }

        public async Task<Result<Player>> GetByIdAsync(long tgId)
        {
            var userResult = await db.Users.FirstOrDefaultAsync(u => u.TelegramId == tgId);
            if (userResult == null)
            {
                return Result<Player>.Fail("Пользователь не найден");
            }
            
            var convertedUser = userResult.ConvertToPlayer();
            if (!convertedUser.IsSuccessAndNotNull())
            {
                return Result<Player>.Fail(convertedUser.Error);
            }
            
            return Result<Player>.Ok(convertedUser.Value);
        }
    }
}
