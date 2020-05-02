using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{

    public ParticleSystem blood;

    public Gun gun;

    private GameObject player;
    private float shootDistance = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < shootDistance)
        {
            gun.lookAt(player.transform.position);
        }
        
    }

    public void die()
    {
        blood.Play();
        gun.destroyAllBullets();
        Destroy(this.gameObject, 3);
    }
    
}
