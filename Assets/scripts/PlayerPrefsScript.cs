using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsScript : MonoBehaviour {


    // int value 1 means soundFX is ON
    // int value 0 means soundFX is OFF
    public static int soundFXState = 1;
    public static int musicState = 1;
    public static bool hasPlayedBefore;
//	public GameObject tutorialPage;

//	bool musicc;
//	bool soundd = true;
//
    private void Awake()
    {
        hasPlayedBefore = PlayerPrefsX.GetBool("HasPlayedBefore");

        //		Debug.Log(soundd);
        //		soundd = PlayerPrefsX.GetBool("SoundfxState");
        //		Debug.Log(soundd);
    }


    // Use this for initialization
    void Start () {
        //		PlayerPrefs.DeleteAll ();
        if (hasPlayedBefore == true)
        {
            soundFXState = PlayerPrefs.GetInt("SoundFX State");
            musicState = PlayerPrefs.GetInt("Music State");
            
        } else if (hasPlayedBefore == false)
        {
//			if (tutorialPage != null) {
//				tutorialPage.SetActive (true);
//			}
            PlayerPrefs.SetInt("Music State", musicState);
            PlayerPrefs.SetInt("SoundFX State", soundFXState);
        }

        Debug.Log("music state is " + musicState);
        Debug.Log("soundfx state is " + soundFXState);

    }

	public void resetHasPlayed(){
		hasPlayedBefore = false;
		PlayerPrefsX.SetBool("HasPlayedBefore", hasPlayedBefore);
		Debug.Log ("hasplayedbefore = " + hasPlayedBefore);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ToggleSoundFX()
    {
        if (soundFXState == 0)
        {
            // turn soundFX ON
            soundFXState = 1;
            Debug.Log("soundFx is ON" );
        } else if (soundFXState == 1)
        {
            // turn soundFX OFF
            soundFXState = 0;
            Debug.Log("soundFx is OFF");
        }

        PlayerPrefs.SetInt("SoundFX State", soundFXState);
    }


    public void ToggleMusic()
    {
        if (musicState == 0)
        {
            // turn soundFX ON
            musicState = 1;
            Debug.Log("music State is ON");
        }
        else if (musicState == 1)
        {
            // turn soundFX OFF
            musicState = 0;
            Debug.Log("musicState is OFF");
        }

        PlayerPrefs.SetInt("Music State", musicState);
    }




}
