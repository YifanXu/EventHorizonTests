using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    Vector3 playerPosition;

    public float debugging;


    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float dashSpeed = 4;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;
    public float fireRate = 0;
    public float fireDelay = 0;
    float timeToFire;
    public float attackDelay = 0.5f;
    float timeToAttack;
    Vector3 firePoint;
    public LayerMask whatToHit;
    public float sprintMod;
    public bool canDash = true;
    public float dashDelay = 0;
    public float dashStep = 0;
    public bool canSprint = false;
    public bool doubleJump = false;
    public bool doubleJumped = false;
    public bool attacking;
    public GameObject weapon;



    public GameObject bulletPreFab;

    public bool walljump = true;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public float wallSlideSpeedMax = 3;
    public float wallStickTIme = .25f;
    float timeToWallUnstick;


    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityxSmoothing;


    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight / Mathf.Pow(timeToJumpApex, 2));
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }
    void Update()
    {
        debugging = Input.GetAxisRaw("Horizontal");

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirectionX = (controller.collisions.left) ? -1 : 1;
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            float targetVelocityX = input.x * moveSpeed * sprintMod;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityxSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        }
        else
        {
            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityxSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        }

        if (controller.collisions.below && doubleJumped)
        {
            doubleJumped = false;
        }
        /* what the fuck 
        if (dashDelay >= 1)
        {
            dashDelay = 0;
            dashStep = 0;
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && dashStep != 10)
        {
            dashStep = 0;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5 && dashStep == 0)
        {
            dashDelay += Time.deltaTime;
            dashStep = 10;
        }
        if (Input.GetAxisRaw("Horizontal") < 0.5 && Input.GetAxisRaw("Horizontal") >= 0 && dashStep == 10)
        {
            dashStep = 200;
        }
        else if (Input.GetAxisRaw("Horizontal") > -0.5 && Input.GetAxisRaw("Horizontal") <= 0 && dashStep == 10)
        {
            dashStep = 200;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.5 && dashStep == 200 && dashDelay <= 1)
        {
            velocity.x += dashSpeed;
            dashStep = 3000;
        }
        if (Input.GetAxisRaw("Horizontal") < -0.5 && dashStep == 200 && dashDelay <= 1)
        {
            velocity.x -= dashSpeed;
            dashStep = 3000;
        }
        */
       


        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below )
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityxSmoothing = 0;
                velocity.x = 0;
                if (input.x != wallDirectionX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTIme;
                }

            }
            else
            {
                timeToWallUnstick = wallStickTIme;
            }
           

        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            if (wallSliding && walljump)
            {
                if (wallDirectionX == input.x)
                {
                    velocity.x = -wallDirectionX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0)
                {
                    velocity.x = -wallDirectionX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirectionX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }
            if (wallSliding && !walljump)
            {
                velocity.x = -wallDirectionX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            if (!doubleJump)
            {
                if (controller.collisions.below)
                {
                    velocity.y = maxJumpVelocity;
                }
                if (!controller.collisions.below)
                {
                    velocity.y = velocity.y;   
                }

            }
            if (doubleJump)
            {
                if (controller.collisions.below || controller.collisions.left || controller.collisions.right)
                {
                    velocity.y = maxJumpVelocity;
                    doubleJumped = false;
                }
                if (!controller.collisions.below && !controller.collisions.left && !controller.collisions.right && !doubleJumped)
                {
                    velocity.y = maxJumpVelocity;
                    doubleJumped = true;
                }
                
                
                
            }
            
            
            else if (!controller.collisions.below && !doubleJump)
            {

            }
            
            if (controller.collisions.gainDoubleJump)
            {
                doubleJump = true;
                
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
            
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);



        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        playerPosition = transform.position;
        


    }
    void Shoot()
    {
        /*
        firePoint = transform.position;
        Vector2 mouseposition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.x, firePoint.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mouseposition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mouseposition-firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);

        }
        */
    }
    
}﻿
    




