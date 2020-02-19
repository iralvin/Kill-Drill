using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EMPController : MonoBehaviour {

	Animator acornAnim;
	Animator empAnim;
	Animator empCounterAnim;

	BoxCollider2D acornBoxCollider;

	public Sprite acornEmpty;
	public Sprite acornFull;
	SpriteRenderer currentSpriteImage;
	Image acornButtonImg;

	Slider EMPCounterBar;
	Button acornButton;

	public  AudioClip picklesEMPsound;
	public AudioClip EMPBlastSound;
	 AudioSource audioSource;

	public static float EMPCount;
	public GameObject empPulse;

	public static bool isEMPSoundReady = false;

//	float shakingX;
//	public float speed = 10.0f; //how fast it shakes
//	public float amount = 1f; //how much it shakes
//
//




	// Use this for initialization
	void Start () {
		EMPCount = 0;
		audioSource = gameObject.GetComponent<AudioSource> ();
		EMPCounterBar = GameObject.Find ("EMP Counter Bar").GetComponent<Slider> ();
		empCounterAnim = GameObject.Find ("EMP Counter Bar").GetComponent<Animator> ();
		empAnim = GameObject.Find ("EMPPulse").GetComponent<Animator> ();
		acornAnim = GameObject.Find ("AcornButton").GetComponent<Animator> ();
		acornButtonImg = GameObject.Find ("AcornButton").GetComponent<Image> ();

		acornButton = GameObject.Find ("AcornButton").GetComponent<Button> ();

		acornButton.interactable = false;
		empPulse.SetActive (false);
		empCounterAnim.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (levelControler.gameOver == false) {
			EMPCounterBar.value = EMPCount;
			if (EMPCount == 8) {
				acornButtonImg.sprite = acornFull;
				acornButton.interactable = true;
				acornAnim.SetBool ("empFull", true);
//			isEMPSoundReady = true;
			}
			if (isEMPSoundReady == true) {
				PlayEMPSound ();
			}
		}

		if (EMPCount == 0) {
			acornButtonImg.sprite = acornEmpty;
			acornAnim.SetBool ("empFull", false);
			acornButton.interactable = false;
		}
	}


	public void PlayEMPSound(){
		if (PlayerPrefsScript.soundFXState == 1) {
			audioSource.PlayOneShot (picklesEMPsound);
			isEMPSoundReady = false;
		}
	}
		

	public void EMPBlast(){
		Debug.Log ("EMP BLASTED");
//		EMPCount = 0;
		audioSource.PlayOneShot (EMPBlastSound);
		empPulse.SetActive (true);
		empCounterAnim.enabled = true;
		StartCoroutine (TurnOffEMP ());

		empCounterAnim.SetBool ("usedEMP", true);
		empAnim.SetBool ("empIsOn", true);



	}

	IEnumerator TurnOffEMP(){
		yield return new WaitForSeconds(1.0f);
		empCounterAnim.SetBool ("usedEMP", false);
		empCounterAnim.enabled = false;
		empAnim.SetBool ("empIsOn", false);
		empPulse.SetActive (false);
		EMPCount = 0;

	}




}
