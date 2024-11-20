using mrstruijk;
using UnityEngine;


[CreateAssetMenu(menuName = "mrstruijk/CurrentDevice", fileName = "CurrentDevice")]
public class CurrentDevice : ScriptableObject
{
    public Device Current;
    [DisableEditing] public string DeviceName;
}


public enum Device
{
    None,
    HMD,
    Tablet,
    Editor
}