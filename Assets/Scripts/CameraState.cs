using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraState : MonoBehaviour {

	private Camera camera;
	private List<GameObject> objectsInView;
	private bool isFrozen;

	void Awake () {
		objectsInView = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
		isFrozen = false;
		camera = GetComponent<Camera>();
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
		foreach (GameObject enemy in objectsInView)
		{
			Destroy(enemy);
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
		camera.clearFlags = CameraClearFlags.Nothing;
		yield return null;
		camera.cullingMask = 0;
		yield return new WaitForSeconds(2.0f);
		Unfreeze();
	}

	IEnumerator UnfreezeCam()
	{
		//yield return null;
		camera.clearFlags = CameraClearFlags.Skybox;
		yield return null;
		camera.cullingMask = 1;
	}

}
