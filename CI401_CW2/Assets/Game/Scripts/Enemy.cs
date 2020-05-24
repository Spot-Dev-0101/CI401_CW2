using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    
    private GameObject player;
    public float shootDistance = 5;

    public float maxShootDelay = 3;
    public float minShootDelay = 0.5f;
    private float shootDelay = 2;

    private float timer = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggeredDeath == false && gm.isGameOver == false)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < shootDistance)
            {
                gun.lookAt(player.transform.position);
            }
            if (health <= 0)
            {
                die();
                sm.multiplier = 2;
                sm.score += (int)(sm.scoreIncreaseEnemyKill*sm.multiplier);
            }

            timer += Time.deltaTime;
            if (timer >= shootDelay)
            {
                gun.shoot();
                shootDelay = Random.Range(minShootDelay, maxShootDelay);
                timer = 0;
            }
        }
    }
    
}
