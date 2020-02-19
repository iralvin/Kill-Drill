using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour {
    public Text buttonText;
    private bool buttonState = false;

    // Use this for initialization
    void Start () {
        buttonText = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Buttonpress()
    {
        buttonText.text = "button on";

        //if (buttonState == true)
        //{
        //    buttonText.text = "button off";
        //    buttonState = false;
        //}
        //else if (buttonState == false)
        //{
        //    buttonText.text = "button on";
        //    buttonState = true;
        //
    }

    
}
