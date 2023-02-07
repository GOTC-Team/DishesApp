using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVM>
{
    public GetNoteListQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public async Task<NoteListVM> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        var notesOfTheUser = await _context
                                .Notes
                                // .Where(x => x.UserId == request.UserId)
                                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

        return new NoteListVM(notesOfTheUser);
    }
}

