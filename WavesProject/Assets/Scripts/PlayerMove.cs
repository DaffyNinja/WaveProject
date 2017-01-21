using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class PlayerMove : MonoBehaviour
{

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
    Animator animate;

    [FMODUnity.EventRefAttribute]
    FMOD.Studio.EventInstance waterSFX;
    FMOD.Studio.EventInstance footstepsSFX;
    FMOD.Studio.ParameterInstance musicEndParam;


    // Use this for initialization
    void Awake()
    {
        animate = GetComponent<Animator>();

        rig = GetComponent<Rigidbody2D>();

        waterSFX = FMODUnity.RuntimeManager.CreateInstance("event:/sfx/sfx_Swimming");
        waterSFX.getParameter("end", out musicEndParam);

        footstepsSFX = FMODUnity.RuntimeManager.CreateInstance("event:/sfx/sfx_Footsteps");
        footstepsSFX.getParameter("end", out musicEndParam);



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
        else
        {
            onLand = false;
        }

        if (onLand)
        {
            animate.SetBool("InWaterAn", false);
            animate.SetBool("onGround", true);

            FMOD.Studio.PLAYBACK_STATE play_Foot;
            footstepsSFX.getPlaybackState(out play_Foot);
            if (play_Foot != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                footstepsSFX.start();
            }



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

            animate.SetBool("InWaterAn", true);
            animate.SetBool("onGround", false);

            FMOD.Studio.PLAYBACK_STATE play_Water;
            waterSFX.getPlaybackState(out play_Water);
            if (play_Water != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                waterSFX.start();
            }

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
                if (timer <= 0f)
                {
                    rig.AddForce(new Vector2(0, -diveForce));
                    timer = 2f;
                }
                //GetComponent<Collider2D>().enabled = false;
            }

            if (timer > 1)
            {
                rig.AddForce(new Vector2(0, -diveForce * ((timer - 1) / 1f)));
            }
            timer -= Time.deltaTime;

        }
        else if (inWater == false && onLand == false)
        {
            animate.SetBool("InWaterAn", false);

            waterSFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            footstepsSFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        }


        if (Input.GetKeyDown(KeyCode.Space) && grounded || Input.GetKeyDown(KeyCode.W) && grounded || Input.GetKeyDown(KeyCode.UpArrow) && grounded || Input.GetKeyDown(KeyCode.W) && inWater)
        {
            rig.AddForce(new Vector2(0, jumpForce));

            animate.SetBool("jumpAni", true);

            transform.localEulerAngles = new Vector3(0, 0, 0);
            //   rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

}
