using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandValidator: AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(request =>
            request.Id).NotEqual(Guid.Empty);
        RuleFor(request =>
            request.UserId).NotEqual(Guid.Empty);
        RuleFor(request =>
            request.Title).NotEmpty().MaximumLength(250);
        

    }
}