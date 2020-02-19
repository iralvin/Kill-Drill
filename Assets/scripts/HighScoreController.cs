 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreController : MonoBehaviour {

//	public int highScore = levelControler.score;

	Text scoreText;
    Text nameText;
    int i, x, y, n, z;
//    private float[] highScoresList = new float[9];

	float scoreA, scoreB;
	float scoreToDisplay;
	float storedScore;
	levelControler levelcontroller;
    public static bool hasNewHighScore = false;
	float resetScore = 0;
    public static int rankToChangeName;
    string nameA, nameB;
    string nameToDisplay;
	GameObject scoreDisplayBox, nameDisplayBox;
    public GameObject resetWindow;


	void Awake(){
//		if (PlayerPrefs.HasKey ("HighScoresList") == true) {
//			Debug.Log ("playerprefs has key");
//			highScoresList = PlayerPrefsX.GetFloatArray ("HighScoresList");
//		}

		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

		if (sceneName != "options") 
		{
			InitializeScores ();
		}
        //resetWindow = GameObject.Find("Reset Scores Popup");
        //resetWindow.SetActive(false);
	}




    // Use this for initialization
   public void InitializeScores () {
//        scoreText = GetComponent<Text>();
//        nameText = GetComponent<Text>();



        // load high score GAMES to display
		for (y = 4; y >= 0; y--) {
			scoreDisplayBox = GameObject.Find ("highscore" + y);
//			if (gameObject.name == "highscore" + y) {
			scoreToDisplay = PlayerPrefs.GetFloat ("HighScores" + y);
//				scoreText.text = (9 - y) + ".\t\t\t" + scoreToDisplay;
			scoreText = scoreDisplayBox.GetComponent<Text>();
			scoreText.text = "" + scoreToDisplay;
//			}
		}


        // load high score NAMES to displayer
        for (z = 4; z >= 0; z--) {
			nameDisplayBox = GameObject.Find ("name" + z);
//            if (gameObject.name == "name" + z) {
            nameToDisplay = PlayerPrefs.GetString("ScoreName" + z);
			nameText = nameDisplayBox.GetComponent<Text> ();
			nameText.text = "" + nameToDisplay;
//            }
        }

		Debug.Log ("scores and names displayed");


    }



	// Update is called once per frame
	void Update () {
//		if (levelcontroller.highScoreGroup.activeInHierarchy == true)
//        {
//            InitializeScores();
//			hasNewHighScore = false;
//        }
    }



    public void ResetScores(){
		for (i = 0; i <= 4; i++) {
			PlayerPrefs.SetFloat ("HighScores" + i, resetScore);
            PlayerPrefs.SetString("ScoreName" + i, "");
            //Debug.Log ("scores reset to " + resetScore);
        }

        //GameObject highscoretextobject; 
        //Text highscoretext;
        //GameObject scorenametextobject; 
        //Text scorenametext;
        //      for (y = 8; y >= 0; y--) {
        //	highscoretextobject = GameObject.Find ("highscore" + y);
        //	highscoretext = highscoretextobject.GetComponent<Text>();
        //	highscoretext.text = "" + resetScore;

        //	scorenametextobject = GameObject.Find ("name" + y);
        //	scorenametext = scorenametextobject.GetComponent<Text> ();
        //	scorenametext.text = "";
        //}
        resetWindow.SetActive(false);
    }

    public void OpenResetPopUp()
    {
        resetWindow.SetActive(true);

    }

    public void CloseResetPopUp()
    {
        resetWindow.SetActive(false);

    }


    
    public void  ScoreCheck(float score) {
        
        // replace current high scores with new high score
        for (x = 4; x >= 0; x--) {
			Debug.Log (x);
			storedScore = PlayerPrefs.GetFloat ("HighScores" + x);
			if (score >= storedScore && x >= 1) {
                rankToChangeName = x;
                hasNewHighScore = true;
                //StartCoroutine(ChangeName());
                Debug.Log ("has new high score is true");
				for (i = 1; i <= x; i++) {
					scoreB = PlayerPrefs.GetFloat ("HighScores" + (i - 1));
					scoreA = PlayerPrefs.GetFloat ("HighScores" + i);
					scoreB = scoreA;
					PlayerPrefs.SetFloat ("HighScores" + (i - 1), scoreB);
				}
                //Debug.Log("new high score = " + score);
				PlayerPrefs.SetFloat ("HighScores" + x, score);
				break;
			}
			if (score >= storedScore && x == 0){
				rankToChangeName = x;
				hasNewHighScore = true;
				Debug.Log ("has new high score is true");
				PlayerPrefs.SetFloat ("HighScores" + x, score);
				break;
			}
		}

		//InitializeScores ();
//		Debug.Log("new scores are displayed");
        

        //		for (i = 8; i > 0; i--) {
        //			if (score >= highScoresList [i]) {
        //				for (x = 1; x < i; x++) {
        //					highScoresList [x - 1] = highScoresList [x];
        //				}
        //				highScoresList [i] = score;
        //				break;
        //			}
        //		}
        //
        //		// NEED TO FIGURE OUT CODE PLACEMENT IF SCORE IS ONLY BETTER THAN THE VERY LOWEST SCORE
        //		if (score >= highScoresList [0] && score < highScoresList[1]) {
        //			highScoresList [0] = score;
        //		}
        //
        //		PlayerPrefsX.SetFloatArray ("HighScoresList", highScoresList);


    }

    //IEnumerator ChangeName()
    public void ChangeName()
    {
		if (hasNewHighScore == true) {
			Debug.Log ("names are changed");
			//yield return new WaitForSeconds(0);
			for (n = 1; n <= rankToChangeName; n++) {
				nameB = PlayerPrefs.GetString ("ScoreName" + (n - 1));
				nameA = PlayerPrefs.GetString ("ScoreName" + n);
				nameB = nameA;
				PlayerPrefs.SetString ("ScoreName" + (n - 1), nameB);
			}
			nameA = "";
			PlayerPrefs.SetString ("ScoreName" + x, nameA);
		}
    }



}
