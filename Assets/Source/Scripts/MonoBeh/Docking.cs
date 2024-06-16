using Ecs;
using UnityEngine;
using Leopotam.Ecs;

public class Docking : MonoBehaviour
{
    private EcsEntity _soyuzEntity;

    // [SerializeField] private GameObject transferMenu;

    private void Start()
    {
        var soyuzScript = FindObjectOfType<Soyuz>();
        if (soyuzScript != null)
        {
            _soyuzEntity = soyuzScript.GetSoyuzEntity();
            // ref var dockingComponent = ref _soyuzEntity.Get<DockingComponent>();
            // dockingComponent.transferMenu = transferMenu;
        }
        else
        {
            Debug.LogError("Soyuz script not found in the scene.");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dock"))
        {
            ref var dockingComponent = ref _soyuzEntity.Get<DockingComponent>();
            ref var uiComponent = ref _soyuzEntity.Get<UIComponent>();
            dockingComponent.gameObjectWithDock = other.gameObject;
            dockingComponent.readyToInteract = true;
            uiComponent.dockText.text = "Для стыковки нажмите P";
            Debug.Log($"Trigger {other.gameObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dock"))
        {
            ref var uiComponent = ref _soyuzEntity.Get<UIComponent>();
            ref var dockingComponent = ref _soyuzEntity.Get<DockingComponent>();
            dockingComponent.gameObjectWithDock = null;
            dockingComponent.readyToInteract = false;
            uiComponent.dockText.text = "";
            if (!dockingComponent.isDocked)  // Убедитесь, что мы не сбрасываем isDocked, если объект все еще стыкуется
            {
                // dockingComponent.transferMenu.SetActive(false);
                Debug.Log("Un trigger");
            }
        }
    }
}