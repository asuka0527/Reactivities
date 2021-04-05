using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Ativities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class ActivitiesController : BaseAPIController
    {

        [HttpGet]
        public async Task<IActionResult> GetActitvites()
        {
            // return await _context.Activities.ToListAsync();
            // make a new instance of the List mediator in the Application Activities
            // get Mediator from BaseAPIController
            var result = await Mediator.Send(new List.Query());
            return HandleResult(result);
        }


        // [Authorize]
        [HttpGet("{id}")]
        //IActionResult lets us return HTTP response instead of TYPE of thing
        public async Task<IActionResult> GetActivity(Guid id)
        {
            // return await _context.Activities.FindAsync(id);
            // need to use the object initializer to we can send the Id when the class Details is instanciated {Id = id}
         var result = await Mediator.Send(new Details.Query{Id = id});

        // pass the result to the Error handling logic of baseController
        return HandleResult(result);
        }

        [HttpPost]
        //we're not really retruning anything from this  so we use and IActionResult to give access to HTTP response methods such as Ok(), NotFound() and etc.
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            var result =await Mediator.Send(new Create.Command {Activity = activity});

            return HandleResult(result);

        }

        [HttpPut("{id}")]
        // Get from the body the id and Activity
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command{Activity = activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {

            var result =
            await Mediator.Send(new Delete.Command{Id=id});

            return HandleResult(result);
        }
    }
}