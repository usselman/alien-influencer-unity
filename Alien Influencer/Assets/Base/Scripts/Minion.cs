using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public enum MinionState
    {
        StartIdle,
        Idle,
        StartMovingToBuilding,
        MovingToBuilding,
        StartAttackingBuilding,
        AttackingBuilding,
        StartMovingToUFO,
        MovingToUFO
    }
    public MinionState state = MinionState.Idle;
    public enum AttackState
    {
        StartAttack,
        Attacking
    }
    public AttackState attackState = AttackState.StartAttack;
    public float DistanceToTarget;
    public Animator animator;
    public Transform houseTrans;
    public ParticleSystem attackParticles;
    public float walkingSpeed = 2.0f;
    Vector3 destination;
    Transform ufoTrans;
    void Start()
    {
        ufoTrans = GameObject.FindWithTag("UFO").transform;
    }
    void OnEnable()
    {
        MinionManager.OnNewBuildingSelected += MoveToBuilding;
    }
    void OnDisable()
    {
        MinionManager.OnNewBuildingSelected -= MoveToBuilding;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            FinishedAttackingBuilding();
        }
        switch (state)
        {
            case MinionState.StartIdle:
                StartIdle();
                break;
            case MinionState.Idle:
                Idle();
                break;
            case MinionState.MovingToBuilding:
                MovingToBuilding();
                break;
            case MinionState.StartAttackingBuilding:
                StartAttackingBuilding();
                break;
            case MinionState.AttackingBuilding:
                AttackingBuilding();
                break;
            case MinionState.StartMovingToUFO:
                StartMovingToUFO();
                break;
            case MinionState.MovingToUFO:
                MovingToUFO();
                break;
            default:
                break;
        }
    }

    void StartIdle()
    {
        animator.SetTrigger("StartIdle");
        state = MinionState.Idle;
    }
    void Idle()
    {
        //Idle
    }
    public void MoveToBuilding()
    {
        destination = MinionManager.Instance.SelectedBuilding.transform.position;
        transform.LookAt(destination);
        transform.position = Vector3.MoveTowards(transform.position, destination, walkingSpeed * Time.deltaTime);
        animator.SetTrigger("StartWalk");
        state = MinionState.MovingToBuilding;
    }
    void MovingToBuilding()
    {
        DistanceToTarget = Vector3.Distance(transform.position, destination);
        if (Vector3.Distance(transform.position, destination) < 1.0f)
        {
            state = MinionState.StartAttackingBuilding;
        }
    }
    void StartAttackingBuilding()
    {
        animator.SetTrigger("StartAttack");
        state = MinionState.AttackingBuilding;
    }
    void AttackingBuilding()
    {
        switch(attackState)
        {
            case AttackState.StartAttack:
                StartCoroutine(StallAttack());
                attackState = AttackState.Attacking;
                break;
            case AttackState.Attacking:
                break;
        }
    }
    public void FinishedAttackingBuilding()
    {
        animator.SetTrigger("StartWalk");
        state = MinionState.StartMovingToUFO;
    }
    void StartMovingToUFO()
    {
        animator.SetTrigger("StartWalk");
        destination = GameObject.Find("UFO").transform.position;
        state = MinionState.MovingToUFO;
    }
    void MovingToUFO()
    {
        Vector3 ufoGroundPosition = new Vector3(ufoTrans.position.x, transform.position.y, ufoTrans.position.z);
        transform.position = Vector3.MoveTowards(transform.position, ufoGroundPosition, walkingSpeed * Time.deltaTime);
        transform.LookAt(ufoGroundPosition);
        DistanceToTarget = Vector3.Distance(transform.position, ufoGroundPosition);
        if (Vector3.Distance(transform.position, ufoGroundPosition) < 1.0f)
        {
            state = MinionState.StartIdle;
        }
    }
    IEnumerator StallAttack()
    {
        attackParticles.Play();
        MinionManager.Instance.AttackBuilding();
        yield return new WaitForSeconds(1.3f);
        attackState = AttackState.StartAttack;
    }
}
