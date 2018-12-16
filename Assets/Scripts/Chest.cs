using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = GameController.khattabHips.GetComponent<Transform>().position;
		float Distance = Vector3.Distance(targetPosition , transform.position);
		if(Distance <5){
			Debug.Log("Health full");
			KhattabController.health = KhattabController.maxHealth ;
			Destroy(this.gameObject);
		}
	}
}
