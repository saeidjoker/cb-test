using FluentValidation;

namespace Cleverbit.CodingTask.Application.DTO.Authentication {

    public class SignInInput {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SignInValidator : AbstractValidator<SignInInput> {
        public SignInValidator() {
            RuleFor(a => a.UserName).NotEmpty().MaximumLength(255);
            RuleFor(a => a.Password).NotEmpty().MaximumLength(255);
        }
    }
}
