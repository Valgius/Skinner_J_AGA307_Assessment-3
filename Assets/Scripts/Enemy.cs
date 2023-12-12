using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : GameBehaviour
{
    public static event Action<GameObject> OnEnemyHit = null;
    public static event Action<GameObject> OnEnemyDie = null;

    public PatrolType myPatrol;
    public float mySpeed = 1f;
    public float currentSpeed;

    int baseHealth = 100;
    int maxHealth;
    public int myHealth;
    public int myScore;
    public int myDamage = 20;

    [Header("AI")]
    public Transform moveToPos;     //Needed for all patrols
    Transform startPos;             //Needed for loop patrol movement
    Transform endPos;               //Needed for loop patrol movement
    bool reverse;                   //Needed for loop patrol movement
    public float attackDistance = 2;
    public float detectTime = 5f;
    public float detectDistance = 10f;
    int currentWaypoint;
    NavMeshAgent agent;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        SetupAI();
    }

    void SetupAI()
    {
        currentWaypoint = UnityEngine.Random.Range(0, _EM.patrolPoints.Length);
        agent.SetDestination(_EM.patrolPoints[currentWaypoint].position);
        ChangeSpeed(mySpeed);
    }

    void ChangeSpeed(float _speed)
    {
        agent.speed = _speed;
        currentSpeed = _speed;
    }

    private void Update()
    {
        if (myPatrol == PatrolType.Die)
            return; //cancels anything after this line

        //Always get the distance between the player and this object
        float distToPlayer = Vector3.Distance(transform.position, _PLAYER.transform.position);

        if (distToPlayer <= detectDistance && myPatrol != PatrolType.Attack)
        {
            if (myPatrol != PatrolType.Chase)
            {
                myPatrol = PatrolType.Detect;
            }
        }

        //Set the animators speed parameter to that of this objects speed.
        anim.SetFloat("Speed", currentSpeed);

        //Switching patrol states logic
        switch (myPatrol)
        {
            case PatrolType.Patrol:
                //Get distance between this object and current waypoint
                float distToWaypoint = Vector3.Distance(transform.position, _EM.patrolPoints[currentWaypoint].position);
                //If the distance is close enough, get a new waypoint
                if (distToWaypoint < 1)
                    SetupAI();
                //Reset the detect time
                detectTime = 5;
                break;
            case PatrolType.Detect:
                //Set the destination to this object, essentially stopping it
                agent.SetDestination(transform.position);
                //Stop our speed
                ChangeSpeed(0);
                //Decrement our detect time
                detectTime -= Time.deltaTime;
                if (distToPlayer <= detectDistance)
                {
                    myPatrol = PatrolType.Chase;
                    detectTime = 5;
                }
                if (detectTime <= 0)
                {
                    myPatrol = PatrolType.Patrol;
                    SetupAI();
                }
                break;
            case PatrolType.Chase:
                //Set the destination to that of the player
                agent.SetDestination(_PLAYER.transform.position);
                //increase the speed of which to chase the player
                ChangeSpeed(2);
                //If the player gets outside the detect distance, go back to the detect state.
                if (distToPlayer > detectDistance)
                    myPatrol = PatrolType.Detect; //single line If statements don't need brackets
                //Check if we are close to the player, then attack
                if (distToPlayer <= attackDistance)
                    StartCoroutine(Attack());
                break;
        }
    }

    IEnumerator Attack()
    {
        myPatrol = PatrolType.Attack;
        ChangeSpeed(0);
        PlayAnimation("Attack");
        yield return new WaitForSeconds(1);
        ChangeSpeed(mySpeed);
        myPatrol = PatrolType.Chase;
    }

    void Hit(int _damage)
    {
        myHealth -= _damage;

        if (myHealth <= 0)
        {
            Die();
        }
        else
        {
            int rnd = UnityEngine.Random.Range(1, 4);
            PlayAnimation("Hit");
            OnEnemyHit?.Invoke(this.gameObject);
        }
    }

    void Die()
    {
        myPatrol = PatrolType.Die;
        ChangeSpeed(0);
        GetComponent<Collider>().enabled = false;
        PlayAnimation("Die");
        StopAllCoroutines();
        OnEnemyDie?.Invoke(this.gameObject);
    }

    void PlayAnimation(string _animationName)
    {
        anim.SetTrigger(_animationName);
    }
}
