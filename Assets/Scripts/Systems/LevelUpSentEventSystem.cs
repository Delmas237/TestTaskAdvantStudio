using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class LevelUpSentEventSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BusinessComponent> _businessFilter;

        public void Run()
        {
            foreach (int i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);

                if (business.LevelUpRequested)
                {
                    ref var entity = ref _businessFilter.GetEntity(i);
                    entity.Get<LevelUpRequest>();
                    business.LevelUpRequested = false;
                }
            }
        }
    }
}
