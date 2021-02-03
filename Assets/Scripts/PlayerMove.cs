using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    private Animation ani;

    public float RunMultiplier = 0.002f;   // Speed when sprinting
    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    public Transform gCheck;
    public float gDistance = 0.4f;
    public LayerMask gMask;
    bool isGrounded;

    public Vector3 velocity;
    //public KeyCode RunKey;

    void Start()
    {
        ani = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (Input.GetKey(KeyCode.C))
        {
            speed *= RunMultiplier;
        }
        else
        {
            m_Running = false;
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            speed *= RunMultiplier;
            //this.gameObject.GetComponent<Animation>().CrossFade("Run", 1);
            ani.Play("Run");
        }
        if(!Input.GetMouseButtonDown(0))
        {
            speed = 6f;
            //this.gameObject.GetComponent<Animation>().CrossFade("Idle", 1);
            ani.Play("Idle");
        }
        
        isGrounded = Physics.CheckSphere(gCheck.position, gDistance, gMask);
        if (isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; 

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {   
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        
    }
}
