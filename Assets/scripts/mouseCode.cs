using UnityEngine;
using System.Collections;

public class mouseCode : MonoBehaviour {

	public GameObject oilRigdestuctTimer;
	public GameObject pickles;
	public GameObject pickles1;
	public GameObject fastBullet;
	public GameObject slowBullet;
	public GameObject firePosition;


	public float chargeTimer;
	public float chargedUp;

	public Sprite chargedPickles;

	public LineRenderer dragLine;
	public float dragDistance;
	public float regularVelocity;
	public float chargedVelocity;
    Vector3 mouseWorldPos3D;

    Animator picklesAnimation;
	Animator picklesCloneAnim;

    void Start(){
		picklesCloneAnim = pickles1.GetComponent<Animator> ();
        picklesAnimation = pickles.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0)) {
			chargeTimer += Time.deltaTime;
			mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		}



        if (Input.GetMouseButtonDown (0)){
            //mouse clicked

//            mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            mouseWorldPos3D = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            Vector2 mousePos2D = new Vector2 (mouseWorldPos3D.x, mouseWorldPos3D.y);
			Vector2 dir = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast (mousePos2D, dir);

			if (hit.collider!=null){
				// we clicked somethign with a collider
				if (hit.collider.name == "pickles")
				{
                    picklesAnimation.SetBool("isLoaded", true);
                    picklesAnimation.SetBool("hasFired", false);

                    dragLine.enabled = true;

					Debug.Log ("clicked squirrel");
				} 

//				if (hit.collider.name == "oilrig(Clone)")
//				{
//					Vector3 _pos = hit.collider.transform.position;
//					_pos.z = -2;
//					oilRigdestuctTimer.transform.position = _pos;
//					oilRigdestuctTimer.SetActive (true);
//					oilRigdestuctTimer.GetComponent<oilRigDestructionTimer> ().timing = true;
//					oilRigdestuctTimer.GetComponent<oilRigDestructionTimer> ().oilrig = hit.collider.transform.gameObject;
//					Debug.Log ("clicked rig");
//				} 
//				else 
//				{
//					Debug.Log ("clicked other");
//				}
//				////////////////////////////////////////

			}		
		}

		if (dragLine.enabled == true) {
//			Debug.Log ("CHARGE ANIMATION ENABLED");
			picklesCloneAnim.SetBool ("isCharged", true);
		}


//		if (Input.GetMouseButtonUp (0) && oilRigdestuctTimer.activeSelf)
//		{
//			oilRigdestuctTimer.GetComponent<oilRigDestructionTimer>().currentAmmount=0;
//			oilRigdestuctTimer.GetComponent<oilRigDestructionTimer>().timing=false;
//			oilRigdestuctTimer.SetActive (false);
//		}

		dragDistance = Mathf.Abs(Vector2.Distance(pickles.transform.position, mouseWorldPos3D));


		if (Input.GetMouseButtonUp (0) && dragLine.enabled==true){
//            picklesAnimation.SetBool("isLoaded", false);
//            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			dragLine.enabled = false;
//            dragDistance = Mathf.Abs(Vector3.Distance(pickles.transform.position, mouseWorldPos3D));
//            Debug.Log(mouseWorldPos3D);
//            Debug.Log(pickles.transform.position);

			if (dragDistance > 3) {
				if (chargeTimer >= chargedUp) {
//					picklesAnimation.SetBool ("hasFired", true);

//					picklesCloneAnim.SetBool ("isCharged", false);
					GameObject newBullet = (GameObject)Instantiate (slowBullet, firePosition.transform.position, pickles.transform.rotation);
					newBullet.GetComponent<bulletCode> ().speed = chargedVelocity;
					newBullet.GetComponent<bulletCode> ().direction = mouseWorldPos3D;
				}
				if (chargeTimer < chargedUp) {
//					picklesAnimation.SetBool ("hasFired", true);
//					picklesCloneAnim.SetBool ("isCharged", false);

					GameObject newBullet = (GameObject)Instantiate (fastBullet, firePosition.transform.position, pickles.transform.rotation);
					newBullet.GetComponent<bulletCode> ().speed = regularVelocity;
					newBullet.GetComponent<bulletCode> ().direction = new Vector2 (mouseWorldPos3D.x, mouseWorldPos3D.y);
				}
			}
			picklesCloneAnim.SetBool ("isCharged", false);
			picklesAnimation.SetBool ("hasFired", true);
			picklesAnimation.SetBool("isLoaded", false);


        }

		if (Input.GetMouseButtonUp (0)) {
			chargeTimer = 0;
		}
        
	}


	void LateUpdate(){
		
		if (dragLine.enabled == true) {
//			Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			dragLine.SetPosition (0, new Vector3 (0,0,0));
			dragLine.SetPosition (0, pickles.transform.position);

			dragLine.SetPosition (1, new Vector3 (mouseWorldPos3D.x, mouseWorldPos3D.y, pickles.transform.position.z));

			//rotate pickles
			Vector3 dir = mouseWorldPos3D - pickles.transform.position;
			float angleToMouse = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			pickles.transform.rotation = Quaternion.AngleAxis(angleToMouse+180, Vector3.forward);
		}

	}
}