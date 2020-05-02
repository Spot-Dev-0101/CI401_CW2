using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;
    private Collider2D c;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "IgnoreBullet")
        {
            rb.velocity = Vector2.zero;
            transform.position = new Vector3(-9999, -9999, 0);
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().die();
            }
        }
    }
}
