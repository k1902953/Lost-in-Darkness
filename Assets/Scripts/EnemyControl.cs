using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyControl : MonoBehaviour
{
    public NavMeshAgent agent;     // the nav mesh agent
    Animator anim;         // the animator
    Transform target;        // the target (player)
    public Transform startbase;    // location crawler is at the start of the game
    public float lookradius = 10f;

    // Start is called before the first frame update
    void Start()
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
            agent.SetDestination(startbase.transform.position);
            agent.isStopped = true;
            agent.speed = 0;
        }

        // Distance to the home base of the crawler 
        float basedis = Vector3.Distance(startbase.transform.position, transform.position);
        anim.SetFloat("basedis", basedis);

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
            //agent.SetDestination(target.position);
            agent.isStopped = false;
        }

        // If crawler is in back to base state
        if (asi.IsName("crawl"))
        {
            //agent.SetDestination(startbase.position);
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
            agent.SetDestination(startbase.transform.position);
            anim.SetFloat("basedis", Vector3.Distance(transform.position, startbase.transform.position));
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //lookRotation.x = 10.0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        //Quaternion rotation = Quaternion.LookRotation(direction);
        //transform.rotation = rotation;
    }
    public void Hit()
    {
        /*Debug.Log("exo is attacking");

        // tell the attacked game object it has been attacked
        GameManager.Instance.SendMessage("EnemyAttack", null, SendMessageOptions.DontRequireReceiver);*/
    }
}
