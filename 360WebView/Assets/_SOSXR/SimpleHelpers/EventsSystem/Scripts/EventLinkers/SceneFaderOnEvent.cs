using UnityEngine.SceneManagement;


public class SceneFaderOnEvent : SceneFader
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartFadeIn;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartFadeIn;
    }
}