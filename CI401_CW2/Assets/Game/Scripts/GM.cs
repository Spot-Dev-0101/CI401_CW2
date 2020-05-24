using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    [SerializeField]
    private PlayerMovement pm;

    private ScoreManager sm;

    private float m_timer = 0;

    public float timer
    {
        get { return m_timer; }
        set
        {
            m_timer = value;
            timerText.SetText(m_timer.ToString("F1"));
        }
    }

    [HideInInspector]
    public bool isGameOver = false;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private GameObject gameOverUIParent;
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    [SerializeField]
    private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        timer = 60;
        sm = GameObject.FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 || pm.health <= 0)
            {
                gameOver();
            }
            healthBar.transform.localScale = new Vector3(pm.health, 0.2f, 0);
        }
    }

    private void gameOver()
    {
        audioMixer.SetFloat("Momentum", 0);
        audioMixer.SetFloat("Power", 0);
        audioMixer.SetFloat("Depth", 0);
        ScoreText.text = "Score: " + sm.score;
        gameOverUIParent.SetActive(true);
        isGameOver = true;
    }
}
