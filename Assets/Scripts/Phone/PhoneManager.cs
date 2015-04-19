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

	public bool isUploading;
	public PhoneOverlayManager.Signal currentSignalStrength;


	private static PhoneManager instance;
	private PhoneOverlayManager overlay;
	private MeshRenderer screenRenderer;
	private bool isEnemyInCamera;
	private EnemyScript enemyScript;
	private CameraState selfieCameraState;
	//private CameraState frontCameraState;
	private float uploadPercentage;



	public static PhoneManager Instance {
		get {
			if(instance == null)
				instance = GameObject.FindObjectOfType<PhoneManager>();
			return instance;
		}
	}

	void Awake() {
		instance = this;
		uploadPercentage = 0;
	}

	// Use this for initialization
	void Start () {
		overlay = PhoneOverlayManager.Instance;

		screenRenderer = screen.GetComponent<MeshRenderer>();
		screenRenderer.material = frontCamMaterial;

		selfieCameraState = selfieCamera.GetComponent<CameraState>();
		//frontCameraState = frontCamera.GetComponent<CameraState>();

		currentSignalStrength = overlay.signalStrength;

		SwitchToTutorial();
	}
	
	// Update is called once per frame
	void Update () {
		//Left click
		if(Input.GetMouseButtonDown(0))
			ActionButton();
		//Right click
		else if(Input.GetMouseButtonDown(1))
			SwitchView();
		CheckUpload();
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

	/// <summary>
	///	Starts the upload process
	/// </summary>
	public void Upload() {
		uploadPercentage = 0;
		isUploading = true;

		//StartCoroutine(UploadTime());
		//isUploading = false;

	}

	void CheckUpload() {
		if(!isUploading)
			return;
		if(uploadPercentage >= 100) {
			FinishUpload();
			return;
		}
			uploadPercentage += 1;
			if(uploadPercentage % 10 == 0)
				overlay.UploadedTo((int)uploadPercentage);
			//overlay.UploadedTo((int)uploadPercentage);
	}
	
	void FinishUpload() {
		foreach (GameObject obj in selfieCameraState.objectsInView)
			Destroy(obj);
		selfieCameraState.Unfreeze();
		uploadPercentage = 0;
		isUploading = false;
	}

	/// <summary>
	///Checks if obj is visible to the selfie camera
	/// </summary>
	/// <param name="obj">Object to check visibility of</param>
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

	IEnumerator UploadTime() {
		while(uploadPercentage < 100) {
			uploadPercentage += .00001f;
			if(uploadPercentage == 10)
				overlay.UploadedTo(10);
			else if(uploadPercentage == 20)
				overlay.UploadedTo(20);
			else if(uploadPercentage == 30)
				overlay.UploadedTo(30);
			else if(uploadPercentage == 40)
				overlay.UploadedTo(40);
			else if(uploadPercentage == 50)
				overlay.UploadedTo(50);
			else if(uploadPercentage == 60)
				overlay.UploadedTo(60);
			else if(uploadPercentage == 70)
				overlay.UploadedTo(70);
			else if(uploadPercentage == 80)
				overlay.UploadedTo(80);
			else if(uploadPercentage == 90)
				overlay.UploadedTo(90);
			else if(uploadPercentage == 100)
				overlay.UploadedTo(100);
			//overlay.UploadedTo((int)uploadPercentage);
		}


		yield return null;
	}
}
