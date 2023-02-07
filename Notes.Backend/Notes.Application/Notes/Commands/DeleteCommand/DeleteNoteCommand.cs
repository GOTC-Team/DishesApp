using MediatR;

namespace Notes.Application.Notes.Commands.DeleteCommand;

public record DeleteNoteCommand(Guid NoteId, Guid UserId)
    : IRequest<Unit>;
