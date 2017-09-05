using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMover : MonoBehaviour {
	private Rigidbody2D rigidBody;
	private float laser_speed = 500f;

	public int impact_damage = 100;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rigidBody.velocity = new Vector3 (0, laser_speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.gameObject.tag == "enemy") {
			Destroy (gameObject);
		}
	}

	public int get_impact_damage(){
		return impact_damage;
	}
}
