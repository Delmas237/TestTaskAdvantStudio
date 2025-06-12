using Components;
using Config;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Systems
{
    public sealed class LevelUpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BalanceComponent> _balanceFilter;
        private readonly EcsFilter<BusinessComponent, LevelUpRequest> _levelUpRequestFilter;

        private readonly List<BusinessConfig> _configs;

        public LevelUpSystem(List<BusinessConfig> configs)
        {
            _configs = configs;
        }

        public void Run()
        {
            foreach (var i in _levelUpRequestFilter)
            {
                ref var business = ref _levelUpRequestFilter.Get1(i);
                ref var balance = ref _balanceFilter.Get1(0);

                var config = _configs[business.Index];
                float price = (business.Level + 1) * config.BasePrice;

                if (balance.Value >= price)
                {
                    balance.Value -= price;
                    business.Level++;

                    ref var entity = ref _levelUpRequestFilter.GetEntity(i);
                    entity.Get<LevelUpEvent>();
                }
            }
        }
    }
}
