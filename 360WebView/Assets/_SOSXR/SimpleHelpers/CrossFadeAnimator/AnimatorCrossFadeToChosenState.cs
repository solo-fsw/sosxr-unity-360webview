using System.Collections.Generic;
using mrstruijk.Extensions;
using UnityEngine;


public class AnimatorCrossFadeToChosenState : AnimatorCrossFade
{
    [SerializeField] private string m_chosenState;

    private List<string> _animatorStates;


    /// <summary>
    ///     Makes sure that automatic override by AnimatorCrossFadeConversation is not possible.
    /// </summary>
    public void CrossFadeWithOverride()
    {
        CrossFadeTo(m_chosenState, true);
    }


    /// <summary>
    ///     Will continue to allow automatic override by AnimatorCrossFadeConversation. So when an Animator is first set to a
    ///     specific state here, the AnimatorCrossFadeConversation can still set it to either Talk or Listen state.
    /// </summary>
    public void CrossFadeWithoutOverride()
    {
        CrossFadeTo(m_chosenState);
    }


    #region Get States For List

    #if UNITY_EDITOR


    private void OnValidate()
    {
        GetStateList();
    }


    private void Reset()
    {
        GetStateList();
    }


    private void GetStateList()
    {
        var helper = new ComponentHelper(gameObject);

        Anim = helper.GetComponentInGameObjectTree<Animator>();

        DestroyImmediate(helper);

        if (Anim == null)
        {
            return;
        }

        if (_animatorStates == null || _animatorStates.Count == 0 || _animatorStates != Anim.GetAnimatorStateNames())
        {
            _animatorStates = Anim.GetAnimatorStateNames();
        }
    }
    #endif

    #endregion
}