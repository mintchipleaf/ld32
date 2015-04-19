using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraState : MonoBehaviour {

	public List<GameObject> objectsInView;
	
	private PhoneManager phone;
	private Camera thisCamera;
	private bool isFrozen;
	private int oldMask;

	void Awake () {
		objectsInView = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;
		isFrozen = false;
		thisCamera = GetComponent<Camera>();
		oldMask = thisCamera.cullingMask;
	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// Adds object to list of objects visible from camera
	/// </summary>
	public void AddToVisible(GameObject obj) {
		objectsInView.Add(obj);
	}
	
	/// <summary>
	/// Removes object from list of objects visible to camera
	/// </summary>
	/// <param name="obj">Object.</param>
	public void RemoveFromVisible(GameObject obj) {
		objectsInView.Remove(obj);
	}

	/// <summary>
	/// Logic for when a pic is taken with this camera
	/// </summary>
	public void TakePicture() {
		if(IsFrozen)
			return;
		else {
			Freeze();
			phone.Upload();
			//foreach (GameObject obj in objectsInView)
			//Unfreeze();
		}
	}

	public bool IsFrozen {
		get {return isFrozen;}
	}

	public void Freeze() {
		StartCoroutine(FreezeCam());
		isFrozen = true;
	}

	public void Unfreeze() {
		StartCoroutine(UnfreezeCam());
		isFrozen = false;
	}

	IEnumerator FreezeCam()
	{
		//yield return null;
		thisCamera.clearFlags = CameraClearFlags.Nothing;
		yield return null;
		thisCamera.cullingMask = 0;
		//yield return new WaitForSeconds(2.0f);
		//Unfreeze();
	}

	IEnumerator UnfreezeCam()
	{
		//yield return null;
		thisCamera.clearFlags = CameraClearFlags.Skybox;
		yield return null;
		thisCamera.cullingMask = oldMask;
	}

}
