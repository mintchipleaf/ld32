using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private PhoneManager phone;
	private bool markedVisible;

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;
		markedVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
		phone.CheckCameraVisibility(gameObject);
	}

	public bool MarkedVisible {
		get {return markedVisible;}
		set {markedVisible = value;}
	}

	/*public void Kill() {
		//phone.RemoveFromVisible(gameObject);
		Destroy(gameObject);
	}*/
}
