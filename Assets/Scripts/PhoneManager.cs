using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhoneManager : MonoBehaviour {

	public GameObject screen;
	public Camera frontCamera;
	public Camera selfieCamera;
	public Material frontCamMaterial;
	public Material selfieCamMaterial;

	public enum View {Front, Selfie};
	public View currentView;

	private static PhoneManager instance;
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
		screenRenderer = screen.GetComponent<MeshRenderer>();
		screenRenderer.material = frontCamMaterial;

		selfieCameraState = selfieCamera.GetComponent<CameraState>();
		//frontCameraState = frontCamera.GetComponent<CameraState>();

		currentView = View.Front;
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
		//if (currentView == View.Front)
			TakePic();
		//else if (currentView == View.Selfie)
		//	SwitchView();
	}

	/*void SelfieViewButton() {
		Debug.Log("Right Click");
		if (currentView == View.Selfie)
			TakePic();
		else if (currentView == View.Front)
			SwitchView();
	}*/

	/// <summary>
	/// Switches screen to opposite view
	/// </summary>
	public void SwitchView() {
		//Change view to selfie
		if (currentView == View.Front) {
			screenRenderer.material = selfieCamMaterial;
			currentView = View.Selfie;
		}
		//Change view to front
		else if (currentView == View.Selfie) {
			screenRenderer.material = frontCamMaterial;
			currentView = View.Front;
		}
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

		//Freeze or unfreeze the screen
		currentCameraState = currentCamera.GetComponent<CameraState>();
		if (currentCameraState.IsFrozen)
			currentCameraState.Unfreeze();
		else if (!currentCameraState.IsFrozen)
			currentCameraState.Freeze();

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
