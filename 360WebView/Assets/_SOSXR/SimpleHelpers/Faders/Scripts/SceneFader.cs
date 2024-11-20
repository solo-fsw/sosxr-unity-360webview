using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneFader : MonoBehaviour
{
    [SerializeField] private ScreenFader s_screenFader;
    [SerializeField] private List<BoolFloat> s_fadeOutAfterScene;


    public void StartFadeIn(Scene arg0, LoadSceneMode loadSceneMode)
    {
        s_screenFader.FadeToClear();
    }


    public float CheckIfFadeOutIsDesired()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;

        var timer = 0f;

        if (s_fadeOutAfterScene.Count < currentIndex)
        {
            return s_screenFader.FadeToBlack();
        }

        if (s_fadeOutAfterScene[currentIndex].Bool)
        {
            timer = s_screenFader.FadeToBlack(s_fadeOutAfterScene[currentIndex].Float);
        }

        return timer;
    }
}