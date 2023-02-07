using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteList;

public record struct NoteLookupDto(Guid Id, string Title) : IMapWith<Note>;
