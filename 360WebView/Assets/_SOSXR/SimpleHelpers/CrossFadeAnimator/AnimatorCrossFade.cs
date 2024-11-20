using mrstruijk.Extensions;
using UnityEngine;


public class AnimatorCrossFade : MonoBehaviour
{
    private AnimatorCrossFade[] _crossFaders;
    private string _currentStateName;

    protected Animator Anim;
    protected bool ConversatorOverriden;
    protected ComponentHelper Helper;


    protected virtual void Awake()
    {
        Helper = new ComponentHelper(gameObject);

        Anim = Helper.GetComponentInGameObjectTree<Animator>();

        _crossFaders = FindObjectsOfType<AnimatorCrossFade>();
    }


    /// <summary>
    ///     CrossFade to a state, if we're not already in that state.
    ///     Can set the bool ConversatorOverriden, which is important to stop automatic state switching such as
    ///     is done in the LateUpdate of AnimatorCrossFadeConversation.
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="overrideOthers"></param>
    /// <returns></returns>
    protected bool CrossFadeTo(string stateName, bool overrideOthers = false)
    {
        if (_currentStateName == stateName)
        {
            return false;
        }

        OverrideAutomaticStateSwitching(overrideOthers);

        _currentStateName = stateName;

        return Anim.StartValidStateCrossFade(stateName);
    }


    /// <summary>
    ///     Makes sure that automatic override by AnimatorCrossFadeConversation is not possible.
    /// </summary>
    public void CrossFadeWithOverride(string stateName)
    {
        CrossFadeTo(stateName, true);
    }


    /// <summary>
    ///     Will continue to allow automatic override by AnimatorCrossFadeConversation. So when an Animator is first set to a
    ///     specific state here, the AnimatorCrossFadeConversation can still set it to either Talk or Listen state.
    /// </summary>
    public void CrossFadeWithoutOverride(string stateName)
    {
        CrossFadeTo(stateName);
    }


    /// <summary>
    ///     This makes sure that the automatic crossfade (in LateUpdate) of the AnimatorCrossFadeConversation has a way to be
    ///     overridden.
    /// </summary>
    private void OverrideAutomaticStateSwitching(bool overrideAll)
    {
        foreach (var crossFader in _crossFaders)
        {
            if (crossFader.Anim == Anim)
            {
                crossFader.ConversatorOverriden = overrideAll;
            }
        }
    }


    /// <summary>
    ///     Allows for automatic state switching to continue, such as in LateUpdate of AnimatorCrossFadeConversation.
    /// </summary>
    public void StopOverriding()
    {
        OverrideAutomaticStateSwitching(false);
    }


    private void OnDestroy()
    {
        if (Helper != null)
        {
            Destroy(Helper);
        }
    }
}