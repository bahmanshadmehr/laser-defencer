using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int health;
	public int impact_damage;
	public int points = 100;
	public bool can_shoot;
	public int shot_count = 1;
	public GameObject[] lasers;
	public GameObject distroy_effect;

	// Use this for initialization
	void Start () {
		if (can_shoot) {
			InvokeRepeating ("shoot", 0.00001f, Random.Range(2f / Mathf.Max(ScoreKeeper.get_score() / 15000, 1), 4f / Mathf.Max(ScoreKeeper.get_score() / 15000, 1)));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void damage (int n){
		health -= n;
	}

	public int get_impact_damage(){
		return impact_damage;
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.gameObject.tag == "player_laser" || other.transform.gameObject.tag == "player") {
			int damage = other.transform.gameObject.GetComponent<LaserMover> ().get_impact_damage ();;
			if (other.transform.gameObject.tag == "player") {
				damage = other.transform.gameObject.GetComponent<PlayerControler> ().get_impact_damage ();
			}
			health -= damage;
			if (other.transform.gameObject.tag == "player") {
				EnemiesScript.enemy_destroyed (gameObject);
			}
			if (health <= 0){
				EnemiesScript.enemy_destroyed (gameObject);
				ScoreKeeper.add_to_score (points);
				Destroy (gameObject);
				Destroy (other.transform.gameObject);
			}
		}
		if (other.transform.gameObject.tag == "player" || other.transform.gameObject.tag == "Shield") {
			EnemiesScript.enemy_destroyed (gameObject);
			Destroy (gameObject);

		}

	}

	void shoot(){
		if (shot_count == 1 || shot_count == 3) {
			Instantiate (lasers[0], new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
		}
		if (shot_count < 4 && shot_count > 1) {
			Instantiate (lasers[0], new Vector3(transform.position.x + 0.25f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers[0], new Vector3(transform.position.x - 0.25f, transform.position.y - 1, transform.position.z), Quaternion.identity);
		} 
		if (shot_count == 4) {
			Instantiate (lasers [0], new Vector3 (transform.position.x + 0.10f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x - 0.10f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x + 0.20f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x - 0.20f, transform.position.y - 1, transform.position.z), Quaternion.identity);
		} else if (shot_count == 6) {
			Instantiate (lasers [0], new Vector3 (transform.position.x + 0.04f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x - 0.04f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x + 0.12f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x - 0.12f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x + 0.20f, transform.position.y - 1, transform.position.z), Quaternion.identity);
			Instantiate (lasers [0], new Vector3 (transform.position.x - 0.20f, transform.position.y - 1, transform.position.z), Quaternion.identity);
		} 
	}

}
