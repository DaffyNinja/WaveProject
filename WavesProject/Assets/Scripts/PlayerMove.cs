using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    [Header("Land")]
    public float landSpeed;
    public bool onLand;

    [Header("Jump")]
    public float jumpForce;

    public float groundCheck;
    public bool grounded;


    [Header("Water")]
    public float waterSpeed;
    public bool inWater;

    public float diveForce;
    public bool inSea;
    bool goDive;

    float timer = 0f;

    // Add Dive

    Rigidbody2D rig;


    // Use this for initialization
    void Awake()
    {

        rig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheck), 1 << LayerMask.NameToLayer("Ground"));
        inWater = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheck), 1 << LayerMask.NameToLayer("Water"));
        inSea = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheck), 1 << LayerMask.NameToLayer("Sea"));

        if (grounded)
        {
            onLand = true;
           // rig.constraints = RigidbodyConstraints2D.None;
        }

        if (onLand)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-landSpeed, 0, 0);

            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(landSpeed, 0, 0);
            }
        }
        else if (inWater)
        {
           // print("Water");

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-waterSpeed, 0, 0);

            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(waterSpeed, 0, 0);
            }



            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (timer <= 0f)
                {
                    rig.AddForce(new Vector2(0, -diveForce));
                    timer = 2f;
                }
                //GetComponent<Collider2D>().enabled = false;
            }

            if(timer > 1)
            {
                rig.AddForce(new Vector2(0, -diveForce * ((timer - 1) / 1f)));
            }
            timer -= Time.deltaTime;

        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded || Input.GetKeyDown(KeyCode.W) && grounded || Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            rig.AddForce(new Vector2(0, jumpForce));

            transform.localEulerAngles = new Vector3(0, 0, 0);
         //   rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.layer == LayerMask.NameToLayer("Water") && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        //{

        //    Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //}
    }
        
}
