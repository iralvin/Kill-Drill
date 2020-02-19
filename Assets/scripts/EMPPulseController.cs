using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPPulseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D otherObject)
	{
		if (otherObject.gameObject.tag == "oilRig") {
//			Debug.Log ("OIL RIGS DISABLED");
			otherObject.gameObject.GetComponent<oilRigControl> ().isEMPed = true;
		} else if (otherObject.gameObject.tag == "drill") {
//			Debug.Log ("DRILLS DISABLED");
			otherObject.gameObject.GetComponent<DrillCode> ().isEMPed = true;

			float currentDrillRotation = otherObject.gameObject.transform.localEulerAngles.z;
//			Debug.Log ("drill rotation = " +currentDrillRotation);



			Animation anim = otherObject.gameObject.GetComponent<Animation> ();
			AnimationCurve curve;

			// create a new AnimationClip
			AnimationClip clip = new AnimationClip();
			clip.legacy = true;
			clip.wrapMode = WrapMode.Loop;

			float randomAngleRotationA = Random.Range (5,20);
			float randomAngelRotationB = Random.Range (5,20);
//			Debug.Log ("AngleA = " + randomAngleRotationA + "   AngleB = " + randomAngelRotationB);
			// create a curve to move the GameObject and assign to the clip
			Keyframe[] keys;
			keys = new Keyframe[5];
			keys[0] = new Keyframe(0.0f, currentDrillRotation);
			keys[1] = new Keyframe(0.1f, currentDrillRotation + randomAngleRotationA);
			keys[2] = new Keyframe(0.2f, currentDrillRotation);
			keys [3] = new Keyframe (0.3f, currentDrillRotation - randomAngelRotationB);
			keys [4] = new Keyframe (0.4f, currentDrillRotation);
			curve = new AnimationCurve(keys);


			clip.SetCurve("", typeof(Transform), "localEulerAngles.z", curve);
			anim.AddClip (clip,"drillEMPd");
			anim.Play ("drillEMPd");

		}
	}
	
}
