using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyCtrl : MonoBehaviour
{
    public PlayerCtrl playerCtrl;
    public Slider HPBar;
    public State state;

    float distance;
    float attackDelayTime;

    Vector3 idleLocation;

    Animator anim;
    NavMeshAgent nav;
    Enemy_Bandit01Stat _stat;
    PlayerQuests playerQuests;


    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        _stat = GetComponent<Enemy_Bandit01Stat>();
        HPBar = transform.Find("Canvas").Find("HPBar").GetComponent<Slider>();
        idleLocation = transform.position;
        state = State.Idle;
    }


    void Update()
    {
        if (state == State.Death)
        {
            return;
        }
        if(playerCtrl != null)
        {
            if (playerCtrl.isGameOver)
            {
                state = State.ReturnToIdle;
                nav.destination = idleLocation;
                nav.isStopped = false;
                anim.SetBool("isWalking", true);
            }
        }
        switch (state)
        {
            case State.Follow:
                Follow();
                break;
            case State.Attack:
                Attack();
                break;
            case State.ReturnToIdle:
                ReturnToIdle();
                break;
        }

    }



    void Follow()
    {
        distance = Vector3.Distance(transform.position, playerCtrl.transform.position);

        if (distance >= 8f)
        {
            state = State.ReturnToIdle;
            nav.destination = idleLocation;
            nav.isStopped = false;
            anim.SetBool("isWalking", true);

        }
        else if (distance <= 0.9f)
        {
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            attackDelayTime = 3;
            state = State.Attack;
            anim.SetBool("isWalking", false);
        }
        else
        {
            nav.destination = playerCtrl.transform.position;
            nav.isStopped = false;
            anim.SetBool("isWalking", true);
        }
    }

    void Attack()
    {
        attackDelayTime += Time.deltaTime;
        if (attackDelayTime >= 3f)
        {
            anim.SetTrigger("attack");

            attackDelayTime = 0;
        }

        distance = Vector3.Distance(transform.position, playerCtrl.transform.position);

        if (distance >= 1f)
        {
            state = State.Follow;
        }
    }

    void GiveDamage()
    {
        playerCtrl.TakeDamage(_stat.AttackPower);
    }



    public void TakeDamage(float damage)
    {
        if (damage > _stat.DefencePower)
        {
            _stat.HP -= damage - _stat.DefencePower;
            HPBar.value = _stat.HP / _stat.MaxHP;
            if (_stat.HP <= 0)
            {
                anim.SetTrigger("death");
                Death();
            }
            else
            {
                anim.SetTrigger("damaged");
                Destroy(gameObject, 5.0f);
            }
        }
    }


    void DamagedAnimOver()
    {
        state = State.Follow;
    }


    void Death()
    {
        state = State.Death;
        playerQuests = playerCtrl.GetComponent<PlayerQuests>();
        

        for(int i = 0; i < playerQuests.questList_Kill.Count; i++)
        {
            if(_stat.EnemyNum == playerQuests.questList_Kill[i].enemyNum)
            {
                if (playerQuests.questList_Kill[i].currentKillCount != playerQuests.questList_Kill[i].targetKillCount)
                {
                    playerQuests.questList_Kill[i].currentKillCount++;
                }
            }
        }

    }


    void ReturnToIdle()
    {
        HPBar.gameObject.SetActive(false);
        distance = Vector3.Distance(transform.position, idleLocation);
        if (distance <= 0.5f)
        {
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            state = State.Idle;
            playerCtrl = null;
            anim.SetBool("isWalking", false);
        }

    }

    public enum State
    {
        Idle,
        Follow,
        Attack,
        Damaged,
        Death,
        ReturnToIdle
    }

}
