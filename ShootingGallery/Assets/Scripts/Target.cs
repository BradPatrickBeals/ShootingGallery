using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/**
 * Target class - handles movement of targets
 */
public class Target : MonoBehaviour
{
    // Fields *****************************************************************
    [HideInInspector]public bool b_isStanding = false;

    [SerializeField] private float m_flipSpeed = 1f;

    private Quaternion m_targetRotation;

    private float m_startTime;
    private Quaternion m_starRotation;

    private Coroutine m_flipCoroutine;

    // Properties *************************************************************

    // Methods ****************************************************************

    //get desired location and start rotation
    public void FlipTarget()
    {
        if (b_isStanding)
        {
            m_targetRotation = Quaternion.Euler(90, transform.rotation.y, transform.rotation.z);
        }
        else
        {
            m_targetRotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
        }

        m_startTime = Time.time;
        m_starRotation = transform.rotation;

        //check if Coroutine has already started
        if (m_flipCoroutine != null)
        {
            StopCoroutine(m_flipCoroutine);
        }

        m_flipCoroutine = StartCoroutine(MoveTarget());
    }

    //lerp target towards location
    IEnumerator MoveTarget()
    {
        while (Time.time - m_startTime < m_flipSpeed)
        {
            float distanceCovered = Time.time - m_startTime;

            float fractionOfJourney = distanceCovered / m_flipSpeed;

            transform.rotation = Quaternion.Lerp(m_starRotation, m_targetRotation, fractionOfJourney);

            yield return null;
        }

        transform.rotation = m_targetRotation;

        b_isStanding = !b_isStanding;
    }
}
