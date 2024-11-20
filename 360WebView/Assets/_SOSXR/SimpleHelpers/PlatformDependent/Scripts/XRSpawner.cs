using mrstruijk.Events;
using UnityEngine;


public class XRSpawner : Spawner
{
    [SerializeField] private GameObject m_rigPrefab;
    private GameObject _spawnedRig;


    protected override void OnEnable()
    {
        base.OnEnable();
        EventsSystem.DeviceHasBeenSet += SpawnRig;
    }


    protected override void Start()
    {
        base.Start();
        SpawnRig();
    }


    private void SpawnRig()
    {
        if (_spawnedRig != null)
        {
            return;
        }

        _spawnedRig = Instantiate(m_rigPrefab);
    }


    private void DestroyRig()
    {
        if (_spawnedRig == null)
        {
            return;
        }

        Destroy(_spawnedRig);
    }


    public override void SpawnWaitingUI()
    {
        SpawnRig();

        base.SpawnWaitingUI();
    }


    public override void DestroyAll()
    {
        base.DestroyAll();
        DestroyRig();
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        EventsSystem.DeviceHasBeenSet -= SpawnRig;

        DestroyAll();
    }
}