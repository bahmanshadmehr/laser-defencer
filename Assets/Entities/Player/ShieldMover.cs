using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMover : MonoBehaviour {

	public float player_speed = 5f;
	public float health = 500;

	private float xmin = 0;
	private float xmax = 0;

	// Use this for initialization
	void Start () {
		float camera_distance = Mathf.Abs ( Camera.main.transform.position.z - transform.position.z );
		Vector3 left_most = Camera.main.ViewportToWorldPoint ( new Vector3(0, 0, camera_distance) );
		Vector3 right_most = Camera.main.ViewportToWorldPoint ( new Vector3(1, 0, camera_distance) );

		xmin = left_most.x + 1f;
		xmax = right_most.x - 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += new Vector3 ( player_speed * Time.deltaTime, 0, 0);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += new Vector3 (-player_speed * Time.deltaTime, 0, 0);
		}

		transform.position = new Vector3 ( Mathf.Clamp(transform.position.x, xmin, xmax), transform.position.y, transform.position.z );
	}

	public void damage (int n){
		health -= n;
	}
}
