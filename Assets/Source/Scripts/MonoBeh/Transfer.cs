using System;
using Ecs;
using UnityEngine;
using Leopotam.Ecs;

public class Transfer : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private EcsFilter<ResourceComponent> _resourceFilter;

    private EcsEntity soyuzEntity;
    private EcsEntity stationEntity;

    private void Start()
    {
        var ecsGameStartup = FindObjectOfType<EcsGameStartup>();
        if (ecsGameStartup != null)
        {
            _world = ecsGameStartup.GetWorld();
            _systems = ecsGameStartup.GetSystems();
        }
        else
        {
            Debug.LogError("EcsGameStartup not found in the scene.");
            return;
        }

        // _resourceFilter = _world.GetFilter<EcsFilter<ResourceComponent>>();

        // soyuzEntity = _resourceFilter.GetEntity(0);
        var soyuzScript = FindObjectOfType<Soyuz>();
        var stationScript = FindObjectOfType<Station>();
        soyuzEntity = soyuzScript.GetSoyuzEntity();
        stationEntity = stationScript.GetStationEntity();
        // stationEntity = _resourceFilter.GetEntity(1);

        

        // if (_resourceFilter.GetEntitiesCount() < 2)
        // {
        //     Debug.LogError("Not enough entities with ResourceComponent found.");
        //     return;
        // }
        //
        // soyuzEntity = _resourceFilter.GetEntity(0);
        // stationEntity = _resourceFilter.GetEntity(1);

        UpdateUI();
    }


    private void Update()
    {
        // Debug.Log(soyuzEntity.Get<ResourceComponent>().currentFuel); 
        // Debug.Log(stationEntity.Get<ResourceComponent>().currentFuel); 
    }

    public void TransferFuelButtonToSoyuz()
    {
        ref var Soyuz = ref soyuzEntity.Get<ResourceComponent>();
        ref var Station = ref stationEntity.Get<ResourceComponent>();

        if (Station.currentFuel > 0)
        {
            float soyuzNeed = Soyuz.maxFuel - Soyuz.currentFuel;
            float fuelToTransfer = Mathf.Min(Station.currentFuel, soyuzNeed);
        
            stationEntity.Get<ResourceComponent>().currentFuel -= fuelToTransfer;
            soyuzEntity.Get<ResourceComponent>().currentFuel += fuelToTransfer;

            Debug.Log($"Transferred {fuelToTransfer} units of fuel to Soyuz. Soyuz fuel: {Soyuz.currentFuel}, Station fuel: {Station.currentFuel}");

            // Обновление UI после обновления значений топлива
            UpdateUI();
        }
    }


    public void TransferElectricityButtonToSoyuz()
    {
        ref var Soyuz = ref soyuzEntity.Get<ResourceComponent>();
        ref var Station = ref stationEntity.Get<ResourceComponent>();

        if (Station.currentElectricity > 0)
        {
            float soyuzNeed = Soyuz.maxElectricity - Soyuz.currentElectricity;
            float electricityToTransfer = Mathf.Min(Station.currentElectricity, soyuzNeed);
            
            stationEntity.Get<ResourceComponent>().currentElectricity -= electricityToTransfer;
            soyuzEntity.Get<ResourceComponent>().currentElectricity += electricityToTransfer;
            Debug.Log($"Transferred {electricityToTransfer} units of electricity to Soyuz. Soyuz electricity: {Soyuz.currentElectricity}, Station electricity: {Station.currentElectricity}");

            UpdateUI();
        }
    }

    public void TransferMaterialsButtonToSoyuz()
    {
        ref var Soyuz = ref soyuzEntity.Get<ResourceComponent>();
        ref var Station = ref stationEntity.Get<ResourceComponent>();

        if (Station.currentMaterials > 0)
        {
            float soyuzNeed = Soyuz.maxMaterials - Soyuz.currentMaterials;
            float materialsToTransfer = Mathf.Min(Station.currentMaterials, soyuzNeed);
            
            stationEntity.Get<ResourceComponent>().currentMaterials -= materialsToTransfer;
            soyuzEntity.Get<ResourceComponent>().currentMaterials += materialsToTransfer;
            Debug.Log($"Transferred {materialsToTransfer} units of materials to Soyuz. Soyuz materials: {Soyuz.currentMaterials}, Station materials: {Station.currentMaterials}");

            UpdateUI();
        }
    }

    public void TransferFuelButtonToStation()
    {
        ref var Soyuz = ref soyuzEntity.Get<ResourceComponent>();
        ref var Station = ref stationEntity.Get<ResourceComponent>();

        if (Soyuz.currentFuel > 0)
        {
            float stationNeed = Station.maxFuel - Station.currentFuel;
            float fuelToTransfer = Mathf.Min(Soyuz.currentFuel, stationNeed);
            
            soyuzEntity.Get<ResourceComponent>().currentFuel -= fuelToTransfer;
            stationEntity.Get<ResourceComponent>().currentFuel += fuelToTransfer;
            Debug.Log($"Transferred {fuelToTransfer} units of fuel to Station. Soyuz fuel: {Soyuz.currentFuel}, Station fuel: {Station.currentFuel}");

            UpdateUI();
        }
    }

    public void TransferElectricityButtonToStation()
    {
        ref var Soyuz = ref soyuzEntity.Get<ResourceComponent>();
        ref var Station = ref stationEntity.Get<ResourceComponent>();

        if (Soyuz.currentElectricity > 0)
        {
            float stationNeed = Station.maxElectricity - Station.currentElectricity;
            float electricityToTransfer = Mathf.Min(Soyuz.currentElectricity, stationNeed);
            
            soyuzEntity.Get<ResourceComponent>().currentElectricity -= electricityToTransfer;
            stationEntity.Get<ResourceComponent>().currentElectricity += electricityToTransfer;
            Debug.Log($"Transferred {electricityToTransfer} units of electricity to Station. Soyuz electricity: {Soyuz.currentElectricity}, Station electricity: {Station.currentElectricity}");

            UpdateUI();
        }
    }

    public void TransferMaterialsButtonToStation()
    {
        ref var Soyuz = ref soyuzEntity.Get<ResourceComponent>();
        ref var Station = ref stationEntity.Get<ResourceComponent>();

        if (Soyuz.currentMaterials > 0)
        {
            float stationNeed = Station.maxMaterials - Station.currentMaterials;
            float materialsToTransfer = Mathf.Min(Soyuz.currentMaterials, stationNeed);
            
            soyuzEntity.Get<ResourceComponent>().currentMaterials -= materialsToTransfer;
            stationEntity.Get<ResourceComponent>().currentMaterials += materialsToTransfer;
            Debug.Log($"Transferred {materialsToTransfer} units of materials to Station. Soyuz materials: {Soyuz.currentMaterials}, Station materials: {Station.currentMaterials}");

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _systems.Run();
    }
}
