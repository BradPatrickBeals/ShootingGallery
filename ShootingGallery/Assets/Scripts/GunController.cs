using System;
using UnityEngine;
using UnityEngine.VFX;

/**
 * Gun Controller - Manages firing, effects and interactions with other classes for the gun
 */
public class GunController : MonoBehaviour
{
    // Fields *****************************************************************
    [SerializeField] private VisualEffectAsset m_HitVFX;
    [SerializeField] private UIManager m_UIManager;

    private bool b_canFire = true;

    [SerializeField] private float m_FireRate = 100;

    private float m_LocalTimer;

    [SerializeField] private Transform m_Muzzle;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private GameObject m_MuzzleEffect;

    // Properties *************************************************************

    // Methods ****************************************************************
    void Update()
    {
        //Fire input
        if (Input.GetButtonDown("Fire1"))
        {
            FireGun();
            b_canFire = false;
        }

        //Fire cooldown - would be better in a corutine but couldn't get it working in the timeframe
        if (b_canFire == false)
        {
            m_LocalTimer--;
            if (m_LocalTimer <= 0)
            {
                m_LocalTimer = m_FireRate;
                b_canFire = true;
            }
        }
    }

    //Handles firing of gun
    void FireGun()
    {
        if (b_canFire == false) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            CheckHit(hit);
        }

        //play audio on fire
        if (m_AudioSource != false)
        {
            m_AudioSource.Play();
        }
        
        //play fire vfx
        if (m_MuzzleEffect != false && m_Muzzle != false)
        {
            GameObject flashEffect = Instantiate(m_MuzzleEffect, m_Muzzle.position, m_Muzzle.rotation);
            Destroy(flashEffect, 5.0f);
        }
        

    }

    //Check what was hit and handle objects reaction
    private void CheckHit(RaycastHit hit)
    {
        Target target = hit.collider.GetComponentInParent<Target>();
        if (target != null)
        {
            target.FlipTarget();

            //play hit VFX
            if (m_HitVFX != null)
            {
                GameObject newEffect = new GameObject("VFXInstance");
                newEffect.transform.position = hit.point;

                VisualEffect newVFX = newEffect.AddComponent<VisualEffect>();
                newVFX.visualEffectAsset = m_HitVFX;
                newVFX.Play();
                Destroy(newEffect, 1.0f);
            }
            //update ui
            if (m_UIManager != null)
            {
                StartCoroutine(m_UIManager.ChangeCrosshairColour(Color.green));
                m_UIManager.m_PlayerScore++;
                m_UIManager.UpdateText();
            }

        }
        else
        {
            FalseTarget falseTarget = hit.collider.GetComponentInParent<FalseTarget>();
            if (falseTarget != null)
            {
                //update ui
                if (m_UIManager != null)
                {
                    StartCoroutine(m_UIManager.ChangeCrosshairColour(Color.red));
                    m_UIManager.m_PlayerScore--;
                    m_UIManager.UpdateText();
                }
            }
        }
    }
}
