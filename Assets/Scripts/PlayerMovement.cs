using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    //Chaecks if player is touching the ground
    public bool isGrounded;
    public float groundDistance = 0.4f;
    public float gravity = -9.81f; //Real force of gravity in units
    public LayerMask groundMask;
    public Transform groundCheck;

    public float jumpHeight;

    int baseHealth = 500;
    int maxHealth;
    public int myHealth;
    HealthBar healthbar;

    private CharacterController controller;
    private Animator anim;
   
    AudioSource audioSource;
    public float stepRate = 0.5f;
    float stepCooldown;
    public AudioClip footstep;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        healthbar = GetComponentInChildren<HealthBar>();
        myHealth = maxHealth = baseHealth;
    }

    private void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }

        //Footstep Audio Stuff
        stepCooldown -= Time.deltaTime;
        if (stepCooldown < 0 && isGrounded)
        {
            stepCooldown = stepRate;
            _AM.PlaySound(footstep, audioSource);
        }

    }

    /// <summary>
    /// Moves the chracter
    /// </summary>
    private void Move()
    {
        //Checks if we are touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //creates sphere at foot of player to see if they are touching the ground.
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        //Get input for player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, z);
        moveDirection = transform.TransformDirection(moveDirection);

        if(isGrounded)
        {
            //Checks to see if player is walking or running
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
                Jump();
        }

        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        anim.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);;
    }

    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);


        if (_PEC.weaponType == "1H")
        {
            anim.SetTrigger("Attack1H");

            yield return new WaitForSeconds(1f);
        }
        if (_PEC.weaponType == "2H")
        {
            anim.SetTrigger("Attack2H");
            yield return new WaitForSeconds(1.5f);
        }
        if (_PEC.weaponType == "Unarmed")
        {
            anim.SetTrigger("Unarmed");
            yield return new WaitForSeconds(1f);
        }

        _AM.PlaySound(_AM.GetAttackSound(), audioSource);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }

    private void Block()
    {
        anim.SetTrigger("Block");
    }

    public void Hit(int _damage)
    {
        myHealth -= _damage;
        healthbar.UpdateHealthBar(myHealth, maxHealth);
        anim.SetTrigger("Hit");
        _AM.PlaySound(_AM.GetHitSound(), audioSource);

        if (myHealth < 0)
        {
            _AM.PlaySound(_AM.GetDieSound(), audioSource);
            _UI.EndGame();
        }

    }
}
