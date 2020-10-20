using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;

    private float speed = 15.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    private float animationDuration = 3.0f;
    private float startTime;

    private bool isDead = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
            return;

        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        //x - left and right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetMouseButton(0))
        {
            //Are we holding the touch on right side?
            if (Input.mousePosition.x > Screen.width / 2)
                moveVector.x = speed;
            else
                moveVector.x = -speed;
        }

        //Screen.width

        //y - up and down
        moveVector.y = verticalVelocity;

        //z - forward and backward
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float modifier)
    {
        speed = 15.0f + modifier;
    }

    //Called when capsule hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((hit.point.z > transform.position.z + (controller.radius * 40)) && hit.gameObject.tag == "Enemy")
            Death();
    }

    private void Death()
    {
        Debug.Log("Death");
        isDead = true;
        GetComponent<Score>().OnDeath();
    }



}