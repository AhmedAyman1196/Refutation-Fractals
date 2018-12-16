using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Animator anim;
    public GameObject camera_;
    public float walkSpeed, runSpeed, jumpForce;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(camera_.transform.eulerAngles.x, camera_.transform.eulerAngles.y, camera_.transform.eulerAngles.z);
        bool shift_down = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        anim.SetBool("Running", Input.GetKey(KeyCode.W) && shift_down);
        anim.SetBool("Walking", Input.GetKey(KeyCode.W));
        anim.SetBool("RunningBack", Input.GetKey(KeyCode.S));
        anim.SetBool("Right", Input.GetKey(KeyCode.D));
        anim.SetBool("Left", Input.GetKey(KeyCode.A));
        if(transform.position.y <= 0)anim.SetBool("Jumping", Input.GetKey(KeyCode.Space));
        anim.SetBool("LightAttack", Input.GetMouseButtonDown(0));
        anim.SetBool("HeavyAttack", Input.GetMouseButtonDown(1));
        // forward
        if (Input.GetKey(KeyCode.W)){
            transform.Translate(Vector3.forward * (shift_down ? runSpeed : walkSpeed));
		}
        //backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * walkSpeed);
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkSpeed);
        }
        //left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walkSpeed);
        }

        if (transform.position.y <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(Vector3.up * walkSpeed);
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
