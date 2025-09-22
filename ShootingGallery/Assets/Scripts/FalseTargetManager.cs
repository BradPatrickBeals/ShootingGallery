using NUnit.Framework.Constraints;
using UnityEngine;

/**
 * False target manager class - handles spawning of false targets
 */
public class FalseTargetManager : MonoBehaviour
{
    [SerializeField] private Transform m_FalseTarget;
    [SerializeField] private float m_SpawnOffset = 5f;
    [SerializeField] private float m_SpawnTime = 20f;

    private float m_LocalTimer;

    private void Start()
    {
        m_LocalTimer = m_SpawnTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        //Spawn timer - would be better in a couritine but it kept breaking
        m_LocalTimer--;
        if (m_LocalTimer <= 0)
        {
            SpawnFalseTarget();
            m_LocalTimer = m_SpawnTime;
        }
    }

    //Spawn false targets
    void SpawnFalseTarget()
    {
        if (m_FalseTarget == null) return;

        float SpawnX = Random.Range(transform.position.x - m_SpawnOffset, transform.position.x + m_SpawnOffset);
        float SpawnZ = Random.Range(transform.position.z - m_SpawnOffset, transform.position.z + m_SpawnOffset);
        Vector3 SpawnPosition = new Vector3(SpawnX, transform.position.y, SpawnZ);

        Instantiate(m_FalseTarget, SpawnPosition, transform.rotation);
    }
}
