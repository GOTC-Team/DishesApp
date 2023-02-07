using MediatR;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public record class UpdateNoteCommand(
        Guid UserId, 
        Guid Id, 
        string Title, 
        string Details) : IRequest<Unit>;
}                                  