using UnityEngine;
using System.Collections;

public class SignalZone : MonoBehaviour {

	public enum Signal {Low, Med};
	public Signal signalStrength;
	

	private PhoneManager.Signal signal;
	private PhoneManager phone;

	void Awake () {
		if(signalStrength == Signal.Low)
			signal = PhoneManager.Signal.Low;
		else if (signalStrength == Signal.Med)
			signal = PhoneManager.Signal.Med;
	}

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 	void OnTriggerEnter (Collider collider) {
		if(collider.gameObject.CompareTag("Phone"))
			phone.signalStrength = signal;
	}

	void OnTriggerExit (Collider collider) {
		//This badly assumes all zones concentric
		if(signalStrength == Signal.Med)
			phone.signalStrength = PhoneManager.Signal.High;
		else if (signalStrength == Signal.Low)
			phone.signalStrength = PhoneManager.Signal.Med;
	}
}
