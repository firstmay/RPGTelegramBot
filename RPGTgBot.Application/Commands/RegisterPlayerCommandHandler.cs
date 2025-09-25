using RPGTgBot.Application.Interfaces;
using RPGTgBot.Domain;
using RPGTgBot.Domain.Entities;

namespace RPGTgBot.Application.Commands
{
    public record class RegisterPlayerCommand(Player Player) : ICommand;

    public class RegisterPlayerCommandHandler(IPlayerRepo playerRepo) : ICommandHandler<RegisterPlayerCommand>
    {
        private readonly IPlayerRepo _PlayerRepo = playerRepo;

        public async Task<Result> HandleAsync(RegisterPlayerCommand command, CancellationToken cancellationToken = default)
        {
            var registrationResult = await _PlayerRepo.AddAsync(command.Player);
            if (!registrationResult.IsSuccess)
            {
                return Result.Fail(registrationResult.Error);
            }
            return Result.Ok();
        }
    }
}
