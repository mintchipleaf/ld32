using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PhoneOverlayManager : MonoBehaviour {

	public GameObject signalBarGroup;
	public List<GameObject> signals;

	public GameObject uploadBarGroup;
	public List<GameObject> uploadBars;

	public enum Signal {Low, Med, High};
	public Signal signalStrength;

	private PhoneManager phone;
	private Image lowSignal;
	private Image medSignal;
	private Image highSignal;

	private static PhoneOverlayManager instance;

	public static PhoneOverlayManager Instance {
		get {
			if(instance == null)
				instance = GameObject.FindObjectOfType<PhoneOverlayManager>();
			return instance;
		}
	}

	void Awake () {
		UploadVisible(false);
	}

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;
		signalStrength = Signal.High;
		lowSignal = signals[0].GetComponent<Image>();
		medSignal = signals[1].GetComponent<Image>();
		highSignal = signals[2].GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckSignal();
	}

	void CheckSignal() {
		if (signalStrength == Signal.High) {
			lowSignal.enabled = true;
			lowSignal.color = Color.green;
			medSignal.enabled = true;
			medSignal.color = Color.green;
			highSignal.enabled = true;
			highSignal.color = Color.green;
		}
		else if (signalStrength == Signal.Med) {
			lowSignal.enabled = true;
			lowSignal.color = Color.yellow;
			medSignal.enabled = true;
			medSignal.color = Color.yellow;
			highSignal.enabled = false;
        }
		else if (signalStrength == Signal.Low)
		{
			lowSignal.enabled = true;
			lowSignal.color = Color.red;
			medSignal.enabled = false;
			highSignal.enabled = false;
		}
	}
	
	public void UploadVisible (bool state) {
		uploadBarGroup.SetActive(state);
	}

	public void SignalVisible (bool state) {
		signalBarGroup.SetActive(state);
	}
}
