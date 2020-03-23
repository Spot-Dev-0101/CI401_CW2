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

    private List<GameObject> currentLevels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        int playerPosIndex = Mathf.RoundToInt(player.transform.position.x / levels[0].length)+1;
        if (playerPosIndex > currentLevels.Count)
        {
            Vector2 pos = new Vector2(levels[0].length * playerPosIndex, 0);
            currentLevels.Add(Instantiate(levels[0].prefab, pos, Quaternion.identity));
        }


    }
}
