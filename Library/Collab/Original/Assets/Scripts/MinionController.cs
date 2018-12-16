using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour {

	NavMeshAgent navMeshAgent;
	Animator animator;
	public float approachDistance = 15, attackDistance = 3;
    bool approached;
    GameObject target;
    int health = 50;
    float UNDEFINED = -10000;
    float lastCollisionTime = -100.0f;
    float time = 0;
    float timeDied;
	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
        navMeshAgent.SetDestination(transform.position);
        approached = false;
        timeDied = UNDEFINED;
    }

    // Update is called once per frame
    void Update () {
        if (health <= 10) navMeshAgent.isStopped = true;
        if (health <= 0 && timeDied == UNDEFINED) timeDied = time;
        else if (health <= 0 && time - timeDied >= 4) {
            Destroy(GameController.minion);
            return;
        }
        GameController.minionHealthImage.fillAmount = Mathf.Max(0.0f, (1.0f * health) / 50);
        target = GameController.player;
        time += Time.deltaTime;
        Vector3 targetPosition = target.GetComponent<Transform>().position;
        bool canAttack = approached && Vector3.Distance(targetPosition, transform.position) <= attackDistance;
        approached = Vector3.Distance(targetPosition, transform.position) <= approachDistance;
        if(approached) navMeshAgent.SetDestination(targetPosition);
        animator.SetBool("attack", canAttack);
        animator.SetBool("Die", health <= 0);
        animator.SetBool("approach", approached);
        if (canAttack) navMeshAgent.isStopped = true;
        else if (!canAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) navMeshAgent.isStopped = false;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) animator.ResetTrigger("Hit");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool playerIsAttacking = GameController.khattabAnimator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") ||
                                    GameController.khattabAnimator.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack");
            Debug.Log(playerIsAttacking);
            if (time - lastCollisionTime <= 1 || !playerIsAttacking) return;
            lastCollisionTime = time;
            if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack")) health -= 10;
            else if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack")) health -= 30;
            KhattabController.rage += 10;
            Debug.Log("El Minion Et3awwar" + " , Minion Health = " + health);
            animator.SetTrigger("Hit");
        }
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >=
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
