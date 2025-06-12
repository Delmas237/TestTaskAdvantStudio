using Components;
using Config;

namespace UI
{
    public interface IRefreshableItem
    {
        void InitializeUI(BusinessConfig config, BusinessComponent business);
        void UpdateUI(BusinessConfig config, BusinessComponent business);
        void RefreshLevelUI(BusinessConfig config, BusinessComponent business);
        void RefreshUpgradeUI(BusinessConfig config, BusinessComponent business);
        void RefreshIncomeText(BusinessConfig config, BusinessComponent business);
    }
}
