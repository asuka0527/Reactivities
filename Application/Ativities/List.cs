using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Ativities
{
    public class List
    {
        //First Mediator
        // we want a query the has a type of List and entity of Activity Class
        public class Query : IRequest<List<Activity>> { }
        // in the handler will pass the query, and return the List of Actitivity
        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            // inject our db context
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            //CancellationToken gives us the ability to cancel a long running request
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                 return await _context.Activities.ToListAsync(cancellationToken);
            }
        }
    }
}