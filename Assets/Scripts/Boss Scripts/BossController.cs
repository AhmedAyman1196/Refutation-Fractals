using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class BossController : MonoBehaviour {

	NavMeshAgent navMeshAgent;
	public static Animator animator;
	public float attack1Dis , attack2Dis , attack3Dis , speed , rotationSpeed ;
	public static float health , lastCollisionTime = -100.0f;
	float attack1Time = 2.7f , attack2Time = 2.5f ,attack3Time = 3.5f ;
	float attackDistance , attackTime ;
	public static string attackName ;
	int nextAttacknum;
  public static float time = 5  , lastAttackTime = 0 , lastHitPlayerTime = 0;
	System.Random random ;

	public static GameObject rightHandBandage , leftHandBandage , legBandage ;
	Image healthImage;
	bool paused ;

	// Weakness points variables
	static int righLegHealth , rightArmHealth ,leftArmHealth = 3;

	public static bool  attacking ;

	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		random = new System.Random() ;
		nextAttacknum = (int)random.Next(1,4);

		Vector3 targetPosition = GameController.khattabHips.GetComponent<Transform>().position;
		navMeshAgent.SetDestination(targetPosition);
		righLegHealth=rightArmHealth = leftArmHealth  = 3 ;
		chooseAttack();
		attacking  = false ;
		speed = 5f ;
		rotationSpeed= 0.5f ;
		health = 200  ;
		legBandage = GameObject.FindWithTag("legBandage");
		leftHandBandage = GameObject.FindWithTag("leftHandBandage");
		rightHandBandage = GameObject.FindWithTag("rightHandBandage");
		leftHandBandage.SetActive(true);
		rightHandBandage.SetActive(true);
		legBandage.SetActive(true);
		healthImage = GameController.findChildWithTag(this.gameObject, "MinionHealthImage").GetComponent<Image>();
		paused = false ;
		}

    // Update is called once per frame
    void Update () {
						if(Input.GetKeyDown(KeyCode.Escape))
				paused = !paused;
			 if(paused){
				 	Time.timeScale = 0;
			 		return ;
		 		}else Time.timeScale = 1 ;

			time += Time.deltaTime;
			if(health<=0 ){
				animator.SetTrigger("death");
				GetComponent<BossAudio>().PlayDying();
				SceneManager.LoadScene("Credits");

				return ;
			}

			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Staggered"))
				GetComponent<BossAudio>().PlayDying();
			else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")){
					GetComponent<BossAudio>().PlayWalking();
			}


			if(KhattabController.paused){
				Time.timeScale = 0;
				return ;
			}else Time.timeScale =	1 ;

			healthImage.fillAmount = Mathf.Max(0.0f, (1.0f * health) / 200);



				Vector3 targetPosition = GameController.khattabHips.GetComponent<Transform>().position;
				navMeshAgent.SetDestination(targetPosition);
				RotateTowards(GameController.khattabHips.GetComponent<Transform>());

				bool canAttack =  navMeshAgent.remainingDistance <= attackDistance && time -lastAttackTime >= 5;
				if( attacking|| canAttack || navMeshAgent.remainingDistance <= attackDistance
				|| animator.GetCurrentAnimatorStateInfo(0).IsName("Staggered") || animator.GetCurrentAnimatorStateInfo(0).IsName("Asad Yala") ) {
					navMeshAgent.speed= 0 ;
					animator.SetBool("approach" , false);
				}
				else{
					animator.SetBool("approach" , true);
					navMeshAgent.speed = speed ;
				}


				if(!attacking && canAttack &&(leftArmHealth>0 || rightArmHealth >0 ||righLegHealth >0)){
					chooseAttack();
					lastAttackTime = time;
					animator.SetBool(attackName, true);
					attacking = true ;
				}

				if (time-lastAttackTime > attackTime) {
					attacking = false;
					animator.SetBool(attackName, false);
					KhattabController.bossExit();
 				}

    }

		void printAnimationsTime(){
			// used to get animation time on runtime
			 RuntimeAnimatorController ac = animator.runtimeAnimatorController;

			 for(int i = 0; i< ac.animationClips.Length; i++)                 //For all animations
			 	{
			 			Debug.Log(ac.animationClips[i].name +" "+ac.animationClips[i].length);
			 	}
		}

		private void RotateTowards (Transform target) {
			Vector3 direction = (target.position - transform.position).normalized;
     Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
		}

		public int chooseAttack(){
			while(true){
				nextAttacknum = (int)random.Next(1,4);
				if(nextAttacknum==1&&leftArmHealth>0)
					break ;
				if(nextAttacknum==2&&rightArmHealth>0)
					break ;
				if(nextAttacknum==3&&righLegHealth>0)
					break ;
			}

			if(nextAttacknum==1){
				attackDistance = attack1Dis ;
				attackName = "Attack1" ;
				attackTime = attack1Time ;
			}
			else if(nextAttacknum==2){
				attackDistance = attack2Dis ;
				attackName = "Attack2" ;
				attackTime = attack2Time;
			}
			else{
				attackDistance = attack3Dis ;
				attackName = "Attack3" ;
				attackTime = attack3Time;
			}

			return nextAttacknum ;
		}
		public static void rightLegHit(bool heavy , bool rage){

			if(righLegHealth<=0)
				return ;
			int damage = 1;
			if(heavy)damage+=1;
			if(rage)damage+=1;
			righLegHealth -= damage;

			if(righLegHealth<=0){
				animator.SetTrigger("stagger");
				 health-=40;
				 legBandage.SetActive(false);
				 Debug.Log("right leg Done");
			}


		}
		public static void rightArmHit(bool heavy , bool rage){

			if(rightArmHealth<=0)
				return ;
			int damage = 1;
			if(heavy)damage+=1;
			if(rage)damage+=1;
			rightArmHealth-= damage;
			health-= damage *10;
			Debug.Log("XXXX  : " +rightArmHealth);

			if(rightArmHealth<=0){
			 animator.SetTrigger("stagger");
			 health-= 40;
			 rightHandBandage.SetActive(false);
			 Debug.Log("right arm Done");

		 }
		}

		public static void leftArmHit(bool heavy , bool rage){
			if(leftArmHealth<=0)
				return;
			int damage = 1;
			if(heavy)damage+=1;
			if(rage)damage+=1;
			leftArmHealth -= damage ;
			health-= damage *10;
			Debug.Log("XXXX  : " +leftArmHealth);

			if(leftArmHealth<=0){
				animator.SetTrigger("stagger");
			 	health-=40;
			 	leftHandBandage.SetActive(false);
			 	Debug.Log("left  arm Done");
			}
		}

		public static void hitPlayer(){
			if(attacking&& time - lastHitPlayerTime > 5){
				lastHitPlayerTime = time ;
				KhattabController.bossHit() ;
			}
		}

		void OnTriggerEnter(Collider collision)
		{
				if (collision.gameObject.tag == "Player")
				{
						bool playerIsAttacking = GameController.khattabAnimator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack") ||
																		GameController.khattabAnimator.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack");
						if (time - lastCollisionTime <= 1 || !playerIsAttacking) return;
						lastCollisionTime = time;

						if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack")){
							health -= 10;
							Debug.Log("Edely bel light");
							Debug.Log("Health : "+health);
						}
						else if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack")){
							health -= 20;
							Debug.Log("Mesh bel heavy ya 3am");
						}
						KhattabController.rage += 10;
				}
		}



}
