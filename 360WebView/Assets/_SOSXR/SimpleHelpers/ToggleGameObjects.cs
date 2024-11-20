using UnityEngine;


public class ToggleGameObjects : MonoBehaviour
{
    [SerializeField] private GameObject m_startingGameObject;
    [SerializeField] private GameObject m_otherGameObject;


    public void SwapToSecond()
    {
        m_startingGameObject.SetActive(true);
        m_otherGameObject.SetActive(false);
    }


    public void SwapToFirst()
    {
        m_startingGameObject.SetActive(false);
        m_otherGameObject.SetActive(true);
    }
}