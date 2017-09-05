using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemiesScript : MonoBehaviour {
	private int game_scene_width = 0;
	private static int[] enemies_positions;

	public GameObject[] enemy_prefab;

	// Use this for initialization
	void Start () {
		float camera_distance = Mathf.Abs ( Camera.main.transform.position.z - transform.position.z );
		Vector3 left_most = Camera.main.ViewportToWorldPoint ( new Vector3(0, 0, camera_distance) );
		Vector3 right_most = Camera.main.ViewportToWorldPoint ( new Vector3(1, 0, camera_distance) );

		game_scene_width = (Mathf.Abs ( (int) left_most.x) + Mathf.Abs ( (int) right_most.x) - 2);
		enemies_positions = new int[game_scene_width];

		for(int i = 0; i < game_scene_width; i++){
			enemies_positions [i] = 0;
		}

		InvokeRepeating ("create_enemy", 0.000001f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void enemy_destroyed (GameObject enemy){
		//Debug.Log ("Position: " + (int) enemies_positions.Length);
//		Debug.Log ("Enemy X:: " +  enemy.transform.position.x);
//		Debug.Log ("Index: " + ( (int)( enemy.transform.position.x) + enemies_positions.Length/2));

		enemies_positions [ ((int) enemy.transform.position.x) + enemies_positions.Length/2] = 0;
		int counter = 0;
		foreach (int x in enemies_positions) {
			counter += x;
		}
		string test = " ";
		foreach (int x in enemies_positions) {
			test += x.ToString () + " ";
		}
	}

	void create_enemy(){
		while (true) {
			int random_position = UnityEngine.Random.Range (0, game_scene_width);
			if (enemies_positions [random_position] == 1) {
				continue;
			} else {
				enemies_positions [random_position] = 1;

				int random_prephab = Mathf.Clamp( (int)ScoreKeeper.get_score() / 2000, 0,  enemy_prefab.Length);
				GameObject enemy = Instantiate (enemy_prefab[UnityEngine.Random.Range(0, random_prephab)], new Vector3(random_position - enemies_positions.Length/2, 6, 0), Quaternion.identity) as GameObject;
				enemy.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -2f);
				break;
			}
		}

		string test = " ";
		foreach (int x in enemies_positions) {
			test += x.ToString () + " ";
		}
//		Debug.Log ("Created   = " + test);
	}
}
