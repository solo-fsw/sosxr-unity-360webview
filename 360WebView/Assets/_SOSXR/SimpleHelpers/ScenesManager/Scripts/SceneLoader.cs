using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 1;
    [SerializeField] private float waitTime = 2f;


    private void Start()
    {
        StartCoroutine(LoadSceneCR());
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }


    private IEnumerator LoadSceneCR()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }
}