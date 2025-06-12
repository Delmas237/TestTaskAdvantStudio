using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "BusinessConfig", menuName = "Configs/Business")]
    public class BusinessConfig : ScriptableObject
    {
        [SerializeField] private string _businessName;
        [SerializeField] private float _incomeDelay;
        [SerializeField] private float _basePrice;
        [SerializeField] private float _baseIncome;

        [SerializeField] private List<Upgrade> _upgrades;

        public string BusinessName => _businessName;
        public float IncomeDelay => _incomeDelay;
        public float BasePrice => _basePrice;
        public float BaseIncome => _baseIncome;
        public List<Upgrade> Upgrades => _upgrades;

        [Serializable]
        public struct Upgrade
        {
            [SerializeField] private string _name;
            [SerializeField] private float _price;
            [SerializeField] private float _incomeMultiplier;

            public string Name => _name;
            public float Price => _price;
            public float IncomeMultiplier => _incomeMultiplier;
        }
    }
}
