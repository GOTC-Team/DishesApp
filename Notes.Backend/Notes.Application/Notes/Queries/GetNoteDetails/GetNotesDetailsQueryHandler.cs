using AutoMapper;
using MediatR;
using Notes.Application.Common.Excpetions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNotesDetailsQueryHandler : IRequestHandler<GetNotesDetailsQuery, NoteVm>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetNotesDetailsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NoteVm> Handle(GetNotesDetailsQuery request, CancellationToken cancellationToken)
    {
        var noteFromDb = await _context.Notes.FindAsync(request.NoteId);

        if (noteFromDb is null 
            // || noteFromDb.UserId != request.UserId
            )
            throw new NotFoundException(nameof(Note), request.NoteId);

        return _mapper.Map<NoteVm>(noteFromDb);
    }
}