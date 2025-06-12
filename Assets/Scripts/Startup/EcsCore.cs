using Components;
using Config;
using Leopotam.Ecs;
using Systems;
using UnityEngine;

public sealed class EcsCore : MonoBehaviour
{
    [SerializeField] private SharedData _data;

    private EcsWorld _world;
    private EcsSystems _systems;

    public SharedData Data => _data;
    public EcsWorld World => _world;
    public EcsSystems Systems => _systems;

    private void Awake()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
    }

    private void Start()
    {
        AddOneFrames();
        AddSystems();

        _systems.Init();
    }

    private void AddOneFrames()
    {
        _systems.OneFrame<UpgradeRequest>();
        _systems.OneFrame<LevelUpRequest>();
        _systems.OneFrame<UpgradeEvent>();
        _systems.OneFrame<LevelUpEvent>();
    }
    private void AddSystems()
    {
        _systems
            .Add(new SaveLoadSystem())
            .Add(new LevelUpSentEventSystem())
            .Add(new UpgradeSendEventSystem())
            .Add(new LevelUpSystem(_data.BusinessesConfig.Businesses))
            .Add(new UpgradeSystem(_data.BusinessesConfig.Businesses))
            .Add(new IncomeSystem(_data.BusinessesConfig.Businesses))
            .Add(new UIUpdateSystem(_data.BusinessesConfig.Businesses));
    }

    private void Update()
    {
        _systems.Run();
    }

    private void OnDestroy()
    {
        if (_systems == null)
            return;

        _systems.Destroy();
        _systems = null;
        _world.Destroy();
        _world = null;
    }
}
