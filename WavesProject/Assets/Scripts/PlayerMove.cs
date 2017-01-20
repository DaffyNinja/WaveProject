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
                rig.AddForce(new Vector2(-landSpeed, 0));

            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rig.AddForce(new Vector2(landSpeed, 0));
            }
        }
        else if (inWater)
        {
           // print("Water");

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rig.AddForce(new Vector2(-waterSpeed, 0));

            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rig.AddForce(new Vector2(waterSpeed, 0));

            }



            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                rig.AddForce(new Vector2(0, -diveForce));

                //GetComponent<Collider2D>().enabled = false;
            }

            if(timer > 0)
            {
                rig.AddForce(new Vector2(0, -diveForce * (timer / 2f)));
                timer -= Time.deltaTime;
            }
         
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded || Input.GetKeyDown(KeyCode.W) && grounded || Input.GetKeyDown(KeyCode.UpArrow) && grounded || Input.GetKeyDown(KeyCode.W) && inWater)
        {
            rig.AddForce(new Vector2(0, jumpForce));

            transform.localEulerAngles = new Vector3(0, 0, 0);
         //   rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
        
}
