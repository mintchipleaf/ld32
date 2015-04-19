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
		signalStrength = Signal.High;
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

	//This should be broken into separate public funcs, called when signal changes
	void CheckSignal() {
		if (signalStrength == Signal.High) {
			phone.currentSignalStrength = Signal.High;
			lowSignal.enabled = true;
			lowSignal.color = Color.green;
			medSignal.enabled = true;
			medSignal.color = Color.green;
			highSignal.enabled = true;
			highSignal.color = Color.green;
		}
		else if (signalStrength == Signal.Med) {
			phone.currentSignalStrength = Signal.Med;
			lowSignal.enabled = true;
			lowSignal.color = Color.yellow;
			medSignal.enabled = true;
			medSignal.color = Color.yellow;
			highSignal.enabled = false;
        }
		else if (signalStrength == Signal.Low) {
			phone.currentSignalStrength = Signal.Low;
			lowSignal.enabled = true;
			lowSignal.color = Color.red;
			medSignal.enabled = false;
			highSignal.enabled = false;
		}
	}

	public void UploadedTo(int tensPercentage) {
		if(tensPercentage == 0)
			foreach (GameObject bar in uploadBars){
				bar.GetComponent<Image>().enabled = false;
			}

		//Convert the percentage (in tens) to the bar list index
		int barToShow = (tensPercentage / 10) - 1;
		uploadBars[barToShow].GetComponent<Image>().enabled = true;

		/*for(int i = 0; i < barToShow; i++)
			uploadBars[i].GetComponent<Image>().enabled = true;
		*/
	}

	public void UploadVisible (bool state) {
		uploadBarGroup.SetActive(state);
	}

	public void SignalVisible (bool state) {
		signalBarGroup.SetActive(state);
	}
}
