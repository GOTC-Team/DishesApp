using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Tests.Common;
using Shouldly;

namespace Notes.Tests.Queries;

[Collection(nameof(QueryTestFixture.QueryCollection))]
public class GetNoteListQueryHandlerTest
{
    public GetNoteListQueryHandlerTest(QueryTestFixture fixture) => this._fixture = fixture;
    
    private readonly QueryTestFixture _fixture;
    
    [Fact]
    public async Task GetNoteListQuery_Success()
    {
        var handler = new GetNoteListQueryHandler(_fixture.Context, _fixture.Mapper);

        var result = await handler.Handle(new GetNoteListQuery(
            NotesContextSeed.UserBId),
            CancellationToken.None);

        result.ShouldBeOfType<NoteListVM>();
        result.Notes.Count.ShouldBe(2);
    }
}