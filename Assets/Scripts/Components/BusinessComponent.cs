using System.Collections.Generic;
using UI;

namespace Components
{
    public struct BusinessComponent
    {
        public int Index;
        public int Level;

        public float Progress;
        public float CurrentIncome;

        public bool LevelUpRequested;

        public bool UpgradeRequested;
        public int UpgradeRequestIndex;

        public List<int> UpgradesPurchased;
        public IRefreshableItem RefreshableItem;
    }
}
