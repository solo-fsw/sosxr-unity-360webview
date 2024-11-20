using UnityEngine;


public class ITLXRControllerHaptics : MonoBehaviour, ITLActivate
{
    [SerializeField] private ListOfTagsToFindListOfGameObjects s_findWithTag;

    [SerializeField] private float s_amplitude = 0.25f;
    [SerializeField] private float s_duration = 0.25f;


    public void TLActivate()
    {
        SendHapticPulseToFoundGameObjects();
    }


    private void SendHapticPulseToFoundGameObjects()
    {
        if (s_findWithTag.TaggedGameObjects == null)
        {
            return;
        }

        foreach (var controller in s_findWithTag.TaggedGameObjects)
        {
            if (controller == null)
            {
                return;
            }

            var haptic = controller.GetComponent<ITLHaptic>();

            if (haptic == null)
            {
                return;
            }

            haptic.SendHaptic(s_amplitude, s_duration);
        }
    }
}