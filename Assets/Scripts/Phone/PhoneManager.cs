using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhoneManager : MonoBehaviour {

	public GameObject screen;
	public Camera frontCamera;
	public Camera selfieCamera;
	public Material frontCamMaterial;
	public Material selfieCamMaterial;
	public Material tutorialCamMaterial;
	
	public enum View {Front, Selfie, Tutorial};
	public View currentView;

	public enum Signal {Low, Med, High};
	public Signal signalStrength;

	private static PhoneManager instance;
	private PhoneOverlayManager overlay;
	private MeshRenderer screenRenderer;
	private bool isEnemyInCamera;
	private EnemyScript enemyScript;
	private CameraState selfieCameraState;
	//private CameraState frontCameraState;


	public static PhoneManager Instance {
		get {
			if(instance == null)
				instance = GameObject.FindObjectOfType<PhoneManager>();
			return instance;
		}
	}

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		overlay = PhoneOverlayManager.Instance;

		screenRenderer = screen.GetComponent<MeshRenderer>();
		screenRenderer.material = frontCamMaterial;

		selfieCameraState = selfieCamera.GetComponent<CameraState>();
		//frontCameraState = frontCamera.GetComponent<CameraState>();

		SwitchToTutorial();
		signalStrength = Signal.High;
	}
	
	// Update is called once per frame
	void Update () {
		//Left click
		if(Input.GetMouseButtonDown(0))
			ActionButton();
		//Right click
		else if(Input.GetMouseButtonDown(1))
			SwitchView();
	}

	void ActionButton() {
		Debug.Log("Left Click");
		if (currentView == View.Selfie)
			TakePic();
	}

	/// <summary>
	/// Switches screen to opposite view
	/// </summary>
	public void SwitchView() {
		if (currentView == View.Front)
			SwitchToSelfie();
		else if (currentView == View.Selfie)
			SwitchToFront();
		else if (currentView == View.Tutorial)
			ExitTutorial();
	}

	void SwitchToSelfie() {
		overlay.UploadVisible(true);
		screenRenderer.material = selfieCamMaterial;
		currentView = View.Selfie;
	}

	void SwitchToFront() {
		overlay.UploadVisible(false);
		screenRenderer.material = frontCamMaterial;
		currentView = View.Front;
	}

	void SwitchToTutorial() {
		overlay.UploadVisible(false);
		overlay.SignalVisible(false);
		screenRenderer.material = tutorialCamMaterial;
		currentView = View.Tutorial;
	}

	void ExitTutorial() {
		overlay.UploadVisible(true);
		overlay.SignalVisible(true);
		SwitchToFront();
	}

	/// <summary>
	/// Takes a picture with the current camera
	/// </summary>
	public void TakePic() {
		//Set the camera the pic is taken from
		Camera currentCamera = new Camera();
		CameraState currentCameraState;
		if(currentView == View.Front)
			currentCamera = frontCamera;
		else if (currentView == View.Selfie)
			currentCamera = selfieCamera;

		currentCameraState = currentCamera.GetComponent<CameraState>();
		currentCameraState.TakePicture();
	}

	public void CheckCameraVisibility(GameObject obj) {
		enemyScript = obj.GetComponent<EnemyScript>();
		isEnemyInCamera = obj.GetComponent<Renderer>().IsVisibleFrom(selfieCamera);

		if (isEnemyInCamera && !enemyScript.MarkedVisible) {
			enemyScript.MarkedVisible = true;
			selfieCameraState.AddToVisible(obj);
		}
		else if (!isEnemyInCamera && enemyScript.MarkedVisible) {
			enemyScript.MarkedVisible = false;
			selfieCameraState.RemoveFromVisible(obj);
		}
		//else if(obj.GetComponent<Renderer>().IsVisibleFrom(frontCamera))
		//	frontCamera.GetComponent<CameraState>().AddToVisible(obj);
	}
}
