using System;
using UnityEngine;


namespace mrstruijk.Events
{
    public static class EventsSystem
    {
        #region MonoBehaviour Cycle

        public static Action OnAwakeAction;
        public static Action OnEnableAction;
        public static Action OnStartAction;
        public static Action OnFixedUpdateAction;
        public static Action OnUpdateAction;
        public static Action OnLateUpdateAction;
        public static Action OnApplicationPauseAction;
        public static Action OnApplicationQuitAction;
        public static Action OnDisableAction;
        public static Action OnDestroyAction;

        #endregion

        #region Define Custom Actions here

        #region Scenes

        public static Action<string> SceneHasBeenLoadedOnAllClients;
        public static Action<string> SceneHasBeenUnloadedOnAllClients;
        public static Action<string> RequestSceneLoad;
        public static Action<int> RequestSceneLoadByIndex;
        public static Action<string> SceneLoadHasBeenRequestedOnServer;
        public static Action<string> RequestSceneUnload;
        public static Action<string> SceneUnloadHasBeenRequestedOnServer;
        public static Action StartFadeIn;
        public static Action StartFadeOut;
        public static Action StartFadeRound;

        #endregion


        #region Overseer

        public static Action LanguageHasBeenChanged;

        #endregion


        #region Networking

        public static Action Authenticated;
        public static Action NetworkConnected;
        public static Action NetworkDisconnectedOrFailed;
        public static Action RequestJoinCodeKeyboard;
        public static Action RequestQuickJoin;
        public static Action<string> RequestJoin;
        public static Action RequestHost;

        #endregion


        #region Avatar

        public static Action<string> PlayerInstantiated;
        public static Action<string> SpawnFacedAvatar;
        public static Action FacedAvatarHasBeenSpawned;
        public static Action NextGlasses;
        public static Action<string> ChooseGlasses;
        public static Action NextShirt;
        public static Action<string> ChooseShirt;
        public static Action NextHair;
        public static Action<string> ChooseHair;
        public static Action<Color> ChooseSkinColour;
        public static Action<Color> ChooseHairColour;
        public static Action<Color> ColorChanged;
        public static Action NextEyes;
        public static Action<string> ChooseEyes;
        public static Action NextLashes;
        public static Action<float> ChangeBodySize;
        public static Action<float> ChangeHeadSize;
        public static Action<float> ChangeHandsSize;
        public static Action<float> ChangeCameraHeadHeight;
        public static Action<float> ChangeCameraYOffset;
        public static Action ReturnToDefaultYOffset;
        public static Action<Vector2> SetSALSACutoff;
        public static Action ResetCurrent;
        public static Action<Color> GiveColorToBrush;
        public static Action<Color> ChooseShirtColour;

        #endregion

        public static Action DeviceHasBeenSet;


        public static Action HandleReleasedByParticipant;
        public static Action HandleGrabbedByParticipant;

        public static Action<float> ScreenFadeStarted;

        public static Action GazedLongEnough;


        public static Action<Chirality> GrabbedWithHand;
        public static Action<Chirality> ReleasedWithHand;
        public static Action<int> KeyCodeEntered;
        public static Action SendDefaultHapticEvent;


        public static Action<string> VideoClipStarted;
        public static Action ConfigInformationChanged;

        #endregion
    }
}