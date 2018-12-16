using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    private bool credits;
    private bool finished;
    private float creditsLimit;

    void Start () {
        credits = true;
        finished = false;
        creditsLimit = 4.0f;
    }
	
	void Update () {
        if (transform.position.y > creditsLimit)
            finished = true;
	}

    void FixedUpdate()
    {
        float speed = 0.4f;
        if(credits && !finished)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
}
