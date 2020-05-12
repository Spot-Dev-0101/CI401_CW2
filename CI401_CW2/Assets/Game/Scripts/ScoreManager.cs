using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;

    private int m_score;

    private float m_multiplier = 1;

    public int score
    {
        get { return m_score; }
        set
        {
            m_score = value;
            scoreText.SetText(m_score.ToString());
        }
    }
    
    public float multiplier
    {
        get { return m_multiplier; }
        set
        {
            if (value >= 1) {
                m_multiplier = value;
            }
            else
            {
                m_multiplier = 1;
            }
            if (m_multiplier > maxMultiplier)
            {
                m_multiplier = maxMultiplier;
            }
            multiText.SetText(multiplier.ToString("F1"));
            multiText.transform.eulerAngles = new Vector3(0, 0, -(m_multiplier-1));
            multiText.transform.localScale = new Vector3(1+m_multiplier/20, 1 + m_multiplier / 20, 1 + m_multiplier / 20);
        }
    }
    

    public int scoreIncreaseSlide = 100;
    public int scoreIncreaseJump = 15;
    public int scoreIncreaseEnemyKill = 10000;

    public float maxMultiplier = 15;

    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        multiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.slide == true && pm.grounded == true && pm.rb.velocity.magnitude > 5)
        {
            score += (int)(scoreIncreaseSlide * multiplier);
            multiplier += 0.1f;
        } else if (pm.slide == false && pm.grounded == false && pm.getGroundDist() > 3.5)
        {
            score += (int)(scoreIncreaseJump * multiplier);
            multiplier += 0.1f;
        }
        else
        {
            multiplier += -0.05f;
        }
    }

    
}
