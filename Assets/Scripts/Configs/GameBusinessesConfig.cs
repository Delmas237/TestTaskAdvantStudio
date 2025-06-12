using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "GameBusinessesConfig", menuName = "Configs/GameBusinesses")]
    public class GameBusinessesConfig : ScriptableObject
    {
        [SerializeField] private List<BusinessConfig> _businesses;

        public List<BusinessConfig> Businesses => _businesses;
    }
}
