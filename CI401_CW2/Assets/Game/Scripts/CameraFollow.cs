using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //private GameObject player;

    private Vector3 targetPos = new Vector3(0, 0, 0);

    private float z = 0;

    private float size = 5;

    private float targetSize = 5;
    
    private Camera camera;

    private float minSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("player");
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        float tempZ = -12.77f;
        if (z != 0)
        {
            tempZ = z;
        }
        Vector3 playerPos = new Vector3(5+targetPos.x, 1+targetPos.y, tempZ); //1+(targetPos.y/3)
        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, 0.05f);
        
        size -= (size - targetSize) / 10;
        camera.orthographicSize = size;
        

    }

    public void setTarget(Vector3 target, float z)
    {
        this.targetPos = target;
        this.z = z;
    }

    public void setTargetSize(float size)
    {
        if (size >= minSize)
        {
            targetSize = size;
        }
        
    }
}
