using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 1000;
	public Text score_text;

	public static void add_to_score (int n){
		score += n;
	}

	public static void minus_from_score(int n){
		score -= n;
	}

	public static int get_score(){
		return score;
	}

	void Update(){
		score_text.text = score.ToString ();
	}
}
