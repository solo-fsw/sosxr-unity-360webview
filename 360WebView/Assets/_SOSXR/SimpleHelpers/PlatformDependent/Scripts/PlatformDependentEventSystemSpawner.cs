using SOSXR.EnhancedLogger;
using UnityEngine;


public class PlatformDependentEventSystemSpawner : MonoBehaviour
{
    [SerializeField] private CurrentDevice m_currentDevice;
    [SerializeField] private bool m_autoStart;

    [Space(20)]
    [SerializeField] private float m_spawnDelay;
    [SerializeField] private bool m_spawnEventSystems;
    [SerializeField] private GameObject m_screenEventSystemPrefab;
    [SerializeField] private GameObject m_xrEventSystemPrefab;


    private GameObject _eventSystemGo;


    private void OnEnable()
    {
        if (m_autoStart)
        {
            SpawnEventSystem();
        }
    }


    private void SpawnEventSystem()
    {
        if (m_currentDevice.Current == Device.HMD)
        {
            SpawnOnOpenXR();
        }
        else
        {
            SpawnScreenBased();
        }
    }


    private void SpawnOnOpenXR()
    {
        Invoke(nameof(SpawnOpenXRObjects), m_spawnDelay);
    }


    private void SpawnScreenBased()
    {
        Invoke(nameof(SpawnScreenBasedObjects), m_spawnDelay);
    }


    private void SpawnOpenXRObjects()
    {
        _eventSystemGo = InstantiateRequiredObject(m_xrEventSystemPrefab, m_spawnEventSystems);
    }


    private void SpawnScreenBasedObjects()
    {
        _eventSystemGo = InstantiateRequiredObject(m_screenEventSystemPrefab, m_spawnEventSystems);
    }


    private GameObject InstantiateRequiredObject(GameObject go, bool toSpawn = true)
    {
        if (toSpawn == false)
        {
            this.Info("Didn't spawn", go.name, "because it wasn't needed");

            return null;
        }

        var spawnedObject = Instantiate(go);

        this.Success("Spawned", go.name);

        return spawnedObject;
    }


    private void OnDisable()
    {
    }
}