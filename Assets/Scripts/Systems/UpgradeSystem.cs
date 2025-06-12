using Leopotam.Ecs;
using Components;
using Config;
using System.Collections.Generic;

namespace Systems
{
    public sealed class UpgradeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BalanceComponent> _balanceFilter;
        private readonly EcsFilter<BusinessComponent, UpgradeRequest> _upgradeRequestFilter;

        private readonly List<BusinessConfig> _configs;

        public UpgradeSystem(List<BusinessConfig> configs)
        {
            _configs = configs;
        }

        public void Run()
        {
            foreach (var i in _upgradeRequestFilter)
            {
                ref var business = ref _upgradeRequestFilter.Get1(i);
                ref var request = ref _upgradeRequestFilter.Get2(i);
                ref var balance = ref _balanceFilter.Get1(0);

                var config = _configs[business.Index];
                float price = config.Upgrades[request.UpgradeIndex].Price;

                if (balance.Value >= price)
                {
                    balance.Value -= price;
                    business.UpgradesPurchased.Add(request.UpgradeIndex);

                    ref var entity = ref _upgradeRequestFilter.GetEntity(i);
                    entity.Get<UpgradeEvent>();
                }
            }
        }
    }
}
