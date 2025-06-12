using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public sealed class SaveLoadSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<BalanceComponent> _balanceFilter;
        private readonly EcsFilter<BusinessComponent> _businessFilter;

        public void Init()
        {
            if (PlayerPrefs.HasKey("balance"))
            {
                foreach (var i in _balanceFilter)
                {
                    ref var balance = ref _balanceFilter.Get1(i);
                    balance.Value = PlayerPrefs.GetFloat("balance");

                    ref var entity = ref _balanceFilter.GetEntity(i);
                    entity.Get<BalanceUpdatedEvent>();
                }
            }
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);

                business.Level = PlayerPrefs.GetInt($"biz_{i}_lvl", business.Level);
                business.Progress = PlayerPrefs.GetFloat($"biz_{i}_prog", business.Progress);

                int upgradesAmount = PlayerPrefs.GetInt($"biz_{i}_ups_amount");
                for (int j = 0; j < upgradesAmount; j++)
                    if (PlayerPrefs.GetInt($"biz_{i}_up{j}") == 1)
                        business.UpgradesPurchased.Add(j);
            }
        }

        public void Destroy()
        {
            foreach (var i in _balanceFilter)
            {
                ref var balance = ref _balanceFilter.Get1(i);
                PlayerPrefs.SetFloat("balance", balance.Value);
            }
            foreach (var i in _businessFilter)
            {
                ref var business = ref _businessFilter.Get1(i);

                PlayerPrefs.SetInt($"biz_{i}_lvl", business.Level);
                PlayerPrefs.SetFloat($"biz_{i}_prog", business.Progress);

                PlayerPrefs.SetInt($"biz_{i}_ups_amount", business.UpgradesPurchased.Count);
                for (int j = 0; j < business.UpgradesPurchased.Count; j++)
                    PlayerPrefs.SetInt($"biz_{i}_up{j}", 1);
            }
            PlayerPrefs.Save();
        }
    }
}
