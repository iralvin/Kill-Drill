
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class levelControler : MonoBehaviour {

	public static float score;
	static string playerInputName;
	public Text scoreText;
	public GameObject gameOverMenu;
	public GameObject EMPGroup;
	public Button homeButton;

	public static GameObject textBox;
    InputField inputFieldBox;
    public GameObject scoreRankGroup, nameGroup, highScoreGroup, inputFieldGroup;
    Text inputTextText;


    public static bool inCombo ;
	public float comboMulti;
	public float comboTimerTotal;
	public float comboTimer;
	public Text comboText;

	public GameObject earth;
	public GameObject earthDed;
	public GameObject earthExploding;
	private GameObject earthExplodingClone;
	public GameObject pickles;
	public GameObject pickles1Clone;

	public List<GameObject> rigList = new List<GameObject>();
	public List<GameObject> drillList = new List<GameObject>();
	public static List<GameObject> activeRigs = new List<GameObject>();

	public float spawnRadius;
	public float timeUntilSpawn;
	public float MaxtimeBetweenSpawn = 4.0f;
	public float MintimeBetweenSpawn = 2.0f;
	public int MaxAmmo = 10;
	public int MinAmmo = 4;

	public GameObject rigPrefab;
	private Vector3 center;
	public static bool gameOver= true;

	public AudioClip earthExplosionSound;

	public HighScoreController highscoreClass;

    Vector3 centerPickles = new Vector3(0, 0, -1);

    GameObject earthCrust;
    float width;
    float height;


	private TouchScreenKeyboard onScreenKeyboard;

	public AudioSource rigPumpPlayer;
	public GameObject tutorialPage;

    private void Awake()
    {
    }

    public void Start()
    {
        earthCrust = GameObject.Find("Map bounce Soil and Crust");
        Vector2 size = earthCrust.GetComponent<Renderer>().bounds.size;
        width = size.x;
        height = size.y;
		comboTimer = 0;
		rigPumpPlayer.enabled = false;


		PreGameStart ();

		if (PlayerPrefsScript.hasPlayedBefore == false) {
			tutorialPage.SetActive (true);
			homeButton.GetComponent<Button> ().enabled = false;
		} else {
			gamestart ();
		}
    }
    
	public void AcknowledgeTutorialPage(){
		tutorialPage.SetActive (false);
		PlayerPrefsScript.hasPlayedBefore = true;
		PlayerPrefsX.SetBool("HasPlayedBefore", PlayerPrefsScript.hasPlayedBefore);
		homeButton.GetComponent<Button> ().enabled = true;
		gamestart ();
	}

	public void PreGameStart(){
		HighScoreController.hasNewHighScore = false;
		//textBox.SetActive(false);

		scoreRankGroup.SetActive(false);
		nameGroup.SetActive(false);
		highScoreGroup.SetActive(false);

		gameOverMenu.SetActive (false);
		gameOver = true;
		score = 0;

		updateScore (0);
		comboMulti = 1;
		comboText.enabled = false;
	}


    // Use this for initialization
    public void gamestart()
	{
		activeRigs.Clear ();

		Debug.Log ("activeRig list count = " + activeRigs.Count);

		PreGameStart ();
//		HighScoreController.hasNewHighScore = false;
//        //textBox.SetActive(false);
//
//        scoreRankGroup.SetActive(false);
//        nameGroup.SetActive(false);
//        highScoreGroup.SetActive(false);
//
//        gameOverMenu.SetActive (false);
		gameOver = false;
//		score = 0;
//////		updateScore (0);
//		comboMulti = 1;
//		comboText.enabled = false;

		center = new Vector3 (pickles.transform.position.x, pickles.transform.position.y);

		earth.SetActive (true);
		pickles.SetActive (true);
		pickles1Clone.SetActive (true);
		EMPGroup.SetActive (true);

	}

	public void gameEnd()
	{
		EMPController.EMPCount = 0;
		activeRigs.Clear ();

		gameOver = true;
		StartCoroutine (EarthWillExplode());
//		earth.SetActive (false);
//		pickles.SetActive (false);

		for (int rigIndex = 0; rigIndex < rigList.Count; rigIndex++) { //cycle through list of on screen enemies 
			if (rigList [rigIndex] != null) {
				Destroy (rigList [rigIndex]);
//				rigList.RemoveAt (rigIndex);
//				rigList [rigIndex].GetComponent<oilRigControl> ().death (0);
			}
		}

		for (int drillIndex = 0; drillIndex < drillList.Count; drillIndex++) { //cycle through list of on screen enemies 
			if (drillList [drillIndex] != null) {
				Destroy(drillList[drillIndex]);
//				drillList.RemoveAt (drillIndex);
//				drillList [drillIndex].GetComponent<DrillCode> ().death (0);
			}
		}

//		Instantiate (earthDed,center,this.transform.rotation);
		earthExplodingClone = Instantiate (earthExploding,center,this.transform.rotation); 
		Destroy (earthExplodingClone, 1f);
//		gameOverMenu.SetActive (true);

		Debug.Log ("run high score check");
		highscoreClass.ScoreCheck (score);
		highscoreClass.ChangeName();
//        DisplayHighScores();
    }

	IEnumerator EarthWillExplode(){
//		Debug.Log ("earth will explode");
		yield return new WaitForSeconds(0.85f);
//		Debug.Log ("earthEXPLODE sound");
		rigPumpPlayer.enabled = false;
        if (PlayerPrefsScript.soundFXState == 1)
        {
            AudioSource.PlayClipAtPoint(earthExplosionSound, center);
        }
        else
        {
        }


        earth.SetActive (false);
		pickles.SetActive (false);
		EMPGroup.SetActive (false);
		pickles1Clone.SetActive (false);
		DisplayHighScores ();
    }

	public void DisplayHighScores(){
		if (HighScoreController.hasNewHighScore == true)
		{
			// ACTIVATE THE TEXT BOX FOR THE RESPECTIVE RANK TO TYPE NAME IN
			scoreRankGroup.SetActive(true);
			nameGroup.SetActive(true);
			highScoreGroup.SetActive(true);

			textBox = GameObject.Find("name" + HighScoreController.rankToChangeName);
			Vector2 boxPosition = textBox.transform.position;

			//PlayerNameInput.inputField.text = "";
			inputFieldGroup.SetActive(true);
			inputFieldBox = inputFieldGroup.GetComponentInChildren<InputField>();
			inputFieldBox.text = "";
			inputFieldBox.transform.position = boxPosition;
//            inputFieldBox.ActivateInputField();
			onScreenKeyboard =  TouchScreenKeyboard.Open(inputFieldBox.text, TouchScreenKeyboardType.ASCIICapable, false);

            //inputTextText = inputFieldBox.GetComponent<Text>();

			Debug.Log ("HAS NEW HIGH SCORE!!!!!!!");

            highscoreClass.InitializeScores ();
		}
		else
		{
			gameOverMenu.SetActive(true);
			scoreRankGroup.SetActive(true);
			nameGroup.SetActive(true);
			highScoreGroup.SetActive(true);
		}
	}


	public  void EndEdit(){
//        inputFieldBox.text = "";
		Debug.Log("submit triggered");
        inputFieldGroup.SetActive(false);
        gameOverMenu.SetActive (true);
		scoreText.text = "";
		comboText.enabled = false;

	}




		
	void Update () 
	{
		if (inputFieldGroup.activeSelf == true) {
			if (inputFieldBox.isFocused == true  && onScreenKeyboard.done){
				Debug.Log ("keyboard was doned and active=false");
							EndEdit();
			}
			if (inputFieldBox.isFocused == false) {
				inputFieldBox.Select ();
//				inputFieldBox.ActivateInputField ();
			}

//			#if #UNITY_ANDROID
//
//			#endif
		}
//		clearListsofNull ();

		if (gameOver == false) {
			CheckForNonEMPRig ();
		}
		if(inCombo){
			comboTimer -= Time.deltaTime;
			if (comboTimer<=0){
				inCombo = false;
				comboTimer = 0;
				comboMulti = 1;
				comboText.enabled = false;
			}
		}
	}
		
	public void CheckForNonEMPRig(){
		int xxx = 0;

		if (activeRigs.Count == 0) {
			rigPumpPlayer.enabled = false;
		} else if (activeRigs.Count>=1) {
			
			//cycle through list of on screen enemies
			for (int rigNum = 0; rigNum < activeRigs.Count; rigNum++) {
				if (activeRigs [rigNum].GetComponent<oilRigControl> ().isEMPed == false && activeRigs[rigNum] != null) {
					// PLAY PUMP SOUND
//					rigPumpPlayer.enabled = true;
//					break;
					xxx += 1;
				}
			}
			if (xxx >= 1) {
				rigPumpPlayer.enabled = true;
			} else {
				rigPumpPlayer.enabled = false;
			}
		}
	}



	public void updateScore(float scoreUpdate)
	{
		if (inCombo){
			comboMulti += 1;
			comboText.enabled = true;
			comboText.text = "COMBO X " + comboMulti;
		}
		score += (scoreUpdate*comboMulti);
		scoreText.text = score.ToString();
		inCombo = true;
		comboTimer = comboTimerTotal;
	}



	/// <summary>////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// start of spawn code////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// </summary>////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



	void FixedUpdate () 
	{
		if (!gameOver) 
		{
			timeUntilSpawn -= Time.deltaTime;
		}

		if (timeUntilSpawn <= 0 && !gameOver)
		{
			//clear lists of null destroyed gameobjects

			//generate a new spawn location
			float angle = Random.Range (1,30);
			float spawnX = Mathf.Cos(angle)*(width/2);
			float spawnY = Mathf.Sin(angle)*(width/2);
			Vector3 spawnLocation = new Vector3 (spawnX+earthCrust.transform.position.x, spawnY+earthCrust.transform.position.y,0);


			//spawn a new rig
			timeUntilSpawn = Random.Range(MintimeBetweenSpawn,MaxtimeBetweenSpawn);
			GameObject newRig = (GameObject)Instantiate(
				rigPrefab,
				spawnLocation,
				transform.rotation
			);

			newRig.GetComponent<oilRigControl> ().angle = angle;
			newRig.GetComponent<oilRigControl> ().ammo = Random.Range(MinAmmo,MaxAmmo);


			rigList.Add (newRig);

			//rotate rig To Face center
			Vector3 dir = center - newRig.transform.position;
			float angleToCenter = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			newRig.transform.rotation = Quaternion.AngleAxis(angleToCenter, Vector3.forward);



		}
	}


	public void clearListsofNull()
	{
		
		for (int rigIndex = 0; rigIndex < rigList.Count; rigIndex++ ) 
		{ //cycle through list of on screen enemies 
			if ( rigList[rigIndex] == null ) 
			{
//				Debug.Log ("rig index" + rigIndex);
				rigList.RemoveAt(rigIndex);
			}
		}

		for (int drillIndex = 0; drillIndex < drillList.Count; drillIndex++ ) 
		{ //cycle through list of on screen enemies 

			if ( drillList[drillIndex] == null ) 
			{
				drillList.RemoveAt(drillIndex);
			}
		}
	}
}
