using System;


namespace mrstruijk.Events
{
    public static class EventsSystem
    {

        public static Action<int> KeyCodeEntered;

        public static Action<string> VideoClipStarted;
        public static Action ConfigInformationChanged;
    }
}