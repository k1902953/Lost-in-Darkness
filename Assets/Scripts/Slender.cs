// #NVJOB Nicholas Veselov - https://nvjob.github.io
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Slender : MonoBehaviour
{
    NavMeshAgent agent;     // the nav mesh agent
    public SkinnedMeshRenderer body;
    public Transform target;
    public Transform head;
    public float blendTime = 0.4f;
    public float towards = 5.0f;
    public float weightMul = 1;
    public float clampWeight = 0.5f;
    public Vector3 weight = new Vector3(0.4f, 0.8f, 0.9f);
    public bool yTargetHeadSynk;

    //--------------
    public float lookradius = 21f;
    Transform tr;
    Animator anim;
    AudioSource music;
    Vector3 lookAtTargetPosition, lookAtPosition;
    float lookAtWeight;
    float timeFaceCh, facepWeight = 100, timeFace = 10;
    bool faceCh = true;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        tr = transform;
        anim = GetComponent<Animator>();
        music = GetComponent<AudioSource>();
        lookAtTargetPosition = target.position + tr.forward;
        lookAtPosition = head.position + tr.forward;
        agent = GetComponent<NavMeshAgent>();

    }

    

    void Update()
    {
        float playerdist = Vector3.Distance(target.position, transform.position);
        anim.SetFloat("dist", playerdist);

        // Determine which state crawler is in
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);

        // If crawler is in Attack state
        if (asi.IsName("Attack"))
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // If crawler is in run state
        if (asi.IsName("Run"))
        {
            agent.isStopped = false;
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 10);
            anim.SetFloat("dist", Vector3.Distance(transform.position, target.position));
        }

        // If crawler is in back to base state
        if (asi.IsName("Scream"))
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // If crawler is in idle state
        if (asi.IsName("Idle"))
        {
            agent.isStopped = true;
        }

        lookAtTargetPosition = target.position + tr.forward;
        if (playerdist <= lookradius)
        {
            anim.SetBool("Seen", true);
            if (faceCh == true && timeFace < Time.time)
            {
                
                timeFaceCh += Time.deltaTime * 80;
                if (timeFaceCh >= facepWeight * 2)
                {
                    timeFaceCh = 0;
                    faceCh = true;
                    timeFace = Time.time + Random.Range(3.0f, 6.0f);
                    music.pitch = Random.Range(0.8f, 1.0f);
                }
                float var0 = Mathf.PingPong(timeFaceCh, facepWeight);
                body.SetBlendShapeWeight(0, var0);
                music.volume = var0 * 0.1f;
            }
        }
    }

    public void Smash()
    {
            // tell the attacked game object it has been attacked
        GameManager.Instance.SendMessage("SlenderAttack", null, SendMessageOptions.DontRequireReceiver);
        StartCoroutine(Times());
    }

    void OnAnimatorIK()
    {
        if (yTargetHeadSynk == false) lookAtTargetPosition.y = head.position.y;
        Vector3 curDir = lookAtPosition - head.position;
        curDir = Vector3.RotateTowards(curDir, lookAtTargetPosition - head.position, towards * Time.deltaTime, float.PositiveInfinity);
        lookAtPosition = head.position + curDir;
        lookAtWeight = Mathf.MoveTowards(lookAtWeight, 1, Time.deltaTime / blendTime);
        anim.SetLookAtWeight(lookAtWeight * weightMul, weight.x, weight.y, weight.z, clampWeight);
        anim.SetLookAtPosition(lookAtPosition);

    }

    IEnumerator Times()
    {
        yield return new WaitForSeconds(2f);
        Destroy(transform.gameObject);
    }
}