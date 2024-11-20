using UnityEngine;


public class ITLGameObjectToggle : MonoBehaviour, ITLActivate
{
    [SerializeField] private ListOfTagsToFindListOfGameObjects s_findWithTag;


    public void TLActivate()
    {
        ToggleGameObject();
    }


    private void ToggleGameObject()
    {
        if (s_findWithTag.TaggedGameObjects == null)
        {
            return;
        }

        foreach (var gameObj in s_findWithTag.TaggedGameObjects)
        {
            var currentlyActiveInHierarchy = gameObj.activeInHierarchy;
            gameObj.SetActive(!currentlyActiveInHierarchy);
        }
    }
}