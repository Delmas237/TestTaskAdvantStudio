using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class SharedData
    {
        [SerializeField] private GameBusinessesConfig _businessesConfig;

        public GameBusinessesConfig BusinessesConfig => _businessesConfig;
    }
}
