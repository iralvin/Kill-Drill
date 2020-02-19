using UnityEngine;
using System.Collections;

public class DrillCode : MonoBehaviour 
{
	public GameObject pickles;

	public float speed;

	private Vector3 center = new Vector3(0,0,0);

	public float angleOfAproach;

	public bool isInRock = true;
	public float speedInRockModifier;

	public LineRenderer tunnel;
	public Vector3 tunnelPos;

	private float deathTimer = 1.0f;
	public bool isDead = false;

	public levelControler other;
	public oilRigControl oilrigcontroller;
	public GameObject manager;

	public AudioClip explosionSound;
    private AudioSource audioSource;
    //public PlayerPrefsScript playerPrefsScript;

	public Vector3 tunnelStartLocation;
	public bool isEMPed;

	Animator drillEMPanim;
	Animation anim;

    void Start()
	{
		anim = gameObject.GetComponent<Animation>();

		isEMPed = false;
		drillEMPanim = gameObject.GetComponent<Animator> ();
        audioSource = GetComponent<AudioSource>();
        pickles = GameObject.Find("pickles");
        center = new Vector3 (pickles.transform.position.x, pickles.transform.position.y);

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "magma") {
			isInRock = false;
			Debug.Log ("BREACH");
			}
		}

//	public static void UpdateCurrentLocation (Vector3 position){
//		tunnelStartLocation = position;
//		Debug.Log (tunnelStartLocation);
//	}

	void FixedUpdate () {
		if(isDead){
			deathTimer -= Time.deltaTime;
			Color color = tunnel.material.color;
			color.a -= 0.025f;
			tunnel.material.color = color;
			if (deathTimer<=0){
				Destroy (gameObject);
			}
		}


//		Debug.Log (isEMPed);

		tunnel.SetPosition (0, tunnelStartLocation);			// origin position of tunnel
		//		tunnel.SetPosition (0, tunnelPos);					// ORIGIN position of tunnel
		tunnel.SetPosition (1, transform.position);			// DRILL position of tunnel


		if (isEMPed == false) {
			MoveDrill ();
		} else if (isEMPed == true) {
			StartCoroutine (EMPWearsOff ());
		}
	}

	IEnumerator EMPWearsOff(){
		yield return new WaitForSeconds (5f);
		anim.Stop ("drillEMPd");
//		drillEMPanim.enabled = true;
		isEMPed = false;
//		drillEMPanim.SetBool ("isDrillEMP", false);


	}

	public void MoveDrill (){
//		if(isDead){
//			deathTimer -= Time.deltaTime;
//			Color color = tunnel.material.color;
//			color.a -= 0.025f;
//			tunnel.material.color = color;
//			if (deathTimer<=0){
//				Destroy (gameObject);
//			}
//		}


		if (isInRock) {
			var change = (speed/speedInRockModifier) * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, center, change);
		} else{
			var change = (speed) * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, center, change);
		}

		if (transform.position == center && isDead == false){
			death (0);
		}
	}


	public void death (float score){

		GameObject manager = GameObject.Find("manager");     
		levelControler other = manager.GetComponent<levelControler>();

		if (score == 0){
			Debug.Log ("GAME OVER");
			other.gameEnd ();
			isDead = true;
			if (PlayerPrefsScript.soundFXState == 1)
			{
				AudioSource.PlayClipAtPoint(explosionSound, center);
			} else
			{
			}
			return;
		}

		GetComponent<SpriteRenderer>().enabled = false;
		isDead = true;

        other.updateScore(score);
        
		if (PlayerPrefsScript.soundFXState == 1)
        {
            AudioSource.PlayClipAtPoint(explosionSound, center);
        } else
        {
        }

        
    }





}
