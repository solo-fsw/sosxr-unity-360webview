using UnityEngine;


public class ITLSkinnedRendererEnableDisable : MonoBehaviour, ITLActivate
{
    [SerializeField] private ListOfTagsToFindListOfGameObjects s_findWithTag;
    [SerializeField] private bool _enable = true;


    public void TLActivate()
    {
        EnableOrDisableSkinnedRenderer();
    }


    private void EnableOrDisableSkinnedRenderer()
    {
        if (s_findWithTag.TaggedGameObjects == null)
        {
            return;
        }

        foreach (var gameObj in s_findWithTag.TaggedGameObjects)
        {
            var rend = gameObj.GetComponentInChildren<SkinnedMeshRenderer>();

            if (rend != null)
            {
                rend.enabled = _enable;
            }
        }
    }
}