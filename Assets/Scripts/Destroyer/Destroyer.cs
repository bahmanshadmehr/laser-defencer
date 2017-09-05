using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.gameObject.tag == "enemy") {
			EnemiesScript.enemy_destroyed (other.transform.gameObject);
		}
		Destroy (other.transform.gameObject);
	}

}
