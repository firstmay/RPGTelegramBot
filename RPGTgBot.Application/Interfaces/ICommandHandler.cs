﻿using RPGTgBot.Domain;

namespace RPGTgBot.Application.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
    
}
