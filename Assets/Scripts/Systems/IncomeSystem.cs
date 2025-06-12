using Components;
using Config;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public sealed class IncomeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<BalanceComponent> _balanceFilter;
        private readonly EcsFilter<BusinessComponent> _businessFilter;

        private readonly EcsFilter<BusinessComponent, LevelUpEvent> _levelUpFilter;
        private readonly EcsFilter<BusinessComponent, UpgradeEvent> _upgradeFilter;

        private readonly List<BusinessConfig> _configs;

        public IncomeSystem(List<BusinessConfig> configs)
        {
            _configs = configs;
        }

        public void Init()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                var config = _configs[business.Index];

                UpdateCurrentIncome(ref business, config);
            }
        }

        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                if (business.Level == 0)
                    continue;

                var config = _configs[business.Index];
                business.Progress += Time.deltaTime;

                if (business.Progress >= config.IncomeDelay)
                {
                    UpdateCurrentIncome(ref business, config);

                    ref var balance = ref _balanceFilter.Get1(0);
                    balance.Value += business.CurrentIncome;
                    business.Progress = 0f;
                }
            }

            foreach (var i in _levelUpFilter)
            {
                ref var business = ref _levelUpFilter.Get1(i);
                var config = _configs[business.Index];

                UpdateCurrentIncome(ref business, config);
            }
            foreach (var i in _upgradeFilter)
            {
                ref var business = ref _upgradeFilter.Get1(i);
                var config = _configs[business.Index];

                UpdateCurrentIncome(ref business, config);
            }
        }

        private void UpdateCurrentIncome(ref BusinessComponent business, BusinessConfig config)
        {
            business.CurrentIncome = business.Level * config.BaseIncome;
            float value = 1f;
            for (int j = 0; j < business.UpgradesPurchased.Count; j++)
                value += config.Upgrades[business.UpgradesPurchased[j]].IncomeMultiplier;
            business.CurrentIncome *= value;
        }
    }
}
