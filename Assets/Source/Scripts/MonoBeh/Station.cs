using System.Collections.Generic;
using Ecs;
using UnityEngine;
using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;

public class Station : MonoBehaviour, IEntityReady
{
    private EcsWorld _world;
    private EcsEntity _stationEntity;
    
    [SerializeField] private GameObject[] cameras;

    [SerializeField] private GameObject station;
    [SerializeField] private Rigidbody stationRigidbody;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float rotateRollSpeed = 2f;

// [SerializeField] private TMP_Text dockText;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Slider electricitySlider;
    [SerializeField] private TMP_Text materialsText;
    [SerializeField] private TMP_Text stabText;
    [SerializeField] private TMP_Text fuelProductionText;
    [SerializeField] private GameObject statMenu;

    [SerializeField] private float maxFuel;
    [SerializeField] private float currentFuel;
    [SerializeField] private float maxElectricity;
    [SerializeField] private float currentElectricity;
    [SerializeField] private int maxMaterials;
    [SerializeField] private int currentMaterials;

    [SerializeField] private float fuelSpending;
    [SerializeField] private float electricityGenerationRate;
    [SerializeField] private float electicityToProduction;

    [SerializeField] private float transferEnergyRate;
    [SerializeField] private float transferFuelRate;
    [SerializeField] private float transferMaterialRate;
    [SerializeField] private float fuelProductionSpeed;

    [SerializeField] private bool isRestart;
    [SerializeField] private GameObject restartText;

    [SerializeField] private bool isControlledByPlayer;




    private void Start()
    {

        var ecsGameStartup = FindObjectOfType<EcsGameStartup>();
        if (ecsGameStartup != null)
        {
            _world = ecsGameStartup.GetWorld();
        }
        else
        {
            Debug.LogError("EcsGameStartup not found in the scene.");
            return;
        }

        if (_world == null)
        {
            Debug.LogError("EcsWorld is null.");
            return;
        }


        _stationEntity = _world.NewEntity();
        ref var modelComponent = ref _stationEntity.Get<ModelComponent>();
        ref var playerTagComponent = ref _stationEntity.Get<PlayerTagComponent>();
        ref var cameraComponent = ref _stationEntity.Get<CameraComponent>();
        ref var movebleComponent = ref _stationEntity.Get<MovebleComponent>();
        ref var directionComponent = ref _stationEntity.Get<DirectionComponent>();
        ref var stabilizationComponent = ref _stationEntity.Get<StabilizationComponent>();
        ref var resourceComponent = ref _stationEntity.Get<ResourceComponent>();
        ref var transferComponent = ref _stationEntity.Get<TransferComponent>();
        ref var uiComponent = ref _stationEntity.Get<UIComponent>();
        ref var resourceSpendingComponent = ref _stationEntity.Get<ResourceSpendingComponent>();
        ref var restartComponent = ref _stationEntity.Get<RestartComponent>();
        ref var fuelProductionComponent = ref _stationEntity.Get<FuelProductionComponent>();
        // ref var dockingComponent = ref _stationEntity.Get<DockingComponent>();


        modelComponent.modelTransform = station.transform;
        playerTagComponent.IsControlledByPlayer = isControlledByPlayer;
        cameraComponent.cameras = cameras;
        directionComponent.isDocking = false;

        movebleComponent.rigidbody = stationRigidbody;
        movebleComponent.speed = speed;
        movebleComponent.rotateSpeed = rotateSpeed;
        movebleComponent.rotateRollSpeed = rotateRollSpeed;

        stabilizationComponent.isStabization = false;

        uiComponent.fuelSlider = fuelSlider;
        uiComponent.electricitySlider = electricitySlider;
        uiComponent.materialsText = materialsText;
        uiComponent.stabText = stabText;
        uiComponent.statMenu = statMenu;
        
        uiComponent.fuelProductionText = fuelProductionText;

        resourceComponent.maxFuel = maxFuel;
        resourceComponent.currentFuel = currentFuel;
        resourceComponent.maxElectricity = maxElectricity;
        resourceComponent.currentElectricity = currentElectricity;
        resourceComponent.maxMaterials = maxMaterials;
        resourceComponent.currentMaterials = currentMaterials;

        transferComponent.transferEnergyRate = transferEnergyRate;
        transferComponent.transferFuelRate = transferFuelRate;
        transferComponent.transferMaterialRate = transferMaterialRate;

        fuelProductionComponent.fuelProductionSpeed = fuelProductionSpeed;
        fuelProductionComponent.electicityToProduction = electicityToProduction;

        resourceSpendingComponent.fuelSpending = fuelSpending;
        resourceSpendingComponent.electricityGenerationRate = electricityGenerationRate;

        restartComponent.isRestart = isRestart;
        restartComponent.restartText = restartText;

        // Debug.Log("Station entity and components initialized");

        // ecsGameStartup.EntityReady(this);
    }

    public void OnEntityReady()
    {
        // Debug.Log("Station entity is ready.");
    }

    public EcsEntity GetStationEntity()
    {
        return _stationEntity;
    }
}