using Components;
using Config;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace Systems
{
    public sealed class UIUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<BalanceComponent> _balanceFilter;
        private readonly EcsFilter<BusinessComponent> _businessFilter;

        private readonly EcsFilter<BusinessComponent, LevelUpEvent> _levelUpFilter;
        private readonly EcsFilter<BusinessComponent, UpgradeEvent> _upgradeFilter;

        private readonly List<BusinessConfig> _configs;

        public UIUpdateSystem(List<BusinessConfig> configs)
        {
            _configs = configs;
        }

        public void Init()
        {
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                var config = _configs[business.Index];
                business.RefreshableItem.InitializeUI(config, business);
            }
        }

        public void Run()
        {
            foreach (var i in _balanceFilter)
            {
                ref var balance = ref _balanceFilter.Get1(i);
                balance.Text.text = $"Баланс: {balance.Value:F1}$";
            }
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);
                var config = _configs[business.Index];
                business.RefreshableItem.UpdateUI(config, business);
            }

            foreach (var i in _levelUpFilter)
            {
                ref var business = ref _levelUpFilter.Get1(i);
                var config = _configs[business.Index];
                business.RefreshableItem.RefreshLevelUI(config, business);
                business.RefreshableItem.RefreshIncomeText(config, business);
            }
            foreach (var i in _upgradeFilter)
            {
                ref var business = ref _upgradeFilter.Get1(i);
                var config = _configs[business.Index];
                business.RefreshableItem.RefreshUpgradeUI(config, business);
                business.RefreshableItem.RefreshIncomeText(config, business);
            }
        }
    }
}
