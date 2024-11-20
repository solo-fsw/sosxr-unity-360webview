using System.Collections;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class UnloadUnusedAssets : MonoBehaviour
{
    [SerializeField] [Range(-1, 60 * 5)] private float m_autoUnloadInterval = 10f;
    private Coroutine _unloadCoroutine;


    private void OnEnable()
    {
        if (m_autoUnloadInterval <= 0)
        {
            this.Info("Interval is 0 or less, so not looping the unused assets");

            return;
        }

        if (_unloadCoroutine == null)
        {
            _unloadCoroutine = StartCoroutine(UnloadCR());
        }
        else
        {
            StopCoroutine(_unloadCoroutine);
            _unloadCoroutine = StartCoroutine(UnloadCR());
        }
    }


    private IEnumerator UnloadCR()
    {
        for (;;)
        {
            yield return new WaitForSeconds(m_autoUnloadInterval);
            UnloadUnusedAssetsFromGame();
        }
    }


    public void UnloadUnusedAssetsFromGame()
    {
        Resources.UnloadUnusedAssets();
        this.Success("Unloaded unused assets");
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }
}