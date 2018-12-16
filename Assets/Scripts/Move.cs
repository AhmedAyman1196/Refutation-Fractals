using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    void FixedUpdate()
    {
        float movement = 15.0f;
        float rotation = 60.0f;
        float up = 0.0f;
        if(Input.GetKey("e"))
        {
            up = 1;
        }
        if (Input.GetKey("q"))
        {
            up = -1;
        }
        transform.Translate(new Vector3(0.0f, up*Time.deltaTime * movement, Input.GetAxis("Vertical") * Time.deltaTime * movement));
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal") * Time.deltaTime * rotation, 0.0f));
    }
}
