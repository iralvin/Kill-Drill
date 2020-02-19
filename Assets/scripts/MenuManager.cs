using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour {

	public GameObject returnHomePopUp;
	public GameObject picklesGroup, picklesCloneGroup, earthGroup, scoreGroup, empGroup;
	public AudioSource rigPumpPlayer;

	void Start () 
	{
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
	}

	public void sceneselect (string sceneName)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (sceneName);
	}




    private void OnApplicationQuit()
    {
        PlayerPrefsScript.hasPlayedBefore = true;
        PlayerPrefsX.SetBool("HasPlayedBefore", PlayerPrefsScript.hasPlayedBefore);
        Debug.Log("game has quit");
//        Debug.Log("hasplayedbefore = " + PlayerPrefsScript.hasPlayedBefore);
    }

	public void ActivateReturnHome(string sceneName){

		Time.timeScale = 0;

		if (levelControler.gameOver == true) {
			Time.timeScale = 1;
			sceneselect (sceneName);
		} else {
			returnHomePopUp.SetActive (true);
			rigPumpPlayer.volume = 0;
			picklesGroup.SetActive (false);
			picklesCloneGroup.SetActive (false);
			earthGroup.SetActive (false);
//		scoreGroup.SetActive (false);
			empGroup.SetActive (false);
		}
	}

	public void CancelReturnHome(){
		returnHomePopUp.SetActive (false);
		Time.timeScale = 1;
		rigPumpPlayer.volume = 1;
		picklesGroup.SetActive (true);
		picklesCloneGroup.SetActive (true);
		earthGroup.SetActive (true);
//		scoreGroup.SetActive (true);
		empGroup.SetActive (true);
	}



}
