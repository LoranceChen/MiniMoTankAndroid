﻿using UnityEngine;
using System.Collections;

public class ExploreDestory : MonoBehaviour {

	// Use this for initialization
	void DestroyGameObject(){
		GetComponent<AudioSource>().Play ();
		Destroy (gameObject);
	}

}
