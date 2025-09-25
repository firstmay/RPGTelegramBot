using RPGTgBot.Application.Interfaces;
using RPGTgBot.Domain;
using RPGTgBot.Domain.Entities;

namespace RPGTgBot.Application.Commands
{
    public record class RegisterPlayerCommand(long TgId, string Name) : ICommand;

    public class RegisterPlayerCommandHandler(IPlayerRepo playerRepo) : ICommandHandler<RegisterPlayerCommand>
    {
        private readonly IPlayerRepo _PlayerRepo = playerRepo;

        public async Task<Result> HandleAsync(RegisterPlayerCommand command, CancellationToken cancellationToken = default)
        {
            var playerResult = Player.Create(0, command.TgId, command.Name, DateTime.Now);
            if (!playerResult.IsSuccessAndNotNull())
            {
                return Result.Fail(playerResult.Error);
            }

            var registrationResult = await _PlayerRepo.AddAsync(playerResult.Value);
            if (!registrationResult.IsSuccess)
            {
                return Result.Fail(registrationResult.Error);
            }
            return Result.Ok();
        }
    }
}
