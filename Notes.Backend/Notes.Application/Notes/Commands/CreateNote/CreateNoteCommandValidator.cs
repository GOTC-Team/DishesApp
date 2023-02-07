using FluentValidation;

namespace Notes.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(request =>
            request.Title).NotEmpty().MaximumLength(250);
        RuleFor(request => 
            request.UserId)
            .NotEqual(Guid.Empty);
    }
}