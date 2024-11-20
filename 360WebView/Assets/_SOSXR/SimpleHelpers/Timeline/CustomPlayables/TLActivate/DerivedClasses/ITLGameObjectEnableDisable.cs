using UnityEngine;


public class ITLGameObjectEnableDisable : MonoBehaviour, ITLActivate
{
    [SerializeField] private ListOfTagsToFindListOfGameObjects s_findWithTag;
    [SerializeField] private bool s_enable;


    public void TLActivate()
    {
        SetGameObjectToBoolState();
    }


    private void SetGameObjectToBoolState()
    {
        if (s_findWithTag.TaggedGameObjects == null)
        {
            return;
        }

        foreach (var gameObj in s_findWithTag.TaggedGameObjects)
        {
            gameObj.SetActive(s_enable);
        }
    }
}