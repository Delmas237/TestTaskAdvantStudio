using Components;
using Leopotam.Ecs;

namespace Systems
{
    public sealed class UpgradeSendEventSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BusinessComponent> _businessFilter;

        public void Run()
        {
            foreach (int i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);

                if (business.UpgradeRequested)
                {
                    ref var entity = ref _businessFilter.GetEntity(i);
                    ref var upgrade = ref entity.Get<UpgradeRequest>();
                    upgrade.UpgradeIndex = business.UpgradeRequestIndex;
                    business.UpgradeRequested = false;
                }
            }
        }
    }
}
