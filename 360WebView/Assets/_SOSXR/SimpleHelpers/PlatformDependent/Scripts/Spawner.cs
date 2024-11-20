using mrstruijk.Events;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject m_networkedPrefab;

    [SerializeField] protected GameObject m_keyboardPrefab;

    [SerializeField] protected GameObject m_waitingPrefab;
    [SerializeField] private float m_waitingTime = 5f;


    private GameObject _spawnedKeyboardObject;
    private GameObject _spawnedNetworkedObject;
    private GameObject _spawnedWaitingObject;


    protected virtual void OnEnable()
    {
        EventsSystem.RequestJoinCodeKeyboard += SpawnKeyboardUI;
        EventsSystem.NetworkDisconnectedOrFailed += SpawnWaitingUI;
    }


    protected virtual void Start()
    {
        SpawnNetworkedUI();
    }


    public void SpawnNetworkedUI()
    {
        if (_spawnedNetworkedObject != null)
        {
            return;
        }

        _spawnedNetworkedObject = Instantiate(m_networkedPrefab);

        DestroyKeyboardUI();
        DestroyWaitingUI();
    }


    public void SpawnKeyboardUI()
    {
        if (_spawnedKeyboardObject != null)
        {
            return;
        }

        _spawnedKeyboardObject = Instantiate(m_keyboardPrefab);

        DestroyNetworkedUI();
        DestroyWaitingUI();
    }


    public virtual void SpawnWaitingUI()
    {
        if (_spawnedWaitingObject != null)
        {
            return;
        }

        _spawnedWaitingObject = Instantiate(m_waitingPrefab);

        DestroyNetworkedUI();
        DestroyKeyboardUI();

        SpawnNetworkUIChooserAfterDelay();
    }


    private void SpawnNetworkUIChooserAfterDelay()
    {
        Invoke(nameof(SpawnNetworkedUI), m_waitingTime);
    }


    public void DestroyNetworkedUI()
    {
        if (_spawnedNetworkedObject == null)
        {
            return;
        }

        Destroy(_spawnedNetworkedObject);
    }


    public void DestroyKeyboardUI()
    {
        if (_spawnedKeyboardObject == null)
        {
            return;
        }

        Destroy(_spawnedKeyboardObject);
    }


    public void DestroyWaitingUI()
    {
        if (_spawnedWaitingObject == null)
        {
            return;
        }

        Destroy(_spawnedWaitingObject);
    }


    public virtual void DestroyAll()
    {
        DestroyNetworkedUI();
        DestroyKeyboardUI();
        DestroyWaitingUI();
    }


    protected virtual void OnDisable()
    {
        EventsSystem.RequestJoinCodeKeyboard -= SpawnKeyboardUI;
        EventsSystem.NetworkDisconnectedOrFailed -= SpawnWaitingUI;

        DestroyAll();
    }
}