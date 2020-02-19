using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthDED : MonoBehaviour {

    public float delay = 5f;
	Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
//        Destroy(gameObject, delay);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EarthExplode(){
		anim.Play ("endOfTheWorld_animation");
	}
}
