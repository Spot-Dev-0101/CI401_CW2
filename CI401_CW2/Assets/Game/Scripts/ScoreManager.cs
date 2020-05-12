using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;

    private int m_score;

    public int score
    {
        get { return m_score; }
        set
        {
            m_score = value;
            scoreText.SetText(m_score.ToString());
        }
    }

    public float multiplier = 1;

    public float multiIncreaseDelay = 1;
    public float mutliCooldownDelay = 1;

    public int scoreIncreaseSlide = 100;
    public int scoreIncreaseJump = 15;

    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        multiText.SetText(multiplier.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.slide == true && pm.grounded == true && pm.rb.velocity.magnitude > 5)
        {
            score += (int)(scoreIncreaseSlide * multiplier);
        } else if (pm.slide == false && pm.grounded == false && pm.getGroundDist() > 3.5)
        {
            score += (int)(scoreIncreaseJump * multiplier);
        }
        print(pm.getGroundDist());
    }

    
}
