using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;

namespace Notes.Tests.Common;


public static class NotesContextSeed
{
    public static readonly Guid UserAId = Guid.NewGuid();
    public static readonly Guid UserBId = Guid.NewGuid();
    
    public static readonly Guid NoteIdForDelete = Guid.NewGuid();
    public static readonly Guid NoteIdForUpdate = Guid.NewGuid();

    public static ApplicationDbContext Seed()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()); //MS.EFCore.InMemory
        var context = new ApplicationDbContext(optionsBuilder.Options);
        context.Database.EnsureCreated();
        context.Notes.AddRange(
            new Note
            {
                CreationDate = DateTime.Today.ToUniversalTime(),
                Details = "Details1",
                EditDate = null,
                Id = Guid.Parse("{D780CD04-AA75-4DFD-8B50-984C7BE07777}"),
                Title = "Title1",
                UserId = UserAId
            },
            new Note
            {
                CreationDate = DateTime.Today.ToUniversalTime(),
                Details = "Details2",
                EditDate = null,
                Id = Guid.Parse("{FBB0E168-BE1E-4CEE-A553-4526DFEAB784}"),
                Title = "Title2",
                UserId = UserBId
            },
            new Note
            {
                CreationDate = DateTime.Today.ToUniversalTime(),
                Details = "Details3",
                EditDate = null,
                Id = NoteIdForDelete,
                Title = "Title3",
                UserId = UserAId
            },
            new Note
            {
                CreationDate = DateTime.Today.ToUniversalTime(),
                Details = "Details4",
                EditDate = null,
                Id = NoteIdForUpdate,
                Title = "Title4",
                UserId = UserBId
            }
        );
        context.SaveChanges();
        return context;
    }


    public static void Destroy(ApplicationDbContext dbContext)
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }
}