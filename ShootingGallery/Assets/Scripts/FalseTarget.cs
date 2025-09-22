using UnityEngine;

/**
 * False target class - only handles object destruction
 */
public class FalseTarget : MonoBehaviour
{
    // Fields *****************************************************************
    [SerializeField] private float m_despawnTime = 25f;

    // Properties *************************************************************

    // Methods ****************************************************************
    private void Start()
    {
        //Destroy object after set time incase object missed ground
        Destroy(gameObject, m_despawnTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy on contact with ground
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
