using UnityEngine;
using System.Collections;

public class bulletCode : MonoBehaviour {

	public float speed;
	public Vector2 direction;
    public GameObject explosion;
    public GameObject oilrig;
	private float lifetime =10.0f;
	EMPController empController;


	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if(lifetime<=0){
			Destroy (gameObject); 
		}

		//move the bullet
		Vector2 position = transform.position;
		position -= (direction * Time.deltaTime)*speed;
		transform.position = position;
//		Debug.Log (position);
	}

	void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("HIT!");

        if (other.gameObject.name == "drill(Clone)" && other.gameObject.GetComponent<DrillCode>().isDead == false) {
			GameObject explosionNew = (GameObject)Instantiate(explosion,other.transform.position,other.transform.rotation);
			explosionNew.GetComponent<CamShake> ().shakeAmount = 0.7f;
			if (gameObject.tag == "slowPeanut") {
				Destroy (gameObject);
			}
//			empController.EMPCounter (1);

			EMPController.EMPCount += 1;
//			Debug.Log ("EMP Count = " + EMPController.EMPCount);
            other.GetComponent<DrillCode>().death(1);

			if (EMPController.EMPCount == 8) {
				EMPController.isEMPSoundReady = true;
			}
        }




    }






}
