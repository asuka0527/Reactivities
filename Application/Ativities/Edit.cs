using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Ativities
{
    public class Edit
    {
        public class Command : IRequest
        {
            // what we want to get from DB
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // Find the actitivy by Id from DB
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                // Instead of this
                // activity.Title = request.Activity.Title ?? activity.Title;
             
                //AutoMapper (Activity from body , Activity from database )   
                _mapper.Map(request.Activity, activity);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}