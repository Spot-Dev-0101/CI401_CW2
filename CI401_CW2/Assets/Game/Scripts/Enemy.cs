using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    
    private GameObject player;
    public float shootDistance = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (triggeredDeath == false)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < shootDistance)
            {
                gun.lookAt(player.transform.position);
            }
            if (health <= 0)
            {
                die();
            }
        }
    }

    
    
}
