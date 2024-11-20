using UnityEngine;
using UnityEngine.Serialization;


public class Grabber : MonoBehaviour
{
    [FormerlySerializedAs("s_grabber")] [SerializeField] private GrabEnum m_grabber;

    public GrabEnum GrabbedBy => m_grabber;
}