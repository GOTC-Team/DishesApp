using Notes.Application.Common.Excpetions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Common;

namespace Notes.Tests.Commands;

public class UpdateNoteCommandHandlerTest : TestCommandBase
{
    public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
    {
        // arrange
        var handler = new UpdateNoteCommandHandler(Context);
        
        //act 
        //assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(new UpdateNoteCommand(
                Id : NotesContextSeed.NoteIdForUpdate,
                UserId : NotesContextSeed.UserAId,
                Title: "title update note",
                Details: "details update note"), CancellationToken.None);
        });
    }
}