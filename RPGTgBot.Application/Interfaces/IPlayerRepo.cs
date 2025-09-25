using RPGTgBot.Application.DTOs;
using RPGTgBot.Domain;
using RPGTgBot.Domain.Entities;

namespace RPGTgBot.Application.Interfaces
{
    public interface IPlayerRepo
    {
        Task<Result> AddAsync(Player player);
        Task<Result<Player>> GetByIdAsync(long tgId);
    }
}
