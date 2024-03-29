﻿using MediatR;
using Meetups.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Meetups.Aplication.Meetups.Commands.DeleteCommand
{
    public class DeleteMeetupCommandHandler : IRequestHandler<DeleteMeetupCommand>
    {
        private readonly MeetupsDbContext _dbContext;

        public DeleteMeetupCommandHandler(MeetupsDbContext dbContext) => _dbContext = dbContext;

        //Unit - is a type meaning empty response
        public async Task<Unit> Handle(DeleteMeetupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Meetups.FindAsync(new object[] { request.Id }, cancellationToken);

            _dbContext.Meetups.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
