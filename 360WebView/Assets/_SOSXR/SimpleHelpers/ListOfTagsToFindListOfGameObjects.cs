using System.Collections.Generic;
using System.Linq;
using mrstruijk;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class ListOfTagsToFindListOfGameObjects : MonoBehaviour
{
    [SerializeField] private List<string> m_tags;

    [DisableEditing] public List<GameObject> TaggedGameObjects;


    private void Start()
    {
        foreach (var found in m_tags.Select(GameObject.FindWithTag))
        {
            if (found == null)
            {
                this.Warning("No GameObjects with selected tags has been found");

                return;
            }

            this.Success("Added GameObject to list", found.name);
            TaggedGameObjects.Add(found);
        }
    }
}