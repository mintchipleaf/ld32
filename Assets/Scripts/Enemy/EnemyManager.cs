using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public List<GameObject> enemies;

	private PhoneManager phone;
	private static EnemyManager instance;
	private bool gameOver;

	public static EnemyManager Instance {
		get {
			if(instance == null)
				instance = GameObject.FindObjectOfType<EnemyManager>();
			return instance;
		}
	}

	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		phone = PhoneManager.Instance;

	}
	
	// Update is called once per frame
	void Update () {

		gameOver = true;
		foreach (GameObject obj in enemies)
		{
			if (obj != null)
				gameOver = false;
		}
		if(gameOver)
			phone.EndGame();
	}

	public void RemoveFromEnemies(GameObject obj) {
		enemies.RemoveAt(enemies.IndexOf(obj));
	}
}
