using Factories;
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
            var balanceFactory = new BalanceFactory(_ecsCore.World);
            balanceFactory.Create(_balanceText);

            var businessFactory = new BusinessFactory(_ecsCore.World);
            for (int i = 0; i < _ecsCore.Data.BusinessesConfig.Businesses.Count; i++)
            {
                var uiItem = Instantiate(_prefab, _content);
                businessFactory.Create(i, uiItem);
            }
        }
    }
}
