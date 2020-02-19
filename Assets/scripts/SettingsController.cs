using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SettingsController : MonoBehaviour {

    public Sprite soundFXOn;
    public Sprite soundFXOff;
    public Sprite musicOn;
    public Sprite musicOff;
    Image currentButtonImage;
    Button soundButton;
    Button musicButton;




	// Use this for initialization
	void Start () {
        currentButtonImage = GetComponent<Image>();

        if (gameObject.tag == "musicButton")
        {
            musicButton = GetComponent<Button>();
            musicButton.onClick.AddListener(ChangeMusicSprite);
            ChangeMusicSprite();
        }
        else if (gameObject.tag == "soundFXButton")
        {
            soundButton = GetComponent<Button>();
            soundButton.onClick.AddListener(ChangeSoundSprite);
            ChangeSoundSprite();
        }

        if (gameObject.tag == "oilRig")
        {
            AudioSource oilRigPump = GetComponent<AudioSource>();
            if (PlayerPrefsScript.soundFXState == 1)
            {
                oilRigPump.volume = 1;
            } 
            else if (PlayerPrefsScript.soundFXState == 0)
            {
                oilRigPump.volume = 0;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ChangeSoundSprite()
    {
        if (PlayerPrefsScript.soundFXState == 1) { 
            currentButtonImage.sprite = soundFXOn;
			Debug.Log ("soundfx toggle set to ON");
        }
        else if (PlayerPrefsScript.soundFXState == 0)
        {
            currentButtonImage.sprite = soundFXOff;
			Debug.Log ("soundfx toggle set to OFF");
        }
    }


    public void ChangeMusicSprite()
    {
        if (PlayerPrefsScript.musicState == 1)
        {
            currentButtonImage.sprite = musicOn;
			Debug.Log ("music toggle set to ON");
        }
        else if (PlayerPrefsScript.musicState == 0)
        {
            currentButtonImage.sprite = musicOff;
			Debug.Log ("music toggle set to OFF");
        }
    }


}
