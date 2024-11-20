using mrstruijk.Events;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ITLHaptic : MonoBehaviour, ITLActivate
{
    public XRBaseController XRBaseController;
    [SerializeField] private float m_defaultAmplitude = 0.25f;
    [SerializeField] private float m_defaultDuration = 0.25f;

    [SerializeField] private Grabber m_grabber;


    public void TLActivate()
    {
        SendHaptic();
    }


    private void Awake()
    {
        if (XRBaseController == null)
        {
            XRBaseController = GetComponent<XRBaseController>();
        }

        if (m_grabber == null)
        {
            m_grabber = GetComponent<Grabber>();
        }
    }


    private void OnEnable()
    {
        EventsSystem.SendDefaultHapticEvent += SendHaptic;
    }


    public void SendHaptic()
    {
        XRBaseController.SendHapticImpulse(m_defaultAmplitude, m_defaultDuration);
    }


    public void SendHaptic(float amplitude, float duration)
    {
        XRBaseController.SendHapticImpulse(amplitude, duration);
    }


    private void OnDisable()
    {
        EventsSystem.SendDefaultHapticEvent -= SendHaptic;
    }
}