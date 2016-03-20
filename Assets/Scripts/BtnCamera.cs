using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnCamera : MonoBehaviour {

	private bool cameraFlg;

	private GameObject zombie;
	private ZombieControl zombieCtrl;

	// Use this for initialization
	void Start () {
		cameraFlg = false;
		zombie = GameObject.Find ("Zombie");
		zombieCtrl = zombie.GetComponent<ZombieControl>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			cameraFlg = !cameraFlg;


			zombieCtrl.setCameraFlg (cameraFlg);
		}

	}
}
