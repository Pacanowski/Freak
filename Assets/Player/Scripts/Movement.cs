using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using TMPro;

public class Movement : MonoBehaviour
{

    public Camera cam;
    Rigidbody rb;

    InputAction jump;
    InputAction move;
    InputAction look;

    InputAction sprint;


    bool onGround;

    float lookX;
    float lookY;

    float sens = 0.05f;

    float maxVelocity = 5f;

    float sprintSpeed;

    public TMP_Text velocity;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = InputSystem.actions.FindAction("Jump");
        move = InputSystem.actions.FindAction("Move");
        look = InputSystem.actions.FindAction("Look");
        sprint = InputSystem.actions.FindAction("Sprint");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {

        velocity.text = "Velocity: " + Math.Round(rb.linearVelocity.x, 3).ToString();

        cam.transform.position = gameObject.transform.position;


        Vector2 moveValue = move.ReadValue<Vector2>();
        Vector2 lookValue = look.ReadValue<Vector2>();



        lookX += lookValue.x * sens;
        lookY += lookValue.y * sens;



        lookY = Mathf.Clamp(lookY, -45f, 45f);

        if (sprint.IsPressed())
        {
            sprintSpeed = 5f;
        }
        else
        {
            sprintSpeed = 1f;
        }

        if (moveValue.x != 0 || moveValue.y != 0)
        {
            Vector3 moveDir = transform.forward * moveValue.y + transform.right * moveValue.x;
            rb.AddForce(moveDir.normalized * 2 * sprintSpeed, ForceMode.Force);

            //Debug.Log("x: " + moveValue.x + "    y: " + moveValue.y);
        }

        transform.rotation = Quaternion.Euler(0f, lookX, 0f);

        cam.transform.localRotation = Quaternion.Euler(-lookY, 0f, 0f);

        //Debug.Log("x: " + lookValue.x + "    y: " + lookValue.y);

        if (jump.IsPressed() && onGround)
        {
            rb.AddForce(0f, 5f, 0f);
        }


        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            Vector3 horizontalVelocity = rb.linearVelocity;
            horizontalVelocity.y = 0f;

            if (horizontalVelocity.magnitude > maxVelocity)
            {
                horizontalVelocity = horizontalVelocity.normalized * maxVelocity;
                rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);
            }
        }




    }








    //Collisions


    public void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
}