using System.Collections.Generic;
using System.Linq;
using SOSXR.EnhancedLogger;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif


namespace mrstruijk.Extensions
{
    public static class AnimationExtensionMethods
    {
        /// <summary>
        ///     Checks whether given Animator contains a specific parameter with given string name
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static bool HasParameter(this Animator animator, string parameterName)
        {
            return animator.parameters.Any(param => param.name == parameterName);
        }


        /// <summary>
        ///     Checks whether given Animator contains a specific parameter with given int name
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="parameterHash"></param>
        /// <returns></returns>
        public static bool HasParameter(this Animator animator, int parameterHash)
        {
            return animator.parameters.Any(param => param.nameHash == parameterHash);
        }


        /// <summary>
        ///     Checks whether given Animator contains a specific state with given int hash
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateHash"></param>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        public static bool HasState(this Animator animator, int stateHash, int layerIndex = 0)
        {
            return animator.HasState(layerIndex, stateHash);
        }


        /// <summary>
        ///     Checks whether given Animator contains a specific state with given string name
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateName"></param>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        public static bool HasState(this Animator animator, string stateName, int layerIndex = 0)
        {
            return animator.HasState(layerIndex, Animator.StringToHash(stateName));
        }


        public static bool HasTalkState(this Animator animator)
        {
            return animator.HasState("Talk");
        }


        public static string TalkState(this Animator animator)
        {
            return animator.HasTalkState() ? "Talk" : string.Empty;
        }


        public static bool HasListenState(this Animator animator)
        {
            return animator.HasState("Listen");
        }


        public static string ListenState(this Animator animator)
        {
            return animator.HasListenState() ? "Listen" : string.Empty;
        }


        public static bool HasIdleState(this Animator animator)
        {
            return animator.HasState("Idle");
        }


        public static string IdleState(this Animator animator)
        {
            return animator.HasIdleState() ? "Idle" : string.Empty;
        }


        /// <summary>
        ///     CrossFades to a state, but only if it exists, and we're not already in it
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateName"></param>
        /// <param name="transitionTime"></param>
        /// <param name="layerIndex"></param>
        public static bool StartValidStateCrossFade(this Animator animator, string stateName,
                                                    float transitionTime = 0.5f, int layerIndex = 0)
        {
            if (!animator.HasState(stateName))
            {
                Log.Error("ValidStateCrossFade", "No state found with name", stateName, "on Animator", animator.name);

                return false;
            }

            if (animator.IsInState(stateName))
            {
                Log.Info("ValidStateCrossFade", "Currently already in state", stateName);

                return false;
            }

            Log.Info("ValidStateCrossFade", "CrossFade to state", stateName);

            animator.CrossFadeInFixedTime(stateName, transitionTime, layerIndex);

            return true;
        }


        /// <summary>
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateName"></param>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        public static bool IsInState(this Animator animator, string stateName, int layerIndex = 0)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        }


        #if UNITY_EDITOR


        /// <summary>
        ///     Returns all AnimatorStates in an AnimatorController
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
        public static List<AnimatorState> GetAnimatorStates(this Animator animator)
        {
            var ac = animator.runtimeAnimatorController as AnimatorController;

            if (ac == null)
            {
                return null;
            }

            var acLayers = ac.layers;
            var allStates = new List<AnimatorState>();

            foreach (var i in acLayers)
            {
                var animStates = i.stateMachine.states;

                foreach (var j in animStates)
                {
                    allStates.Add(j.state);
                }
            }

            return allStates;
        }


        /// <summary>
        ///     Returns all AnimatorState names as strings in an AnimatorController
        /// </summary>
        /// <param name="animator"></param>
        /// <returns></returns>
        public static List<string> GetAnimatorStateNames(this Animator animator)
        {
            var allStateNames = new List<string>();
            var allStates = animator.GetAnimatorStates();

            foreach (var state in allStates.Where(state => !allStateNames.Contains(state.name)))
            {
                allStateNames.Add(state.name);
            }

            return allStateNames;
        }


        #endif
    }
}