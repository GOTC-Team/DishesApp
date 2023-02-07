using Notes.Application.Common.Excpetions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Tests.Common;

namespace Notes.Tests.Commands;

public class DeleteNoteCommandHandlerTest: TestCommandBase
{
    [Fact]
    public async Task DeleteNoteCommand_Success()
    {
        // Arrange
        var handler = new DeleteCommandHandler(Context);
        
        // Act
        await handler.Handle(new DeleteNoteCommand(
            NotesContextSeed.NoteIdForDelete,
            NotesContextSeed.UserAId), 
            CancellationToken.None);

        // Assert
        Assert.Null(Context.Notes.SingleOrDefault(x=> 
            x.Id == NotesContextSeed.NoteIdForDelete));
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new DeleteCommandHandler(Context);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(new DeleteNoteCommand(
                NoteId: Guid.NewGuid(),
                UserId: NotesContextSeed.UserAId), 
                CancellationToken.None);
        });
    }

    [Fact]
    public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
    {
        // Arrange
        var deleteHandler = new DeleteCommandHandler(Context);
        var createHandler = new CreateNoteCommandHandler(Context);
        var noteId = await createHandler.Handle(
            new CreateNoteCommand(
                Title: "NoteTitle",
                UserId: NotesContextSeed.UserAId, 
                Details: "Note Details",
                Id: Guid.Empty), 
                CancellationToken.None);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await deleteHandler.Handle(
                new DeleteNoteCommand(
                    NoteId: noteId,
                    UserId: NotesContextSeed.UserBId), 
                CancellationToken.None);
        });
    }

    
    [Fact]
    public void Temp()
    {
        // Arrange

        // Act
        
        // Assert
    }
}