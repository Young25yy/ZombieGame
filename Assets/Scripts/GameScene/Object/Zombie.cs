using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private int hp;
    public bool isDead;
    private bool isMove;
    private ZombieInfo zombieInfo;
    private float atkCD = 0;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
        {
            animator = transform.AddComponent<Animator>();
        }
        if (agent == null)
        {
            agent = transform.AddComponent<NavMeshAgent>();
        }
    }
    public void InitZombie(ZombieInfo info)
    {
        zombieInfo = info;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(zombieInfo.animatorRes);
        hp = zombieInfo.hp;
        atkCD = zombieInfo.atkOffset;
        agent.speed = zombieInfo.moveSpeed;
        agent.angularSpeed = zombieInfo.rotateSpeed;
        agent.acceleration = 1000;
    }
    public void Wound(int damage)
    {
        if (isDead) return;
        hp -= damage;
        if (hp <= 0)
        {
            Dead();
        }
        else
        {
            animator.SetTrigger("wound");
            GameDataMgr.Instance.PlaySound("Music/Wound");
        }
    }
    public void Dead()
    {
        hp = 0;
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("isDead", true);
        GameDataMgr.Instance.PlaySound("Music/dead");
    }
    public void DeadEvent()
    {
        GameLevelMgr.Instance.RemoveZombie(this);
        GameLevelMgr.Instance.player.AddMoney(10);
        if (GameLevelMgr.Instance.CheckOver())
        {
            UIManager.Instance.ShowPanel<GameOverPanel>()
            .SetInfo(true, GameLevelMgr.Instance.player.money);
        }
        Destroy(gameObject);
    }
    public void BornOverEvent()
    {
        agent.SetDestination(Shelter.Instance.transform.position);
        animator.SetBool("isMove", true);
    }
    private void Update()
    {
        if (isDead) return;
        animator.SetBool("isMove", agent.velocity.magnitude > 0.1f);
        atkCD += Time.deltaTime;
        if (Vector3.Distance(transform.position, Shelter.Instance.transform.position)< 5f &&
            atkCD >= zombieInfo.atkOffset)
        {
            atkCD = 0;
            animator.SetTrigger("atk");
        }
    }
    public void AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * 0.5f + transform.up * 1f,
                                0.5f, 1 << LayerMask.NameToLayer("Shelter"));
        GameDataMgr.Instance.PlaySound("Music/Eat");
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == Shelter.Instance.gameObject)
            {
                Shelter.Instance.Wound(zombieInfo.atk);
            }
        }
    }
}
