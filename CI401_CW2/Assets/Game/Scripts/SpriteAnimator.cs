using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class FrameSet
{
    public string name;
    public Sprite[] frames;
}

public class SpriteAnimator : MonoBehaviour
{

    [SerializeField]private FrameSet[] frameSets;
    public float delay = 1;

    private int currentFrame;
    private FrameSet currentFrameSet;
    private float timer;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentFrameSet = frameSets[0];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % currentFrameSet.frames.Length;
            sr.sprite = currentFrameSet.frames[currentFrame];
        }
    }

    public bool setFrameSet(string setName)
    {
        foreach (FrameSet fs in frameSets)
        {
            if (fs.name == setName)
            {
                currentFrameSet = fs;
                return true;
            }
        }
        return false;
    }


}
