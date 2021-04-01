using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Ativities
{
    public class Create
    {
        // Command - do not return any type of data unlike QUERY that does
        public class Command : IRequest
        {
            // the parameter we want to receive from our API
            public Activity Activity { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // we are not touching the database at this point (only adding it to memory) so we do not need to make it and AddAsync()
                _context.Activities.Add(request.Activity); 

                await _context.SaveChangesAsync();

                // does not reutrn anything just leeting our API controller know that we are DONE HERE!
                return Unit.Value;
            }
        }
    }
}