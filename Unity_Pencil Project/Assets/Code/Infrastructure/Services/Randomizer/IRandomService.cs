using Code.Infrastructure.Services;

namespace Code.Infrastructure.States
{
    public interface IRandomService : IService
    {
        int Next(int minValue, int maxValue);
    }
}