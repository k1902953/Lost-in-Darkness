using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyControl : MonoBehaviour
{
    NavMeshAgent agent;     // the nav mesh agent
    Animator anim;         // the animator
    Transform target;               // the target (player)
    public float lookradius = 14f;
    //
    public LayerMask ground;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Detect the player
        float enemydist = Vector3.Distance(target.position, transform.position);
        anim.SetFloat("distance", enemydist);

        // Set fake distance if game over so enemy goes away
        if (GameManager.Instance.isGameOver())
        {
            enemydist = 10000;
            agent.isStopped = true;
        }

        // Determine which state crawler is in
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);

        // If crawler is in Attack state
        if (asi.IsName("attack"))
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // If crawler is in run state
        if (asi.IsName("crawl_fast"))
        {
            agent.isStopped = false;
        }

        // If crawler is in back to base state
        if (asi.IsName("crawl"))
        {
            agent.isStopped = false;
        }

        // If crawler is in idle state
        if (asi.IsName("Idle"))
        {
            agent.isStopped = true;
        }

        if (enemydist <= lookradius)
        {
            agent.SetDestination(target.position);
            anim.SetFloat("distance", Vector3.Distance(transform.position, target.position));
            
            if (enemydist <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
        else
        {
            Patroling();   
        }
        
    }
    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        
    }
    
    public void Hit()
    {
        // tell the attacked game object it has been attacked
        GameManager.Instance.SendMessage("EnemyAttack", null, SendMessageOptions.DontRequireReceiver);
    }

    public void Patroling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walk point reached
        if(distanceToWalkPoint.magnitude < 10f)
        {
            walkPointSet = false;
        }
    }
    public void SearchWalkPoint()
    {
        float xrandom = Random.Range(-walkPointRange, walkPointRange);
        float zrandom = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + xrandom, transform.position.y, transform.position.z + zrandom);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, ground))
        {
            walkPointSet = true;
        }
    }
}
