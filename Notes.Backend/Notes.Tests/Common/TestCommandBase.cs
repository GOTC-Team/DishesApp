using Notes.Persistence;

namespace Notes.Tests.Common;

public class TestCommandBase : IDisposable
{
    public TestCommandBase()
    {
        Context = NotesContextSeed.Seed();
    }

    protected readonly ApplicationDbContext Context;
    
    public void Dispose()
    {
        NotesContextSeed.Destroy(Context);
    }
}