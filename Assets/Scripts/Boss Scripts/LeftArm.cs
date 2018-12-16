﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : MonoBehaviour {

	float lastCollisionTime = -100.0f,time = 5  ;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Khattab"&&BossController.attackName == "Attack1" )
		{
				BossController.hitPlayer();
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

					if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("LightAttack"))
						BossController.leftArmHit(false , KhattabController.isRaging) ;
					else if (KhattabController.anim.GetCurrentAnimatorStateInfo(0).IsName("HeavyAttack"))
						BossController.leftArmHit(true , KhattabController.isRaging) ;

					KhattabController.rage += 10;
			}
	}
	}
