using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList;

public record GetNoteListQuery(Guid UserId) : IRequest<NoteListVM>;
