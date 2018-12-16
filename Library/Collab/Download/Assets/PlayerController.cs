using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetKey(KeyCode.W)){
			transform.position=new Vector3(transform.position.x,transform.position.y,transform.position.z+0.1f);
			anim.SetBool("Running",true);
		}else{
			anim.SetBool("Running",false);
		}
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.01f);
            anim.SetBool("RunningBack", true);
        }
        else
        {
            anim.SetBool("RunningBack", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z );
            anim.SetBool("Right", true);
        }
        else
        {
            anim.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z );
            anim.SetBool("Left", true);
        }
        else
        {
            anim.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.Space)){
            anim.SetBool("Jumping", true);
        }
        else{
           GetComponent<Rigidbody>().AddForce(new Vector3(0, -500, 0));

            anim.SetBool("Jumping",false);
		}

		 if (Input.GetKey(KeyCode.E)){

			anim.SetBool("LightAttack",true);
		}else{
			anim.SetBool("LightAttack",false);
		}
		 if (Input.GetKey(KeyCode.Q)){

			anim.SetBool("HeavyAttack",true);
		}else{
			anim.SetBool("HeavyAttack",false);
		}
		if (Input.GetKey(KeyCode.Z)){

			anim.SetBool("Health",true);
		}else{
			anim.SetBool("Health",false);
		}
		if (Input.GetKey(KeyCode.C)){

			anim.SetBool("Rage",true);
		}else{
			anim.SetBool("Rage",false);
		}
	}


}
