using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyControl : MonoBehaviour
{
    Animator anim;      // the animator
    NavMeshAgent na;    // the nav mesh agent
    public Transform target;        // the target (player)
    public Transform startbase;    // location crawler is at the start of the game
    bool found = false;            // found player

    // Start is called before the first frame update
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect the player
        float enemydist = Vector3.Distance(target.transform.position, transform.position);

        // Set fake distance if game over so enemy goes away
        if (GameManager.Instance.isGameOver())
        {
            enemydist = 10000;
            na.SetDestination(startbase.transform.position);
            na.isStopped = true;
            na.speed = 0;
        }

        //enemydist = 10000; //??

        anim.SetFloat("EnemyDist", enemydist);

        // Distance to the home base of the crawler NPC
        float basedist = Vector3.Distance(startbase.transform.position, transform.position);

        anim.SetFloat("BaseDist", basedist);

        // Determine which state crawler is in
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);

        // If crawler is in Attack state
        if (asi.IsName("attack"))
        {
            na.isStopped = true;
            na.velocity = Vector3.zero;
        }

        // If crawler is in run state
        if (asi.IsName("crawl_fast"))
        {
            na.SetDestination(target.position);
            na.isStopped = false;
        }

        // If crawler is in back to base state
        if (asi.IsName("crawl"))
        {
            na.SetDestination(startbase.position);
            na.isStopped = false;
        }

        // If crawler is in idle state
        if (asi.IsName("Idle"))
        {
            na.isStopped = true;
        }

        if (!found && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 2)
        {
            found = true;
        }

        if (found)
        {
            na.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
            anim.SetFloat("EnemyDist", Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
        }
        else
        {
            na.SetDestination(startbase.transform.position);
        }
    }
}
