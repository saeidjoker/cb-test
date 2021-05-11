using System.Threading.Tasks;
using Cleverbit.CodingTask.Application.DTO.Authentication;

namespace Cleverbit.CodingTask.Application.Services {
    public interface IAuthenticationService {
        Task SignIn(SignInInput input);
    }
}
