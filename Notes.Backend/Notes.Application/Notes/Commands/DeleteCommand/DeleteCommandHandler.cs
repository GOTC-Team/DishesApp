using MediatR;
using Notes.Application.Common.Excpetions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteCommand;

public class DeleteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly IApplicationContext _context;

    public DeleteCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Notes
            .FindAsync(request.NoteId);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Note), request.NoteId);
        }

        _context.Notes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
