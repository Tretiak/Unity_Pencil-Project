using Code.Data;
using Code.Infrastructure.Services;

namespace Code.Infrastructure
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}