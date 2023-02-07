using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Domain;

namespace Notes.Application.Common.Mappings;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<Note, NoteLookupDto>();
        CreateMap<Note, NoteVm>();
    }
}