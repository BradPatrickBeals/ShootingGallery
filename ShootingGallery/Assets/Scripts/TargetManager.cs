using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/**
 * Target manager class - handles all targets in scene
 */
public class TargetManager : MonoBehaviour
{
    // Fields *****************************************************************
    [SerializeField]private List<Target> m_TargetList;
    private float m_Interval = 500;

    private float m_LocalTimer;

    // Properties *************************************************************

    // Methods ****************************************************************
    void Start()
    {
        m_LocalTimer = m_Interval;
    }

    public void Update()
    {
        m_LocalTimer--;
        if (m_LocalTimer <= 0)
        {
            FlipTarget();
            m_LocalTimer = m_Interval;
        }
    }

    //Flip a random target up after a set time if the previous has been knocked down already 
    public void FlipTarget()
    {
        if (m_TargetList.Count == 0) return;

        for (int i = 0; i < m_TargetList.Count; i++)
        {
            if (m_TargetList[i].b_isStanding)
            {
                return;
            }
        }
        
        m_TargetList[Random.Range(0, m_TargetList.Count)].FlipTarget();
    }
}
