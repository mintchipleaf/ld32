using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private PhoneManager phone;
	private EnemyManager manager;
	private bool markedVisible;
	private float health;

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;
		manager = EnemyManager.Instance;
		markedVisible = false;
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		phone.CheckCameraVisibility(gameObject);
	}

	public bool MarkedVisible {
		get {return markedVisible;}
		set {markedVisible = value;}
	}

	public float Health {
		get {return health;}
		set {health = value;}
	}

	public void Kill() {
		Destroy(gameObject);
		manager.RemoveFromEnemies(this.gameObject);
	}
}
