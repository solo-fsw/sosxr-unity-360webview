using UnityEngine;
using UnityEngine.Serialization;


public class ITLSkinnedRendererToggle : MonoBehaviour, ITLActivate
{
    [FormerlySerializedAs("s_findWithTag")] [SerializeField] private ListOfTagsToFindListOfGameObjects m_findWithTag;


    public void TLActivate()
    {
        ToggleSkinnedRenderEnabled();
    }


    private void ToggleSkinnedRenderEnabled()
    {
        if (m_findWithTag.TaggedGameObjects == null)
        {
            return;
        }

        foreach (var gameObj in m_findWithTag.TaggedGameObjects)
        {
            if (gameObj == null)
            {
                return;
            }

            var rend = gameObj.GetComponentInChildren<SkinnedMeshRenderer>();

            if (rend == null)
            {
                return;
            }

            rend.enabled = !rend.enabled;
        }
    }
}