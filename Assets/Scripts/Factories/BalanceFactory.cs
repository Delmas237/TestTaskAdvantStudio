using Components;
using Leopotam.Ecs;
using TMPro;

namespace Factories
{
    public class BalanceFactory
    {
        private readonly EcsWorld _world;

        public BalanceFactory(EcsWorld world)
        {
            _world = world;
        }

        public void Create(TextMeshProUGUI balanceText)
        {
            var entity = _world.NewEntity();
            ref var balance = ref entity.Get<BalanceComponent>();
            balance.Text = balanceText;
        }
    }
}
