using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDistroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 0.15f);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
