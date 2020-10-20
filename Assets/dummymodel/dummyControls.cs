using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyControls : MonoBehaviour
{
    private float pitch = 0;
    private float speed;
    public float walkSpeed = 0.02f;
    public float sprintSpeed = 0.06f;
    public float rotationSpeed = 2.5f;
    public Transform cameraTransform;

    private float yaw = 0;


    Animator anim;
    Rigidbody rigidBody;
    CapsuleCollider capsuleCollider;

    //static Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Vertical") * speed;
        float y = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);

        //for camera 
        yaw += rotationSpeed * Input.GetAxis("Mouse X");
        pitch -= rotationSpeed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(0, yaw, 0);
        cameraTransform.eulerAngles = new Vector3(pitch, yaw, 0);

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJumping");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
            }
            speed = sprintSpeed;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
            }
            speed = walkSpeed;
        }
    }
}