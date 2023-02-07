namespace Notes.Application.Notes.Queries.GetNoteList
{
    public record NoteListVM(IList<NoteLookupDto> Notes);
}