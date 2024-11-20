using mrstruijk.Events;
using UnityEngine;


public class HandleConfigData : MonoBehaviour
{
    public ConfigData ConfigData;


    private void Awake()
    {
        ConfigData.LoadConfig();
    }


    private void OnEnable()
    {
        EventsSystem.KeyCodeEntered += AddParticipantNumber;
        EventsSystem.VideoClipStarted += ChangeVideoClipName;
    }


    private void ChangeVideoClipName(string videoClipName)
    {
        AmendConfigData(videoClipName: videoClipName);
    }


    private void AddParticipantNumber(int participantNumber)
    {
        AmendConfigData(participantNumber);
    }


    private void AmendConfigData(int participantNumber = -1, string videoClipName = null)
    {
        if (participantNumber != -1)
        {
            ConfigData.PPN = participantNumber;
        }

        if (videoClipName != null)
        {
            ConfigData.VideoName = videoClipName;
        }

        EventsSystem.ConfigInformationChanged?.Invoke();
    }


    private void OnDisable()
    {
        EventsSystem.KeyCodeEntered -= AddParticipantNumber;
        EventsSystem.VideoClipStarted -= ChangeVideoClipName;
    }
}