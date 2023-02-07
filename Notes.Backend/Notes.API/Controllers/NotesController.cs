using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;

namespace Notes.API.Controllers;
[ApiVersion("2.0")]
public class NotesController : BaseController
{
    /// <summary>
    /// Gets the list of notes
    /// </summary>
    /// <remarks>
    /// Sample of request:
    /// GET /note
    /// </remarks>
    /// <returns>NoteListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    //[Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListVM>> GetAll()
    {
        var query = new GetNoteListQuery(UserId);
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
    
    /// <summary>
    /// Get the not by id
    /// </summary>
    /// <remarks>
    /// Sample of request:
    /// GET /note/54CA6854-C153-403A-A498-380D1B7FFFEC
    /// </remarks>
    /// <param name="id">Note id (guid)</param>
    /// <returns>Return NoteDetailsVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet("{id}")]
    // [Authorize]
    [ActionName(nameof(Get))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteVm>> Get(Guid id)
    {
        var query = new GetNotesDetailsQuery(UserId, id);
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Update a note
    /// </summary>
    /// <remarks>
    /// Sample of request:
    /// PUT /note
    /// {
    ///     title: "updated note title"
    /// }
    /// </remarks>
    /// <param name="request">UpdateNoteCommand object</param>
    /// <returns>Returns No Content</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPut]
    // [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Put(UpdateNoteCommand request)
    {
        var _ = await Mediator.Send(request);
        return NoContent();
    }

    /// <summary>
    /// Create a note
    /// </summary>
    /// <remarks>
    /// Sample of request:
    /// POST /note
    /// {
    ///     title: "note title"
    ///     details: "note details"
    /// }
    /// </remarks>
    /// <param name="request">CreateNoteDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPost]
    // [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Post(CreateNoteCommand request)
    {
        var createdNoteId = await Mediator.Send(request);
        var hello = CreatedAtAction(actionName: nameof(Get), routeValues: new { id = createdNoteId }, value: null);
        return hello;
    }

    /// <summary>
    /// Delete a note
    /// </summary>
    /// <remarks>
    /// Sample of request:
    /// DELETE /note/54CA6854-C153-403A-A498-380D1B7FFFEC
    /// {
    ///     noteid: "E7D47E24-2AD3-479A-8829-2660C385A2B1",
    ///     userid: "F0D764D8-10D4-4B6B-B338-C3D267E7EA11"
    /// }
    /// </remarks>
    /// <param name="request">DeleteNoteCommand dto-object</param>
    /// <returns>No Content</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpDelete]
    // [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(DeleteNoteCommand request)
    {
        var _ = await Mediator.Send(request);
        return NoContent();
    }
}
