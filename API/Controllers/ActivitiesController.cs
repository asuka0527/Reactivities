using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Ativities;

namespace API.Controllers
{
    public class ActivitiesController : BaseAPIController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActitvites()
        {
            // return await _context.Activities.ToListAsync();
            // make a new instance of the List mediator in the Application Activities
            // get Mediator from BaseAPIController
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            // return await _context.Activities.FindAsync(id);
            // need to use the object initializer to we can send the Id when the class Details is instanciated {Id = id}
         return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        //we're not really retruning anything from this  so we use and IActionResult to give access to HTTP response methods such as Ok(), NotFound() and etc.
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return Ok(await Mediator.Send(new Create.Command {Activity = activity}));
        }

        [HttpPut("{id}")]
        // Get from the body the id and Activity
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id=id}));
        }
    }
}