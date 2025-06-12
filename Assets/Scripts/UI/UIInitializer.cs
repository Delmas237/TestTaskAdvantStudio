using Components;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIInitializer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private BusinessItemUI _prefab;
        [SerializeField] private Transform _content;
        [Space(10)]
        [SerializeField] private EcsCore _ecsCore;

        private void Awake()
        {
            var entity = _ecsCore.World.NewEntity();
            ref var balance = ref entity.Get<BalanceComponent>();
            balance.Text = _balanceText;

            for (int i = 0; i < _ecsCore.Data.BusinessesConfig.Businesses.Count; i++)
            {
                var uiItem = Instantiate(_prefab, _content);
                uiItem.Initialize(i, _ecsCore.World);
            }
        }
    }
}
