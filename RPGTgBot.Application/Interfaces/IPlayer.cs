using RPGTgBot.Application.DTOs;
using RPGTgBot.Domain;

namespace RPGTgBot.Application.Interfaces
{
    public interface IPlayer
    {
        Task<Result<PlayerProfileDto>> GetProfileAsync();
    }
}
