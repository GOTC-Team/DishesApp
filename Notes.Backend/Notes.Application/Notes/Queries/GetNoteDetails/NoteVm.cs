using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public record struct NoteVm(
        Guid Id,
        string Title,
        string Details,
        DateTime CreationDate,
        DateTime? EditDate) : IMapWith<Note>;