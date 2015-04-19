using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PhoneOverlayManager : MonoBehaviour {

	public GameObject signalBarGroup;
	public List<GameObject> signals;

	public GameObject uploadBarGroup;
	public List<GameObject> uploadBars;

	public enum UploadSpeed {Low, Med, High};
	public UploadSpeed speed;

	private PhoneManager phone;
	private PhoneManager.Signal currentSignal;
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
		lowSignal = signals[0].GetComponent<Image>();
		medSignal = signals[1].GetComponent<Image>();
		highSignal = signals[2].GetComponent<Image>();
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
	
	public void UploadVisible (bool state) {
		uploadBarGroup.SetActive(state);
	}

	public void SignalVisible (bool state) {
		signalBarGroup.SetActive(state);
	}
}
