using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

	public float lifetime = 1.0f;

	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0f)
			Destroy (gameObject);
	}
}
