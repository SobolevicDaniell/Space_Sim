using System;
using System.Collections;
using System.Collections.Generic;
using Ecs;
using UnityEngine;
using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;

public class Soyuz : MonoBehaviour, IEntityReady
{
    private EcsWorld _world;
    private EcsEntity _soyuzEntity;
    
    [SerializeField] public GameObject[] cameras;

    [SerializeField] private GameObject soyuz;
    [SerializeField] private Rigidbody soyuzRigidbody;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float rotateRollSpeed = 2f;

    [SerializeField] private GameObject[] forwardThrusters;
    [SerializeField] private GameObject[] backThrusters;
    [SerializeField] private GameObject[] upThrusters;
    [SerializeField] private GameObject[] downThrusters;
    [SerializeField] private GameObject[] leftThrusters;
    [SerializeField] private GameObject[] rightThrusters;
    [SerializeField] private GameObject[] rollThrustersPositive;
    [SerializeField] private GameObject[] rollThrustersNegative;
    [SerializeField] private GameObject[] pitchThrustersPositive;
    [SerializeField] private GameObject[] pitchThrustersNegative;
    [SerializeField] private GameObject[] yawThrustersPositive;
    [SerializeField] private GameObject[] yawThrustersNegative;

    [SerializeField] private TMP_Text dockText;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Slider electricitySlider;
    [SerializeField] private TMP_Text materialsText;
    [SerializeField] private TMP_Text stabText;
    [SerializeField] private GameObject statMenu;
    [SerializeField] private GameObject otherStatMenu;

    [SerializeField] private float maxFuel;
    [SerializeField] private float currentFuel;
    [SerializeField] private float maxElectricity;
    [SerializeField] private float currentElectricity;
    [SerializeField] private int maxMaterials;
    [SerializeField] private int currentMaterials;
    [SerializeField] private float minElectricityLazer;

    [SerializeField] private float fuelSpending;
    [SerializeField] private float electricityLazerSpending;
    [SerializeField] private float electricityGenerationRate;

    [SerializeField] private float transferEnergyRate;
    [SerializeField] private float transferFuelRate;
    [SerializeField] private float transferMaterialRate;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _lazerStart;
    [SerializeField] private float _laserRange;
    [SerializeField] private float _maxLazerDistance;
    [SerializeField] private GameObject errorText;
    [SerializeField] private Image lazerProgress;

    [SerializeField] private bool isRestart;
    [SerializeField] private GameObject restartText;

    [SerializeField] private bool IsControlledByPlayer;
    [SerializeField] private GameObject transferMenu;

    private void Start()
    {
        // Debug.Log("Soyz: Start");

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

        // Debug.Log("Creating Soyuz entity");


        _soyuzEntity = _world.NewEntity();
        ref var modelComponent = ref _soyuzEntity.Get<ModelComponent>();
        ref var playerTagComponent = ref _soyuzEntity.Get<PlayerTagComponent>();
        ref var cameraComponent = ref _soyuzEntity.Get<CameraComponent>();
        ref var directionComponent = ref _soyuzEntity.Get<DirectionComponent>();
        ref var movebleComponent = ref _soyuzEntity.Get<MovebleComponent>();
        ref var stabilizationComponent = ref _soyuzEntity.Get<StabilizationComponent>();
        ref var uiComponent = ref _soyuzEntity.Get<UIComponent>();
        ref var resourceComponent = ref _soyuzEntity.Get<ResourceComponent>();
        ref var transferComponent = ref _soyuzEntity.Get<TransferComponent>();
        ref var lazerComponent = ref _soyuzEntity.Get<LazerComponent>();
        ref var resourceSpendingComponent = ref _soyuzEntity.Get<ResourceSpendingComponent>();
        ref var restartComponent = ref _soyuzEntity.Get<RestartComponent>();
        ref var dockingComponent = ref _soyuzEntity.Get<DockingComponent>();
        ref var fuelProductionComponent = ref _soyuzEntity.Get<FuelProductionComponent>();


        modelComponent.modelTransform = soyuz.transform;
        playerTagComponent.IsControlledByPlayer = IsControlledByPlayer;
        cameraComponent.cameras = cameras;
        directionComponent.isDocking = false;

        movebleComponent.rigidbody = soyuzRigidbody;
        movebleComponent.speed = speed;
        movebleComponent.rotateSpeed = rotateSpeed;
        movebleComponent.rotateRollSpeed = rotateRollSpeed;


        stabilizationComponent.isStabization = false;
        stabilizationComponent.forward = forwardThrusters;
        stabilizationComponent.back = backThrusters;
        stabilizationComponent.up = upThrusters;
        stabilizationComponent.down = downThrusters;
        stabilizationComponent.left = leftThrusters;
        stabilizationComponent.right = rightThrusters;
        stabilizationComponent.rollT = rollThrustersPositive;
        stabilizationComponent.rollF = rollThrustersNegative;
        stabilizationComponent.pitchT = pitchThrustersPositive;
        stabilizationComponent.pitchF = pitchThrustersNegative;
        stabilizationComponent.yawT = yawThrustersPositive;
        stabilizationComponent.yawF = yawThrustersNegative;

        uiComponent.dockText = dockText;
        uiComponent.fuelSlider = fuelSlider;
        uiComponent.electricitySlider = electricitySlider;
        uiComponent.materialsText = materialsText;
        uiComponent.stabText = stabText;
        uiComponent.statMenu = statMenu;
        uiComponent.otherStatMenu = otherStatMenu;

        resourceComponent.maxFuel = maxFuel;
        resourceComponent.currentFuel = currentFuel;
        resourceComponent.maxElectricity = maxElectricity;
        resourceComponent.currentElectricity = currentElectricity;
        resourceComponent.maxMaterials = maxMaterials;
        resourceComponent.currentMaterials = currentMaterials;
        resourceComponent.minElectricityLazer = minElectricityLazer;

        transferComponent.transferEnergyRate = transferEnergyRate;
        transferComponent.transferFuelRate = transferFuelRate;
        transferComponent.transferMaterialRate = transferMaterialRate;

        lazerComponent._lineRenderer = _lineRenderer;
        lazerComponent._lazerStart = _lazerStart;
        lazerComponent._laserRange = _laserRange;
        lazerComponent._maxLazerDistance = _maxLazerDistance;
        lazerComponent.errorText = errorText;
        lazerComponent.lazerProgress = lazerProgress;

        resourceSpendingComponent.fuelSpending = fuelSpending;
        resourceSpendingComponent.electricityLazerSpending = electricityLazerSpending;
        resourceSpendingComponent.electricityGenerationRate = electricityGenerationRate;

        restartComponent.isRestart = isRestart;
        restartComponent.restartText = restartText;

        dockingComponent.readyToInteract = false;
        dockingComponent.isDocked = false;
        dockingComponent.transferMenu = transferMenu;
        
        

        // Debug.Log("Soyuz entity and components initialized");

        // ecsGameStartup.EntityReady(this);
    }

    public void OnEntityReady()
    {
        Debug.Log("Soyuz  entity is ready.");
    }

    public EcsEntity GetSoyuzEntity()
    {
        return _soyuzEntity;
    }
}