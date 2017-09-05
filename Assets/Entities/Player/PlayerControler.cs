using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour {

	public float player_speed = 5f;
	private float xmin = 0;
	private float xmax = 0;

	private bool shield_state = false;
	private float wait_time_for_shield = 10f;

	public int max_health = 500;
	private int health;
	public int impact_damage;
	public GameObject[] lasers;
	public GameObject shield;
	public Text health_text;

	// Use this for initialization
	void Start () {
		health = max_health;

		float camera_distance = Mathf.Abs ( Camera.main.transform.position.z - transform.position.z );
		Vector3 left_most = Camera.main.ViewportToWorldPoint ( new Vector3(0, 0, camera_distance) );
		Vector3 right_most = Camera.main.ViewportToWorldPoint ( new Vector3(1, 0, camera_distance) );

		xmin = left_most.x + 1f;
		xmax = right_most.x - 1f;
	}
	
	// Update is called once per frame
	void Update () {
		int new_health = 500 * (int) ( ScoreKeeper.get_score() / 10000 + 1 );
		if (new_health > max_health) {
			max_health = health = new_health;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("shoot", 0.00001f, 0.2f);
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("shoot");
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			if (health < max_health) {
				ScoreKeeper.minus_from_score (Mathf.Min (max_health - health, ScoreKeeper.get_score () - 100));
				health += Mathf.Min (max_health - health, ScoreKeeper.get_score () - 100);
				health_text.text = health.ToString ();
			}
		} else if (Input.GetKeyDown (KeyCode.X)) {
			InvokeRepeating ("shoot_2", 0.00001f, 0.2f);
		} else if (Input.GetKeyUp (KeyCode.X)) {
			CancelInvoke ("shoot_2");
		} else if (Input.GetKeyDown (KeyCode.C)) {
			if (!shield_state && ScoreKeeper.get_score() > 8000) {
				StartCoroutine (create_shield(Mathf.Max(5, ScoreKeeper.get_score()/ 5000)));
				ScoreKeeper.minus_from_score (2000);
			}
		}




		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += new Vector3 ( player_speed * Time.deltaTime, 0, 0);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += new Vector3 (-player_speed * Time.deltaTime, 0, 0);
		}

		transform.position = new Vector3 ( Mathf.Clamp(transform.position.x, xmin, xmax), transform.position.y, transform.position.z );
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.gameObject.tag == "enemy" || other.transform.gameObject.tag == "enemy_laser") {
			int damage = 0;
			if (other.transform.gameObject.tag == "enemy_laser") {
				damage = other.transform.gameObject.GetComponent<EnemyLaserMover> ().get_impact_damage ();
			} else if (other.transform.gameObject.tag == "enemy"){
				damage = other.transform.gameObject.GetComponent<Enemy> ().get_impact_damage ();
			}

			health -= damage;
			health_text.text = health.ToString();
			if (health <= 0) {
				Destroy (gameObject);
				health_text.text = "0";
			}
//			if (other.transform.gameObject.tag == "enemy") {
//				EnemiesScript.enemy_destroyed (other.transform.gameObject);
//			}
			Destroy (other.transform.gameObject);
		}
	}

	void shoot(){
		Instantiate (lasers[0], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
		ScoreKeeper.minus_from_score (20);
	}

	void shoot_2(){
		Instantiate (lasers[0], new Vector3(transform.position.x + 0.15f, transform.position.y + 1, transform.position.z), Quaternion.identity);
		Instantiate (lasers[0], new Vector3(transform.position.x - 0.15f, transform.position.y + 1, transform.position.z), Quaternion.identity);
		ScoreKeeper.minus_from_score (20);
	}

	IEnumerator create_shield(float time){
		GameObject shield_game = Instantiate (shield, new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
		shield_state = true;
		yield return new WaitForSeconds (time);
		Destroy (shield_game);
		yield return new WaitForSeconds (wait_time_for_shield);
		shield_state = false;
	}

	public void damage (int n){
		health -= n;
	}
	public int get_impact_damage(){
		return impact_damage;
	}
}
