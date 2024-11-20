using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class SpawnerSpawner : MonoBehaviour
{
    [SerializeField] private CurrentDevice m_currentDevice;
    [SerializeField] private GameObject m_screenSpawner;
    [SerializeField] private GameObject m_xrSpawner;

    private GameObject _spawnedObject;


    private void Awake()
    {
        EventsSystem.DeviceHasBeenSet += SpawnCorrectSpawner;
    }


    public void SpawnCorrectSpawner()
    {
        if (_spawnedObject != null)
        {
            this.Info("We already have a spawner, not doing this again");

            return;
        }

        if (m_currentDevice.Current == Device.HMD)
        {
            _spawnedObject = Instantiate(m_xrSpawner);
        }
        else
        {
            _spawnedObject = Instantiate(m_screenSpawner);
        }

        _spawnedObject.transform.SetParent(transform);
    }


    private void OnDestroy()
    {
        EventsSystem.DeviceHasBeenSet -= SpawnCorrectSpawner;

        if (_spawnedObject != null)
        {
            Destroy(_spawnedObject);
        }
    }
}