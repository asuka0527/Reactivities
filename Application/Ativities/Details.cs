using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Ativities
{
    public class Details
    {
        // bring in the Id through the  QUERY parameter 
        // use the Result type Activity
        public class Query : IRequest<Result<Activity>>
        {
            public Guid Id { get; set; }
        }

        // pass in the Query to the handler and return an activity
        public class Handler : IRequestHandler<Query, Result<Activity>>
        {
            // inject the Dbcontext to the Handler
            private readonly DataContext _context;
            public Handler(DataContext context) { 
                _context = context;
            }

            // implement the IRequestHandler async method that will use the request.Id from the Query
            public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
            
                var activity = await _context.Activities.FindAsync(request.Id);

                // check in the controller what value of activity if we get
                return Result<Activity>.Success(activity);
            }
        }
    }
}