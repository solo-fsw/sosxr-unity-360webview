using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ChiralXRInteractor : MonoBehaviour
{
    public Chirality Chirality;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor XRBaseInteractor;
    public XRHaptics XRHaptics;
    private ChiralXRInteractible _chiralXRInteractible;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable _interactable;


    private void OnEnable()
    {
        XRBaseInteractor.selectEntered.AddListener(ConveyChirality);
        XRBaseInteractor.selectExited.AddListener(ClearChirality);
    }


    private void OnDisable()
    {
        XRBaseInteractor.selectEntered.RemoveListener(ConveyChirality);
        XRBaseInteractor.selectExited.RemoveListener(ClearChirality);
    }


    private void ConveyChirality(SelectEnterEventArgs selectEnterEventArgs)
    {
        _chiralXRInteractible = selectEnterEventArgs.interactableObject.transform.GetComponent<ChiralXRInteractible>();

        if (_chiralXRInteractible != null)
        {
            _chiralXRInteractible.ChiralXRInteractor = this;
        }
        else
        {
            Debug.LogWarningFormat("XRInteractable {0} does not contain ChiralXRInteractible",
                selectEnterEventArgs.interactableObject.transform.gameObject.name);
        }
    }


    private void ClearChirality(SelectExitEventArgs selectExitEventArgs)
    {
        _chiralXRInteractible = selectExitEventArgs.interactableObject.transform.GetComponent<ChiralXRInteractible>();

        if (_chiralXRInteractible != null)
        {
            if (_chiralXRInteractible.ChiralXRInteractor == this)
            {
                _chiralXRInteractible.ChiralXRInteractor = null;
            }
        }
        else
        {
            Debug.LogWarningFormat("XRInteractable {0} does not contain ChiralXRInteractible",
                selectExitEventArgs.interactableObject.transform.gameObject.name);
        }
    }
}