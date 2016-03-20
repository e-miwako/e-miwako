using UnityEngine;
using System.Collections;

public class HealthControl : MonoBehaviour {

	private GameObject objHealthBar;
	private GameObject zombie;
	private Material[] matHealthBar = new Material [2];
	private int preHp = 0;
	private float texOffset;
	private ZombieControl zombieCtrl;
	private Transform trans;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();

		zombie = GameObject.Find ("Zombie");
		zombieCtrl = zombie.GetComponent<ZombieControl>();

		objHealthBar = GameObject.Find ("Square");
		matHealthBar = objHealthBar.GetComponent<Renderer>().materials;
		preHp = zombieCtrl.getHp ();
	}

	// Update is called once per frame
	void Update () {

		trans.position = new Vector3 (zombieCtrl.getZombiePosition().x, zombieCtrl.getZombiePosition().y + 2, zombieCtrl.getZombiePosition().z);
			
		if (preHp != zombieCtrl.getHp()) {
			preHp = zombieCtrl.getHp();
			texOffset = (1.0f - zombieCtrl.hp / 100 ) * 0.484f;
			matHealthBar[0].mainTextureOffset = new Vector2(texOffset, 0);

		}
			
	}
}
