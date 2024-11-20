using System.Linq;
using UnityEngine;


/// <summary>
///     Combines getting one or multiple components from current gameObject, it's children and all parents.
///     Initialise with a gameObject, then use GetComponentInGameObjectTree and GetComponentsInGameObjectTree to get
///     components in children and parents.
///     Example:
///     var helper = new ComponentHelper(gameObject);
///     var animator = helper.GetComponentInGameObjectTree
///     <Animator>
///         ();
///         or
///         var listeners = helper.GetComponentsInGameObjectTree<ListenerClass>();
/// </summary>
public class ComponentHelper : Component
{
    private readonly GameObject _gameObject;


    public ComponentHelper(GameObject gameObject)
    {
        _gameObject = gameObject;
    }


    /// <summary>
    ///     Performs both GetComponentInChildren and GetComponentInParent, in that order.
    ///     If it doesn't find the component in children or current gameObject, it will check the parents.
    ///     Returns the first component it finds.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetComponentInGameObjectTree<T>()
    {
        var found = _gameObject.GetComponentInChildren<T>() ?? _gameObject.GetComponentInParent<T>();

        return found;
    }


    /// <summary>
    ///     Performs both GetComponentsInChildren and GetComponentsInParent.
    ///     It will return all components it finds in children and parents: returns the full list.
    ///     Will not return any duplicates.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T[] GetComponentsInGameObjectTree<T>()
    {
        var found = _gameObject.GetComponentsInChildren<T>().ToList();

        var inParent = _gameObject.GetComponentsInParent<T>();

        foreach (var comp in inParent)
        {
            if (found.Contains(comp)) // To make sure we're not adding any component twice
            {
                continue;
            }

            found.Add(comp);
        }

        return found.ToArray();
    }
}