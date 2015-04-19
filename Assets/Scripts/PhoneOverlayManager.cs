using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PhoneOverlayManager : MonoBehaviour {

	public List<GameObject> Signals;

	private PhoneManager.Signal currentSignal;
	private PhoneManager phone;
	private Image lowSignal;
	private Image medSignal;
	private Image highSignal;

	/*private static PhoneOverlayManager instance;

	public static PhoneOverlayManager Instance {
		get {
			if(instance == null)
				instance = GameObject.FindObjectOfType<PhoneOverlayManager>();
			return instance;
		}
	}*/

	void Awake () {

	}

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;
		lowSignal = Signals[0].GetComponent<Image>();
		medSignal = Signals[1].GetComponent<Image>();
		highSignal = Signals[2].GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckSignal();
	}

	void CheckSignal() {
		if (phone.signalStrength == PhoneManager.Signal.High) {
			lowSignal.enabled = true;
			lowSignal.color = Color.green;
			medSignal.enabled = true;
			medSignal.color = Color.green;
			highSignal.enabled = true;
			highSignal.color = Color.green;
		}
		else if (phone.signalStrength == PhoneManager.Signal.Med) {
			lowSignal.enabled = true;
			lowSignal.color = Color.yellow;
			medSignal.enabled = true;
			medSignal.color = Color.yellow;
			highSignal.enabled = false;
        }
		else if (phone.signalStrength == PhoneManager.Signal.Low)
		{
			lowSignal.enabled = true;
			lowSignal.color = Color.red;
			medSignal.enabled = false;
			highSignal.enabled = false;
		}
	}
}
