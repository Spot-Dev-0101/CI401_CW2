using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{


    public float jumpForce = 1;
    [SerializeField]
    private float speedLimit = 8;
    public bool slide = false;

    public CameraFollow cf;
    public AudioMixer audioMixer;

    
    private Rigidbody2D rb;
    private Collider2D collider;
    private SpriteAnimator spriteAnimator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (rb.velocity.magnitude < speedLimit && slide == false && Input.GetAxis("Horizontal") != 0)
        {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 25, 0));
            spriteAnimator.setFrameSet("run");
        }
        else
        {
            spriteAnimator.setFrameSet("idle");
        }

        if (Input.GetAxis("Vertical") > 0.25f && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce * 10));
        }

        if (IsGrounded() == false)
        {
            spriteAnimator.setFrameSet("fall");
        }

        if (Input.GetAxis("Vertical") <= -0.25)
        {
            slide = true;
            float groundAngle = getGroundAngle();
            transform.eulerAngles = new Vector3(0, 0, groundAngle);
            if (groundAngle < 0)
            {
                rb.AddForce(transform.right*2);
            } else if (groundAngle > 0)
            {
                rb.AddForce(-transform.right*2);
            }
            spriteAnimator.setFrameSet("slide");
        }
        else
        {
            slide = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        audioMixer.SetFloat("Momentum", Mathf.Lerp(0.5f, 1, rb.velocity.magnitude/10));
        audioMixer.SetFloat("Power", Mathf.Lerp(0.5f, 1, rb.velocity.magnitude / 10));
        audioMixer.SetFloat("Depth", Mathf.Lerp(0, 0.3f, rb.velocity.magnitude / 10));
        cf.setTarget(transform.position, 0);
        cf.setTargetSize(5 + getGroundDist());
        
    }

    private bool IsGrounded(){
        
        Vector2 left = new Vector2(transform.position.x-0.45f, transform.position.y);
        Vector2 right = new Vector2(transform.position.x+0.45f, transform.position.y);
        return Physics2D.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1f) || Physics2D.Raycast(left, -Vector3.up, collider.bounds.extents.y + 0.1f) || Physics2D.Raycast(right, -Vector3.up, collider.bounds.extents.y + 0.1f);
        //Collider2D[] hits = new Collider2D[1];
        //collider.OverlapCollider(new ContactFilter2D().NoFilter(), hits);
        //if (hits[0] != null)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
       // }

    }

    private float getGroundDist()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 25);
        if (hit)
        {
            return hit.distance;
        }
        return 0;
    }

    private float getGroundAngle()
    {
        float angle = 0;

        Vector2 left = new Vector2(transform.position.x - 0.45f, transform.position.y);
        Vector2 right = new Vector2(transform.position.x + 0.45f, transform.position.y);

        RaycastHit2D leftHit = Physics2D.Raycast(left, -Vector2.up, 1.5f);
        RaycastHit2D rightHit = Physics2D.Raycast(right, -Vector2.up, 1.5f);

        if (leftHit && rightHit) {
            float h = Vector2.Distance(leftHit.point, rightHit.point);
            float o = leftHit.point.y - rightHit.point.y;
            float a = leftHit.point.x - rightHit.point.x;

            angle = Mathf.Atan(o / h) * Mathf.Rad2Deg;
        }
        return -angle;
    }
    
}
