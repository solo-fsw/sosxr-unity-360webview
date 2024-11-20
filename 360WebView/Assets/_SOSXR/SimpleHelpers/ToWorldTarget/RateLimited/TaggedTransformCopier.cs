using System.Collections;
using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class TaggedTransformCopier : MonoBehaviour
{
    [Tooltip("Lower means more frequently (value = time between repetitions)")]
    [SerializeField] [Range(0.001f, 5f)] private float m_inverseRepeatRate = 0.25f;
    [SerializeField] [TagSelector] private string m_tagToSearchFor;
    [SerializeField] private Transform m_transformToFollow;

    [SerializeField] private bool m_copyPosition = true;
    [SerializeField] private Vector3Int m_positionAxisToUse = new(1, 0, 1);
    [SerializeField] private bool m_copyRotation = true;
    [SerializeField] private Vector3Int m_rotationAxisToUse = new(0, 1, 0);
    [SerializeField] private bool m_copyLocalScale = false;


    private Coroutine _copier;


    private void Update()
    {
        if (_copier != null)
        {
            return;
        }

        if (m_transformToFollow != null && _copier == null)
        {
            _copier = StartCoroutine(Copier());

            return;
        }

        if (m_transformToFollow == null && string.IsNullOrEmpty(m_tagToSearchFor))
        {
            this.Error("We don't have the desired transform, and the Tag to search for is also left empty");

            return;
        }

        if (GameObject.FindWithTag(m_tagToSearchFor))
        {
            m_transformToFollow = GameObject.FindWithTag(m_tagToSearchFor).transform;
        }
    }


    private IEnumerator Copier()
    {
        for (;;)
        {
            CopyTransformParts();

            yield return new WaitForSeconds(m_inverseRepeatRate);
        }
    }


    private void CopyTransformParts()
    {
        if (m_copyPosition)
        {
            var flatPos = m_transformToFollow.position.ZeroOutVector3(m_positionAxisToUse);
            transform.position = flatPos;
        }

        if (m_copyRotation)
        {
            var flatRot = m_transformToFollow.rotation.eulerAngles.ZeroOutVector3(m_rotationAxisToUse);
            transform.rotation = Quaternion.Euler(flatRot);
        }

        if (m_copyLocalScale)
        {
            transform.localScale = m_transformToFollow.localScale;
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        _copier = null;
    }
}