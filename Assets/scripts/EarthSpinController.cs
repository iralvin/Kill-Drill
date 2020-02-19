using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpinController : MonoBehaviour {

	Vector3 earthRotation = new Vector3(0,0,1);
	private float zRotation;
	public float speed;

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (earthRotation * speed);
	}
}
