using mrstruijk.Events;


public class TweenBulkAudioFaderOnEvent : TweenBulkAudioFader
{
    private void OnEnable()
    {
        EventsSystem.ScreenFadeStarted += StartAudioFade;
    }


    private void OnDisable()
    {
        EventsSystem.ScreenFadeStarted -= StartAudioFade;
    }
}