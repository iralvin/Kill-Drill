using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class oilRigDestructionTimer : MonoBehaviour {

	public GameObject oilrig;
	public GameObject explosion;
	public bool timing = false;
	public GameObject destructTimer;
	[SerializeField] public float currentAmmount;
	[SerializeField] private float speed;


    
	void Update () {
//		if (currentAmmount<100&&timing)
//		{
//			currentAmmount += speed * Time.deltaTime;
//
//		}
//			destructTimer.GetComponent<Image>().fillAmount = currentAmmount /100;
//		if (currentAmmount>=100)
//		{
//			currentAmmount = 0;
//			timing = false;
//			oilrig.GetComponent<oilRigControl> ().death(1);
//			GameObject explosionNew = (GameObject)Instantiate(explosion,oilrig.transform.position,oilrig.transform.rotation);
//			explosionNew.GetComponent<CamShake> ().shakeAmount = 1.2f;
//
//		}
	}
    


}
