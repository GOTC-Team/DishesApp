using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public record class GetNotesDetailsQuery(
    Guid UserId,
    Guid NoteId) : IRequest<NoteVm>;