using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    public float health = 1;

    public ParticleSystem blood;

    public Gun gun;
    
    protected private ScoreManager sm;

    [HideInInspector]
    public bool triggeredDeath = false;
    
    protected private GM gm;

    // Start is called before the first frame update
    protected void Start()
    {
        sm = GameObject.FindObjectOfType<ScoreManager>();
        gm = GameObject.FindObjectOfType<GM>();
    }
    

    public void damage(int amount)
    {
        blood.Play();
        health -= amount;
        if (health <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void die()
    {
        gun.destroyAllBullets();
        Destroy(this.gameObject, 3);
        triggeredDeath = true;

    }
}
