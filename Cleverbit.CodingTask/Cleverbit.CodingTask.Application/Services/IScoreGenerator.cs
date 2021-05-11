using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Application.Services {
    public interface IScoreGenerator {
        Task<uint> GenerateScoreForUser(int userId);
    }
}
