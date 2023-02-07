using MediatR;

namespace Notes.Application.Notes.Commands.CreateNote;

public record class CreateNoteCommand(Guid Id, Guid UserId, string Title, string Details) : IRequest<Guid>;
