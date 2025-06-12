using Components;
using Config;
using Leopotam.Ecs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BusinessItemUI : MonoBehaviour, IRefreshableItem
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _incomeText;
        [Space(10)]
        [SerializeField] private Image _progressBar;
        [Space(10)]
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private List<Button> _upgradeButtons;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _levelUpText;
        [SerializeField] private List<TextMeshProUGUI> _upgradeTexts;

        public void Initialize(EcsEntity entity)
        {
            _levelUpButton.onClick.AddListener(() => 
            {
                ref var business = ref entity.Get<BusinessComponent>();
                business.LevelUpRequested = true; 
            });

            for (int i = 0; i < _upgradeButtons.Count; i++)
            {
                int buffer = i;
                _upgradeButtons[i].onClick.AddListener(() => RequestUpgrade(entity, buffer));
            }
        }
        private void RequestUpgrade(EcsEntity entity, int upgradeIndex)
        {
            ref var business = ref entity.Get<BusinessComponent>();
            business.UpgradeRequested = true;
            business.UpgradeRequestIndex = upgradeIndex;
        }

        public void InitializeUI(BusinessConfig config, BusinessComponent business)
        {
            _nameText.text = config.BusinessName;

            RefreshLevelUI(config, business);
            RefreshUpgradeUI(config, business);
            RefreshIncomeText(config, business);
        }

        public void RefreshLevelUI(BusinessConfig config, BusinessComponent business)
        {
            _levelText.text = $"LVL\n{business.Level}";
            _levelUpText.text = $"LVL UP\n {(business.Level + 1) * config.BasePrice}$";
        }

        public void RefreshUpgradeUI(BusinessConfig config, BusinessComponent business)
        {
            for (int i = 0; i < _upgradeButtons.Count; i++)
                _upgradeButtons[i].interactable = !business.UpgradesPurchased.Contains(i);

            for (int j = 0; j < _upgradeTexts.Count; j++)
                _upgradeTexts[j].text = business.UpgradesPurchased.Contains(j) ? "Куплено" : $"{config.Upgrades[j].Name}\n{config.Upgrades[j].Price}$";
        }

        public void RefreshIncomeText(BusinessConfig config, BusinessComponent business)
        {
            _incomeText.text = $"Доход\n {business.CurrentIncome:F1}$";
        }

        public void UpdateUI(BusinessConfig config, BusinessComponent business)
        {
            _progressBar.fillAmount = business.Progress / config.IncomeDelay;
        }
    }
}
