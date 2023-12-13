using Application.DTOs.Users.HTTP;
using Application.Handlers.Tables.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Authorize(Policy = "SellerOnly")]
    public class EventsController : BaseApiController
    {
        private readonly IEventHandler _eventHandler;

        public EventsController(IEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }

        [HttpGet]
        [Route("MyEvents")]
        public async Task<IActionResult> GetSellerEvents()//[FromQuery] RequestDto request)
        {
            return HandleResult(await _eventHandler.GetListAsync());
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetEvent(Guid id)
        //{
        //    return HandleResult(await Mediator.Send(new Details.Query(id)));
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateEvent(NotebookDto notebook)
        //{
        //    return HandleResult(await Mediator.Send(new Create.Command(notebook)));
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditEvent(Guid id, NotebookDto notebook)
        //{
        //    notebook.Id = id;
        //    return HandleResult(await Mediator.Send(new Edit.Command(notebook)));
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEvent(Guid id)
        //{
        //    return HandleResult(await Mediator.Send(new Delete.Command(id)));
        //}

        //[HttpGet]
        //[Route("UserActionsStat")]
        //public async Task<IActionResult> GetUserCreatActionsStat()
        //{
        //    return HandleResult(await Mediator.Send(new UserCreateActionStatistic.Query()));
        //}
    }
}
