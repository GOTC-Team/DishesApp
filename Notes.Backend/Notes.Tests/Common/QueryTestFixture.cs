using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Persistence;

namespace Notes.Tests.Common;

public class QueryTestFixture: IDisposable
{
    public QueryTestFixture()
    {
        Context = NotesContextSeed.Seed();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ApplicationMappings());
        });
        Mapper = configurationProvider.CreateMapper();
    }
    
    public ApplicationDbContext Context;
    public IMapper Mapper;

    public void Dispose()
    {
        NotesContextSeed.Destroy(Context);
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}