using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour {


	levelControler levelcontroller;

    //	public GameObject textBox;
    public static InputField inputField;
    public static string playerInputName;





    // Use this for initialization
    void Start () {
		levelcontroller = new levelControler ();

        inputField = gameObject.GetComponent<InputField>();

	}
	
	// Update is called once per frame
	void Update () {

	}



	public void GetInput(string playerName){

		//Debug.Log ("you entered " + playerName);
        playerInputName = playerName;
        //		levelcontroller.textBox.SetActive (false);
        //		levelcontroller.gameOverMenu.SetActive(true);
        PlayerPrefs.SetString("ScoreName" + HighScoreController.rankToChangeName, playerInputName);
        Text textField = levelControler.textBox.GetComponent<Text>();
        textField.text = playerInputName;

    }

    public void DisplayNewHighScoreName()
    {
        //PlayerPrefs.SetString("ScoreName" + HighScoreController.rankToChangeName, playerInputName);
        //Text textField = levelControler.textBox.GetComponent<Text>();
        //textField.text = playerInputName;
        //inputField.text = "";

    }


    //	textBox.SetActive (true);
    

}
