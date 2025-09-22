using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/**
 * UI Manager class - manages ui elements
 */
public class UIManager : MonoBehaviour
{
    // Fields *****************************************************************
    private Color m_StartColour = Color.white;
    [SerializeField] private List<Image> m_CrosshairImages = new List<Image>();
    [SerializeField] private float m_Duration = 0.001f;

    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [HideInInspector]public float m_PlayerScore = 0f;
    // Properties *************************************************************

    // Methods ****************************************************************
    private void Start()
    {
        UpdateText();
    }

    //Briefly change colour of cross hair
    public IEnumerator ChangeCrosshairColour(Color color)
    {
        for (int i = 0; i < m_CrosshairImages.Count; i++)
        {
            m_CrosshairImages[i].color = color;
        }

        yield return new WaitForSeconds(m_Duration);

        for (int i = 0; i < m_CrosshairImages.Count; i++)
        {
            m_CrosshairImages[i].color = m_StartColour;
        }
    }

    //Update score text
    public void UpdateText()
    {
        if (m_ScoreText == null) return;

        m_ScoreText.text = "Score: " + m_PlayerScore;
    }

}
