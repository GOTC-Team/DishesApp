using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Tests.Common;
using Shouldly;

namespace Notes.Tests.Queries;

[Collection(nameof(QueryTestFixture.QueryCollection))]
public class GetNoteDetailsQueryHandlerTest
{
    public GetNoteDetailsQueryHandlerTest(QueryTestFixture fixture) => _fixture = fixture;
    private readonly QueryTestFixture _fixture;

    [Fact]
    public async Task GetNoteDetailsQueryHandler_Success()
    {
        // arrange
        var handler = new GetNotesDetailsQueryHandler(_fixture.Context, _fixture.Mapper);
        
        // act
        var result = await handler.Handle(
            new GetNotesDetailsQuery(
                UserId: NotesContextSeed.UserBId,
                NoteId: Guid.Parse("{FBB0E168-BE1E-4CEE-A553-4526DFEAB784}")), 
            CancellationToken.None);
        
        // assert
        result
            .ShouldBeOfType<NoteVm>();
        result.Title
            .ShouldBe("Title2");
        result.CreationDate.ToLocalTime()
            .ShouldBe(DateTime.Today);
    }
}