using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gunPointer;
    public GameObject bulletPrefab;
    public int bulletPoolSize = 5;

    public float bulletSpeed = 5;

    GameObject player;
    private Queue<GameObject> bullets = new Queue<GameObject>();

    void Start()
    {
        player = transform.parent.gameObject;

        //Having to instantiate a new bullet each time the player shoots is slow
        //so creating a pool when the game starts helps with this.
        for (int i = 0; i < bulletPoolSize; i++)
        {
            bullets.Enqueue(Instantiate(bulletPrefab, new Vector3(-9999, -9999, 0), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void lookAt(Vector2 pos)
    {
        //Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (pos - (Vector2)transform.position).normalized;
        transform.up = direction;
    }

    public void destroyAllBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    public void shoot()
    {
        GameObject bullet = bullets.Dequeue();
        bullet.transform.position = new Vector3(-9999, -9999, 0);
        bullet.transform.position = gunPointer.transform.position;
        bullet.transform.eulerAngles = transform.eulerAngles;
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); // this is slow, but I cannot think of a better way at the moment
        bulletRB.velocity = player.GetComponent<Rigidbody2D>().velocity;
        bulletRB.AddForce((transform.up * bulletSpeed)*100);
        bullets.Enqueue(bullet); 
    }
}
