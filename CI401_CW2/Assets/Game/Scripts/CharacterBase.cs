using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{

    public float health = 1;

    public ParticleSystem blood;

    public Gun gun;
    
    public ScoreManager sm;

    [HideInInspector]
    public bool triggeredDeath = false;

    // Start is called before the first frame update
    protected void Start()
    {
        sm = GameObject.FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void damage(int amount)
    {
        blood.Play();
        health -= amount;
    }

    public void die()
    {
        gun.destroyAllBullets();
        Destroy(this.gameObject, 3);
        triggeredDeath = true;

    }
}
