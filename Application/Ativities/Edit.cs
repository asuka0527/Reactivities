using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Ativities
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            // what we want to get from DB
            public Activity Activity { get; set; }
        }

        // VALIDATION
        public class CommandValidator : AbstractValidator<Command>
        {
            // Constructor - specify the rules 
            public CommandValidator()
            {
                // x is the Activity
                // use the class Validator 
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }
        public class Handler : IRequestHandler<Command , Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // Find the actitivy by Id from DB
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                if(activity == null) return null;

                // Instead of this
                // activity.Title = request.Activity.Title ?? activity.Title;
             
                //AutoMapper (Activity from body , Activity from database )   
                _mapper.Map(request.Activity, activity);

                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failed  to update activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}