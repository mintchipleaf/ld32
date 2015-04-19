using UnityEngine;
using System.Collections;

public class SignalZone : MonoBehaviour {

	public enum Signal {Low, Med};
	public Signal signalStrength;
	

	private PhoneOverlayManager.Signal signal;
	private PhoneOverlayManager overlay;

	void Awake () {
		if(signalStrength == Signal.Low)
			signal = PhoneOverlayManager.Signal.Low;
		else if (signalStrength == Signal.Med)
			signal = PhoneOverlayManager.Signal.Med;
	}

	// Use this for initialization
	void Start () {
		overlay = PhoneOverlayManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 	void OnTriggerEnter (Collider collider) {
		if(collider.gameObject.CompareTag("Phone"))
			overlay.signalStrength = signal;
	}

	void OnTriggerExit (Collider collider) {
		//This badly assumes all zones concentric
		if(signalStrength == Signal.Med)
			overlay.signalStrength = PhoneOverlayManager.Signal.High;
		else if (signalStrength == Signal.Low)
			overlay.signalStrength = PhoneOverlayManager.Signal.Med;
	}
}
