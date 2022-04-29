using Application.Activvities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseController
    {        
        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            var result =  await Mediator.Send(new ListofActivities.Query());
            return HandleResult(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetActivity(Guid id)
        {
          var result = await Mediator.Send(new DetailsofActivity.Query{ Id = id });
          return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {             
            return HandleResult(await Mediator.Send(new CreateActivities.Command { Activity = activity }));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateActivity(Guid Id,Activity activity)
        {
            activity.Id = Id;
            return HandleResult(await Mediator.Send(new EditActivity.Command{ Activity = activity}));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteActivity(Guid Id)
        {
            return HandleResult(await Mediator.Send(new DeleteActivity.Command { Id = Id }));
        }
    }
}
