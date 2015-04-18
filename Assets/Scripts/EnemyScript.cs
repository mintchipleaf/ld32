using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public PhoneManager phone;

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		phone.CheckCameraVisibility(gameObject);
	}

	/*public void Kill() {
		//phone.RemoveFromVisible(gameObject);
		Destroy(gameObject);
	}*/
}
