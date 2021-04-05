using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Ativities
{
    public class Create
    {
        // Command - do not return any type of data unlike QUERY that does
        // but we will return WHETHER the COMMAND is SUCCESSFUL OR NOT
        public class Command : IRequest<Result<Unit>>
        {
            // the parameter we want to receive from our API
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

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // we are not touching the database at this point (only adding it to memory) so we do not need to make it and AddAsync()
                _context.Activities.Add(request.Activity); 

                // SaveChangesAsync return an int
                // 0 =false, 
                var result =await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failed to create activity");
                // does not reutrn anything just leeting our API controller know that we are DONE HERE!
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}