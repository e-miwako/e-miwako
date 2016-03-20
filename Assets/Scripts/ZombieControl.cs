using UnityEngine;
using System.Collections;

public class ZombieControl : MonoBehaviour {

	public int hp = 100;
	public float speed = 15f;

	private float houkou = 0;
	private bool rl = true;
	private bool cameraFlg = false;

	public Camera cam;

	private Animator anim;
	private Transform trans;
	private CharacterController zombieCtrl;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		trans = GetComponent<Transform> ();
		zombieCtrl = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {

		// 動き制御
		moveSample ();

	}

	private void moveSample() {
		float targetX = Input.GetAxis( "Horizontal" );
		float targetZ = Input.GetAxis( "Vertical" );

		if (zombieCtrl.isGrounded) {
			//移動方向を取得
			moveDirection = new Vector3(targetX, 0f, targetZ);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed / 50;
		}


		moveDirection.y -= 20f * Time.deltaTime;

		// 移動
		zombieCtrl.Move(moveDirection * Time.deltaTime);


		if (Input.GetKey (KeyCode.UpArrow)) {
			anim.Play ("walk");
		} else if (Input.GetKey (KeyCode.Space)) {
			anim.Play ("attack");
		} else {
			anim.Play ("wait");
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (rl == false) {
				rl = true;
				houkou = 0;
			}
			houkou += targetX;
			trans.Rotate (new Vector3 (0, houkou / 50 % 360, 0));
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			if (rl == true) {
				rl = false;
				houkou = 0;
			}
			houkou += targetX;
			trans.Rotate (new Vector3 (0, houkou / 50 % 360, 0));
		} else {
			houkou = 0;
		}

		// カメラ制御
		float x = trans.transform.position.x;
		float y = trans.transform.position.y;
		float z = trans.transform.position.z;

		if (cameraFlg) {
			cam.transform.position = new Vector3(x, y + 1.6f, z) ;
			cam.transform.Rotate (new Vector3 (0, houkou / 50 % 360, 0));
		} else {
			cam.transform.position = new Vector3(x, y + 2f, z + 5f) ;
		}
	}

	public bool getCameraFlg() {
		return cameraFlg;
	}

	public void setCameraFlg(bool camflg) {

		//print (houkou);

		//if (!camflg) {
			cam.transform.Rotate (new Vector3 (0, 180, 0));
		//}

		cameraFlg = camflg;
	}

	public int getHp() {
		return hp;
	}

	public void setHp(int healthPoint) {
		hp = healthPoint;
	}

	public Vector3 getZombiePosition() {
		return trans.position;
	}
}
