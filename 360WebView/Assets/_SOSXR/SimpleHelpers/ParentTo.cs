using mrstruijk.Extensions;
using UnityEngine;


public class ParentTo : MonoBehaviour
{
    [TagSelector] [SerializeField] private string m_tagToFind = "MainCamera";
    private Transform _transformToFollow;


    private void Update()
    {
        if (_transformToFollow != null)
        {
            enabled = false;

            return;
        }

        if (GameObject.FindWithTag(m_tagToFind) == null)
        {
            return;
        }

        _transformToFollow = GameObject.FindWithTag(m_tagToFind).transform;

        if (_transformToFollow != null)
        {
            transform.SetParent(_transformToFollow);
            transform.LocalZeroOutTransform();
        }
    }
}