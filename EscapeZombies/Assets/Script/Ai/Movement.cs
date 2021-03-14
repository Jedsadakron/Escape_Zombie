using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public enum AIState
    {
        None,
        Idle,
        MoveAround,
        MoveToPlayer,
        Attack,
        Dead,
    }

    //randomMoveRange ระยะเดิน ai
    public float randomMoveRange = 3.0f;

    public Transform findingPlayerPoint;
    public AIState aistate;

    private Player status;
    private PlayerMovement target;
    private NavMeshAgent nav;
    private Animator anima;
    private AIState previosstate;

    private void Start()
    {
        //status

        nav = this.GetComponent<NavMeshAgent>();
        anima = this.GetComponent<Animator>();

        ChangeState(AIState.Idle);
    }

    private void OnEnable()
    {
        //status
    }

    private void OnDisable()
    {
        //status
    }

    public void ChangeState(AIState toSetState)
    {
        if (aistate == toSetState)
        {
            return;
        }

        previosstate = aistate;
        aistate = toSetState;

        switch(aistate)
        {
            case AIState.Idle:
                {
                    StartCoroutine(aiIdle());
                    break;
                }

            case AIState.MoveAround:
                {
                    StartCoroutine(aiMoveAround());
                    break;
                }

            case AIState.MoveToPlayer:
                {
                    StartCoroutine(aiMoveToPlayer());
                    break;
                }

            case AIState.Attack:
                {
                    StartCoroutine(aiAttack());
                    break;
                }

            case AIState.Dead:
                {
                    StartCoroutine(aiDead());
                    break;
                }
        }

    }

    private bool IsPlayerFound()
    {
        if (target)
            return true;

        RaycastHit hit;
        Ray ray = new Ray();

        ray.origin = findingPlayerPoint.position;
        ray.direction = findingPlayerPoint.forward;

        bool isHit = Physics.Raycast(ray, out hit, 100);
        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 100), Color.red);
        if(isHit)
        {
            target = hit.collider.gameObject.GetComponent<PlayerMovement>();
            if(target)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator aiIdle()
    {
        anima.SetBool("IsMove", false);
        nav.speed = 0.0f;
        nav.velocity = Vector3.zero;

        float waitTimeIdle = Random.Range(1.0f, 3.0f);
        while (true)
        {
            waitTimeIdle -= Time.deltaTime;

            if (IsPlayerFound())
            {
                ChangeState(AIState.MoveToPlayer);
                break;
            }
            if (waitTimeIdle <= 0)
            {
                ChangeState(AIState.MoveAround);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator aiMoveAround()
    {
        Vector3 randomPos = Vector3.zero;
        randomPos.x = Random.Range(this.transform.position.x - randomMoveRange, this.transform.position.x + randomMoveRange);
        randomPos.y = this.transform.position.y;
        randomPos.z = Random.Range(this.transform.position.z - randomMoveRange, this.transform.position.z + randomMoveRange);

        anima.SetBool("IsMove", true);

        float randomSpeed = Random.Range(0.25f, 1.5f);
        nav.speed = randomSpeed;

        while (true)
        {
            Vector3 targetPos = randomPos;

            float distFromTarget = Vector3.Distance(targetPos, this.transform.position);

            if (distFromTarget > nav.stoppingDistance)
            {
                nav.SetDestination(targetPos);

                anima.SetFloat("Movement", nav.velocity.magnitude / 1.5f);

                if (IsPlayerFound())
                {
                    ChangeState(AIState.MoveToPlayer);
                    break;
                }
            }
            else
            {
                ChangeState(AIState.Idle);
                break;
            }

            yield return null;
        }
    }

    private IEnumerator aiMoveToPlayer()
    {
        anima.SetBool("IsMove", true);

        float randomSpeed = Random.Range(0.25f, 1.5f);
        nav.speed = randomSpeed;

        while (true)
        {
            float distFromTarget = Vector3.Distance(this.transform.position, target.transform.position);

            if (distFromTarget > nav.stoppingDistance)
            {
                nav.SetDestination(target.transform.position);

                anima.SetFloat("Movement", nav.velocity.magnitude / 1.5f);
            }
            else
            {
                ChangeState(AIState.Attack);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator aiAttack()
    {
        status.playerHp -= 10;

        int randomIndexAtk = Random.Range(0, 2);

        anima.SetInteger("IndexAttack", randomIndexAtk);

        anima.SetTrigger("IsAttack");

        yield return null;
    }

    private IEnumerator aiDead()
    {
        anima.SetBool("IsDead", true);
        nav.speed = 0.0f;
        nav.velocity = Vector3.zero;
        Destroy(this.gameObject, 4.0f);

        yield return null;
    }

    public void OnDead()
    {
        StopAllCoroutines();
        ChangeState(AIState.Dead);
    }

}
