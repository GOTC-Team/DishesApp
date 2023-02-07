using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Tests.Common;

namespace Notes.Tests.Commands;

public class CreateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateNoteCommandHandler_Success()
    {
        // Arrange
        var handler = new CreateNoteCommandHandler(Context);
        var noteName = "note name";
        var noteDetails = "note details";
        
        // Act
        var noteId = await handler.Handle(new CreateNoteCommand(
            Id : Guid.Empty,
            UserId: NotesContextSeed.UserAId,
            Title: noteName,
            Details: noteDetails
        ), CancellationToken.None);

        // Assert
        Assert.NotNull(
            await Context
            .Notes
            .SingleOrDefaultAsync(x=> noteId == x.Id 
                                      && noteName == x.Title 
                                      && noteDetails == x.Details));
    }
}