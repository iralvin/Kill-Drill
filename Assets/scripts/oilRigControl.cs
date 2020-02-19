using UnityEngine;
using System.Collections;

public class oilRigControl : MonoBehaviour {

	public float spawnRadius;
	public float timeUntilSpawn;
	public float timeBetweenSpawn;
	public int maxDrillsSpawned;		// based on speed of drill to center
    public float hitPoints;

	public GameObject drillPrefab;
	public GameObject explosionPrefab;
    public GameObject explosion;

    private GameObject oilrigClone;

	public float angle;

	public int ammo;
	private bool outOfAmmo = false;
	private float deathTimer = 2.0f;

	public AudioClip pump;
	public AudioClip explosionSound;
	public AudioClip spawnSound;
	AudioSource rigAudioSource;

	private GameObject manager;
	public Vector3 currentLocation;
	private Quaternion currentRotation;

	private GameObject[] newDrill;


	private GameObject drillSpawnLocation;
	public GameObject drillSpawnObject;
	public Vector3 tunnelStart;

	private int startingAmmo;

	levelControler levelcontrol;
	EMPController empController;

	public bool isEMPed;

	void Start (){
		isEMPed = false;
		manager = GameObject.Find("manager");
//		rigAudioSource = GetComponent<AudioSource> ();
		levelcontrol = manager.GetComponent<levelControler> ();
		gameObject.transform.SetParent (levelcontrol.earth.transform);
//		ammo = gameObject.GetComponent<oilRigControl> ().ammo;
		startingAmmo = ammo;
		newDrill = new GameObject[15];


		levelControler.activeRigs.Add (gameObject);
		Debug.Log ("NUMBER OF ACTIVE RIGS IN RIGLIST = " + levelControler.activeRigs.Count);


	}

	void Update(){
//		currentLocation = drillSpawnObject.transform.position;
		currentLocation = gameObject.transform.position;
		currentRotation = gameObject.transform.rotation;
//		tunnelStart = drillSpawnLocation.transform.position;

		//	THIS ACCOUNTS FOR CURRENT POSITION OF EACH DRILL BASED ON NORMAL SPAWN RATE OF 2 DRILLS PER RIG AT ANY TIME
		//	IF INCREASING DIFFICULTY AND INCREASING SPAWN RATE (IE. 3 DRILLS PER RIG), WILL NEED TO ADD AN ADDITIONAL
		//	LINE OF CODE TO ACCOUNT OF 3RD DRILL SPAWNED
		if (newDrill [ammo+1] == null) {
		} else {
			newDrill [ammo+1].GetComponent<DrillCode> ().tunnelStartLocation = currentLocation;
		}
		if (newDrill [ammo+2] == null) {
		} else {
			newDrill [ammo+2].GetComponent<DrillCode> ().tunnelStartLocation = currentLocation;
		}
		if (newDrill [ammo+3] == null) {
		} else {
			newDrill [ammo+3].GetComponent<DrillCode> ().tunnelStartLocation = currentLocation;
		}

	}



	void FixedUpdate () {
//		Debug.Log (isEMPed);
		if (isEMPed == false) {
			SpawnDrill ();
		} else if (isEMPed == true) {
			StartCoroutine (EMPWearsOff ());
			gameObject.GetComponent<Animator> ().enabled = false;
//			rigAudioSource.enabled = false;
		}
	}

	IEnumerator EMPWearsOff(){
		yield return new WaitForSeconds (5f);
		isEMPed = false;
		gameObject.GetComponent<Animator> ().enabled = true;
//		rigAudioSource.enabled = true;

	}

	public void SpawnDrill (){
		timeUntilSpawn -= Time.deltaTime;

		float spawnX = Mathf.Cos(angle)*spawnRadius;
		float spawnY = Mathf.Sin(angle)*spawnRadius;
		Vector3 spawnLocation = new Vector3 (spawnX, spawnY,-1);
		//		Vector3 tunnelSpawn = currentLocation;

//		Vector3 tunnelSpawn = new Vector3 (spawnX, spawnY, 0);

		if (timeUntilSpawn <= 0 && !outOfAmmo)
		{

//			drillSpawnLocation = (GameObject)Instantiate(
//				drillSpawnObject,
//				currentLocation,
//				currentRotation,
//				levelcontrol.earth.transform
//				//				transform.rotation
//			);

			//spawn a new drill
			timeUntilSpawn = timeBetweenSpawn;
			newDrill[ammo] = (GameObject)Instantiate(
				drillPrefab,
				currentLocation,
				currentRotation,
				levelcontrol.earth.transform
				//				transform.rotation
			);
			//			Debug.Log (newDrill [ammo]);







			// add the new drill to our list of onscreen drills
			levelcontrol.drillList.Add (newDrill[ammo]);



			if (PlayerPrefsScript.soundFXState == 1)
			{
				AudioSource.PlayClipAtPoint(spawnSound, new Vector3(0, 0, 0));
			}

			//			newDrill.GetComponent<DrillCode> ().tunnelPos = currentLocation;
			//			newDrill.GetComponent<DrillCode> ().tunnelStartLocation = currentLocation;


			ammo -= 1;
			if (ammo <= 0){
				outOfAmmo = true;
			}
		}	




		if (outOfAmmo){
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0){
				GameObject explosion = (GameObject)Instantiate (
					explosionPrefab,
					transform.position,
					transform.rotation);
				levelControler.activeRigs.Remove (gameObject);
				Destroy (gameObject);
			}


		}
	}













    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fastPeanut")
        {
            //Debug.Log("oil rig is hit");
            GameObject explosionNew = (GameObject)Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
			EMPController.EMPCount += 1;
//			Debug.Log ("EMP Count = " + EMPController.EMPCount);
			death(1);
			if (EMPController.EMPCount == 8) {
				EMPController.isEMPSoundReady = true;
			}
        }
    }



    public void death (float score){
		GameObject manager = GameObject.Find("manager");
		levelControler other = manager.GetComponent<levelControler>();
		other.updateScore (score);
		levelControler.activeRigs.Remove (gameObject);
		Destroy (gameObject);

        if (PlayerPrefsScript.soundFXState == 1)
        {
            AudioSource.PlayClipAtPoint(explosionSound, new Vector3(0, 0, 0));
        }
        else
        {
        }

		StartCoroutine (KeepTunnelOpen ());
    }

	IEnumerator KeepTunnelOpen(){
		yield return new WaitForSeconds (7f);
		Destroy (drillSpawnLocation);
	}




}
