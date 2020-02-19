using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePicklesClone : MonoBehaviour {

	public GameObject pickles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.rotation = pickles.transform.rotation;
	}
}
