﻿using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class PlayerMove : MonoBehaviour
{
    public bool isPC;
    public bool isMobile;
    [Space(5)]
    //Mobile
    bool leftPress;
    bool rightPress;
    bool downPress;
    bool upPress;

    [Header("Land")]
    public float landSpeed;
    public bool onLand;

    [Header("Jump")]
    public float jumpForce;

    public float groundCheck;
    public bool grounded;
    public float airMoveSpeed;

    [Header("Water")]
    public float waterSpeed;
    public bool inWater;

    public float waterJumpForce;

    [Space(5)]
    public float velocityLimit;

    [Header("Dive")]
    public float diveForce;
    public float diveSpeed;
    public bool inSea;
    bool goDive;

    float diveTimer;

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

            animate.SetBool("jumpAni", false);

            FMOD.Studio.PLAYBACK_STATE play_Foot;
            footstepsSFX.getPlaybackState(out play_Foot);
            if (play_Foot != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                footstepsSFX.start();
            }

            if (isPC)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  // Left
                {
                    rig.AddForce(new Vector2(-landSpeed, 0));

                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Right
                {
                    rig.AddForce(new Vector2(landSpeed, 0));
                }
            }
            else if (isMobile)
            {
                if (leftPress == true && rightPress == false) //Left
                {
                    rig.AddForce(new Vector2(-landSpeed, 0));

                }
                else if (rightPress == true && leftPress == false) //Right
                {
                    rig.AddForce(new Vector2(landSpeed, 0));
                }

            }
        }
        else if (inWater)
        {
            // print("Water");

            animate.SetBool("InWaterAn", true);
            animate.SetBool("onGround", false);

            animate.SetBool("jumpAni", false);

            FMOD.Studio.PLAYBACK_STATE play_Water;
            waterSFX.getPlaybackState(out play_Water);
            if (play_Water != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                waterSFX.start();
            }

            if (isPC)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Left
                {
                    rig.AddForce(new Vector2(-waterSpeed, 0));

                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))  // Right
                {
                    rig.AddForce(new Vector2(waterSpeed, 0));

                    if (rig.velocity.x > velocityLimit)
                    {
                        rig.velocity = new Vector2(velocityLimit, rig.velocity.y);
                    }
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))  // Down/Dive
                {
                    diveTimer = 0;
                }

                //diving continues, count the timer and add the force accordigly
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))   // Dwon/Dive
                {

                    diveTimer += Time.deltaTime;

                    if (diveTimer >= 2f)
                    {
                        inSea = false;
                    }
                    else
                    {
                        inSea = true;
                    }

                    if (inSea == true)
                    {
                        rig.AddForce(new Vector2(0, -diveForce + (diveForce * diveTimer) / 2f));

                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))   // Left
                        {
                            rig.AddForce(new Vector2(-diveSpeed, 0));

                        }
                        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Right
                        {
                            rig.AddForce(new Vector2(diveSpeed, 0));
                        }

                    }
                    else
                    {
                        rig.AddForce(new Vector2(0, diveForce / 10));
                    }
                }
            }
            else if (isMobile)
            {
                if (leftPress == true && rightPress == false) // Left
                {
                    rig.AddForce(new Vector2(-waterSpeed, 0));

                }
                else if (rightPress == true && leftPress == false)  // Right
                {
                    rig.AddForce(new Vector2(waterSpeed, 0));

                    if (rig.velocity.x > velocityLimit)
                    {
                        rig.velocity = new Vector2(velocityLimit, rig.velocity.y);
                    }
                }

                if (downPress == true && upPress == false)  // Down/Dive
                {
                    diveTimer = 0;
                }

                //diving continues, count the timer and add the force accordigly
                if (downPress == true && upPress == false)   // Dwon/Dive
                {

                    diveTimer += Time.deltaTime;

                    if (diveTimer >= 2f)
                    {
                        inSea = false;
                    }
                    else
                    {
                        inSea = true;
                    }

                    if (inSea == true)
                    {
                        rig.AddForce(new Vector2(0, -diveForce + (diveForce * diveTimer) / 2f));

                        if (leftPress == true && rightPress == false)   // Left
                        {
                            rig.AddForce(new Vector2(-diveSpeed, 0));

                        }
                        else if (rightPress == true && leftPress == false) // Right
                        {
                            rig.AddForce(new Vector2(diveSpeed, 0));
                        }

                    }
                    else
                    {
                        rig.AddForce(new Vector2(0, diveForce / 10));
                    }
                }

            }

        }
        else if (inWater == false && onLand == false)
        {
            animate.SetBool("InWaterAn", false);


            waterSFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            footstepsSFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            if (isPC)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    rig.AddForce(new Vector2(-airMoveSpeed, 0));

                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    rig.AddForce(new Vector2(airMoveSpeed, 0));

                }
            }
            else if (isMobile)
            {
                if (leftPress == true && rightPress == false)   // Left
                {
                    rig.AddForce(new Vector2(-airMoveSpeed, 0));

                }
                else if (rightPress == true && leftPress == false)   // Right
                {
                    rig.AddForce(new Vector2(airMoveSpeed, 0));
                }
            }
        }

        if (isPC)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded || Input.GetKeyDown(KeyCode.W) && grounded || Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            {
                rig.AddForce(new Vector2(0, jumpForce));

                animate.SetBool("jumpAni", true);

            }
            else if (Input.GetKey(KeyCode.W) && inWater || Input.GetKey(KeyCode.Space) && inWater || Input.GetKey(KeyCode.UpArrow) && inWater)
            {
                rig.AddForce(new Vector2(0, waterJumpForce));


                animate.SetBool("jumpAni", true);
            }
        }
        else if (isMobile)
        {
            if (upPress == true && grounded && downPress == false || upPress == true && grounded && downPress == false || upPress == true && grounded && downPress == false)  // Up
            {
                rig.AddForce(new Vector2(0, jumpForce));

                animate.SetBool("jumpAni", true);

            }
            else if (upPress == true && inWater && downPress == false || upPress == true && inWater && downPress == false || upPress == true && inWater && downPress == false)   // Up
            {
                rig.AddForce(new Vector2(0, waterJumpForce));

                animate.SetBool("jumpAni", true);
            }
        }

    }

    public void LeftButton()
    {
        leftPress = true;
        rightPress = false;
    }

    public void LeftFalse()
    {
        leftPress = false;
    }

    public void RightButton()
    {
        rightPress = true;
        leftPress = false;
    }

    public void RightFalse()
    {
        rightPress = false;
    }

    public void UpButton()
    {
        upPress = true;
        downPress = false;
    }

    public void UpFalse()
    {
        upPress = false;
    }

    public void DownButton()
    {
        downPress = true;
        upPress = false;
    }

    public void DownFalse()
    {
        downPress = false;
    }


}
