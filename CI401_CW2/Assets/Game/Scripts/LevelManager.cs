using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public GameObject prefab;
    public float length;
}

public class LevelManager : MonoBehaviour
{

    public Level[] levels;

    private GameObject player;

    //private List<GameObject> currentLevels = new List<GameObject>();

    private Level previousLevel;

    private float totalLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().gameObject;
        previousLevel = levels[0];
        totalLength = levels[0].length;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (((player.transform.position.x - totalLength) + previousLevel.length) > 0)//(playerPosIndex > currentLevels.Count)
        {
            int randomIndex = Random.Range(0, levels.Length);
            
            
            Vector2 pos = new Vector2(totalLength, 0);
            Instantiate(levels[randomIndex].prefab, pos, Quaternion.identity);
            previousLevel = levels[randomIndex];
            totalLength += levels[randomIndex].length;
        }


    }
}
