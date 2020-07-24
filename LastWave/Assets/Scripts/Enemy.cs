using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity 
{
    // Start is called before the first frame update
    public enum State { Idle, Chasing, Attacking };
    State currentState;
    NavMeshAgent pathFinder;
    Transform target;
    Material skinMaterial;
    Color originalColour;

    float attackDistanceThreshold = .5f;
    float timeBetweenAttacks = 1;
    float nextAttackTime;
    float myCollisionRaius;
    float targetCollisionRadius;
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        currentState = State.Chasing;
        skinMaterial = GetComponent<Renderer>().material;
        originalColour = skinMaterial.color;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myCollisionRaius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        StartCoroutine(UpdatePath());

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRaius+ targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }
    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPostion = target.position - dirToTarget * (myCollisionRaius);
        float attackSpeed = 3;
        float percent = 0;
        skinMaterial.color = Color.red;
        while (percent <= 3)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPostion, interpolation);
            yield return null;
        }

        skinMaterial.color = originalColour;
        currentState = State.Chasing;
        pathFinder.enabled = true;

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f ;
        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRaius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }
                yield return new WaitForSeconds(refreshRate);
        }
    }
}
