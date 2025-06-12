using Components;
using Leopotam.Ecs;
using System.Collections.Generic;
using UI;

namespace Factories
{
    public class BusinessFactory
    {
        private readonly EcsWorld _world;

        public BusinessFactory(EcsWorld world)
        {
            _world = world;
        }

        public void Create(int index, BusinessItemUI ui)
        {
            var entity = _world.NewEntity();
            ref var business = ref entity.Get<BusinessComponent>();

            business.Index = index;
            business.Level = index == 0 ? 1 : 0;
            business.UpgradesPurchased = new List<int>();
            business.RefreshableItem = ui;

            ui.Initialize(entity);
        }
    }
}
