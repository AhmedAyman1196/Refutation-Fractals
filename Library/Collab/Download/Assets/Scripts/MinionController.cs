using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    Image healthImage;
		bool paused = false ;
	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
				animator = GetComponent<Animator>();
        navMeshAgent.SetDestination(transform.position);
        approached = false;
        timeDied = UNDEFINED;
        healthImage = GameController.findChildWithTag(this.gameObject, "MinionHealthImage").GetComponent<Image>();

	  }

    // Update is called once per frame
    void Update () {
				if(KhattabController.paused){
					Time.timeScale = 0;
					return ;
				}else Time.timeScale =	1 ;

        if (health <= 0) navMeshAgent.isStopped = true;
        if (health <= 0 && timeDied == UNDEFINED)
        {
            KhattabController.MinionDied();
						GetComponent<MinionsAudio>().PlayDying();

            timeDied = time;
        }
        else if (health <= 0 && time - timeDied >= 4)
        {
            Level1Controller.kills++;
            Destroy(this.gameObject);
            return;
        }
        healthImage.fillAmount = Mathf.Max(0.0f, (1.0f * health) / 50);
        target = GameController.player;
        time += Time.deltaTime;
        Vector3 targetPosition = target.GetComponent<Transform>().position;
        bool canAttack = approached && Vector3.Distance(targetPosition, transform.position) <= attackDistance;
        approached = Vector3.Distance(targetPosition, transform.position) <= approachDistance;
        if(approached){
					GetComponent<MinionsAudio>().PlayDetect();
					navMeshAgent.SetDestination(targetPosition);
			}
        animator.SetBool("attack", canAttack);
        animator.SetBool("Die", health <= 0);
        animator.SetBool("approach", approached);
				if(!canAttack)
					GetComponent<MinionsAudio>().PlayWalking();

        if (canAttack) navMeshAgent.isStopped = true;
        else if (!canAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && health > 0) navMeshAgent.isStopped = false;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) animator.ResetTrigger("Hit");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool playerIsAttacking = GameController.khattabAnimator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") ||
                                    GameController.khattabAnimator.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack");
            if (time - lastCollisionTime <= 1 || !playerIsAttacking) return;
            lastCollisionTime = time;

            int damageMultiply = (KhattabController.isRaging ? 2 : 1 )+KhattabController.attack;
            if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack"))
						{
							 health -= damageMultiply * 10;
							 GetComponent<MinionsAudio>().PlayHit();
            }else if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
            {
							health -= damageMultiply * 30;
							KhattabController.rage += 10;
							GetComponent<MinionsAudio>().PlayHit();
            }
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
