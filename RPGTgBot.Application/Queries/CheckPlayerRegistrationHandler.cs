using RPGTgBot.Application.Interfaces;
using RPGTgBot.Domain;

namespace RPGTgBot.Application.Queries
{
    public record CheckPlayerRegistration(long userId) : IQuery<Result>;
    public class CheckPlayerRegistrationHandler(
        IPlayerRepo playerRepo
        ) : IQueryHandler<CheckPlayerRegistration, Result>
    {
        private readonly IPlayerRepo _playerRepo = playerRepo;

        public async Task<Result> HandleAsync(CheckPlayerRegistration query, CancellationToken cancellationToken = default)
        {
            var checkRegistrationResult = await _playerRepo.GetByIdAsync(query.userId);
            if (!checkRegistrationResult.IsSuccess)
            {
                return Result.Fail(checkRegistrationResult.Error);
            }
            return Result.Ok();
        }
    }
}
