using Leopotam.Ecs;

namespace SA.Tanks.Extensions.UnityComponents
{
    public interface IEcsUnityProvider
    {
        ref EcsEntity Entity { get; }

        void SetEntity(in EcsEntity entity);
    }
}