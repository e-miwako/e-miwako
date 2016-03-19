using UnityEngine;
using System.Collections;

//
// ↑↓キーでループアニメーションを切り替えるスクリプト（ランダム切り替え付き）Ver.3
// 2014/04/03 N.Kobayashi
//

// Require these components when using this script
[RequireComponent(typeof(Animator))]



public class IdleChanger : MonoBehaviour
{
	public GameObject chatterObject;
	
	private Animator anim;						// Animatorへの参照
	private AnimatorStateInfo currentState;		// 現在のステート状態を保存する参照
	private AnimatorStateInfo previousState;	// ひとつ前のステート状態を保存する参照
	public bool _random = true;				// ランダム判定スタートスイッチ
	public float _threshold = 0.5f;				// ランダム判定の閾値
	public float _interval = 2f;				// ランダム判定のインターバル
	//private float _seed = 0.0f;					// ランダム判定用シード
	private bool _isMove = false;
	private const int animationNum = 24;

	public enum AvatorDirection {
		UP,
		DOWN,
		RIGHT,
		LEFT
	};

	public static readonly string[,] animationList = 
	{
		{"HANDUP00_R",	"3.0"}, 				// 0
		{"JUMP00",		"1.25"},
		{"JUMP01",		"2.25"},
		{"RUN00_F",		"3.0"}, 
		{"RUN00_R",		"3.0"},
		{"RUN00_L",		"3.0"},					//5
		{"SLIDE00",		"1.15"},
		{"UMATOBI00",	"1.75"},
		{"WAIT00",		"3.0"},
		{"WAIT01",		"6.65"},
		{"WAIT02",		"8.3"},					//10
		{"WAIT03",		"5.25"},
		{"WAIT04",		"5.25"},
		{"WALK00_B",	"5.0"},
		{"WALK00_F",	"5.0"},
		{"WALK00_L",	"5.0"},
		{"WALK00_R",	"5.0"},
		{"WIN00",		"4.0"},
		{"LOSE00",		"3.2"},
		{"DAMAGE00",	"1.03"},
		{"DAMAGE01",	"3.2"},
		{"REFESH00",	"3.75"},
		{"JUMP00B",		"1.25"},
		{"JUMP01B",		"2.02"},
	};
	private const string _MESSAGE_WAIT00 = "・・・";
	private const string _MESSAGE_WAIT01 = "う〜んつっかれたぁ〜";
	private const string _MESSAGE_WAIT02 = "♪";
	private const string _MESSAGE_WAIT03 = "やっほ〜";
	private const string _MESSAGE_WAIT04 = "せんぷーきゃく";


	// Use this for initialization
	void Start ()
	{
		// 各参照の初期化
		anim = GetComponent<Animator> ();
		currentState = anim.GetCurrentAnimatorStateInfo (0);
		previousState = currentState;
		// ランダム判定用関数をスタートする
		StartCoroutine ("RandomChange");
	}
	
	// Update is called once per frame
	void  Update ()
	{
		// ↑キー/スペースが押されたら、ステートを次に送る処理
		if (Input.GetKeyDown ("up")) {
			// ブーリアンNextをtrueにする
//			anim.SetBool ("WALK00_F" true);
			StopCoroutine("RandomChange");
			anim.Play("WALK00_B");
		}
		
		// ↓キーが押されたら、ステートを前に戻す処理
		if (Input.GetKeyDown ("down")) {
			// ブーリアンBackをtrueにする
//			anim.SetBool ("Back", true);
			StopCoroutine("RandomChange");
			anim.Play("WALK00_F");
		}
		// →キーが押されたら、左に曲がる処理
		if (Input.GetKeyDown ("right")) {
			StopCoroutine("RandomChange");
			anim.Play("WALK00_L");
		}

		// ←キーが押されたら、右に曲がる処理
		if (Input.GetKeyDown ("left")) {
			
			anim.Play("WALK00_R");
		}
		if (Input.GetButton("Jump")) {
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			previousState = currentState;
			anim.SetBool ("Next", true);
			StartCoroutine ("RandomChange");
		}
//		// "Next"フラグがtrueの時の処理
//		if (anim.GetBool ("Next")) {
//			// 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
//			currentState = anim.GetCurrentAnimatorStateInfo (0);
//			if (previousState.nameHash != currentState.nameHash) {
//				anim.SetBool ("Next", false);
//				previousState = currentState;				
//			}
//		}
//		
//		// "Back"フラグがtrueの時の処理
//		if (anim.GetBool ("Back")) {
//			// 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
//			currentState = anim.GetCurrentAnimatorStateInfo (0);
//			if (previousState.nameHash != currentState.nameHash) {
//				anim.SetBool ("Back", false);
//				previousState = currentState;
//			}
//		}
	}


	void OnGUI()
	{
				GUI.Box(new Rect(Screen.width - 110 , 10 ,100 ,90), "Change Motion");
				if(GUI.Button(new Rect(Screen.width - 100 , 40 ,80, 20), "Next"))
					anim.SetBool ("Next", true);
				if(GUI.Button(new Rect(Screen.width - 100 , 70 ,80, 20), "Back"))
					anim.SetBool ("Back", true);
	}

	private const float playTime = 10.0f;
	private bool _isWait = false;
	// ランダム判定用関数
	IEnumerator RandomChange ()
	{			
		while (true) {
			_interval = 0;
			if (currentState.normalizedTime > 0) {
				yield return null;
			}
			int animationNo = Random.Range (8, 17);
			if (!_isWait) {
				animationNo = 8;
			}

			string animationName = animationList [animationNo, 0];
			// アニメーション開始
			anim.Play (animationName);
			this.chattering (animationName);
			if (animationName == "WAIT00") {
				_isWait = true;
			} 
			else {
				_isWait = false;
			}
			bool isWalk = false;
			if (animationName.IndexOf ("WALK") >= 0) {
				isWalk = true;
			}

			currentState = anim.GetCurrentAnimatorStateInfo (0);
			previousState = currentState;
	
			// 移動＆吹き出し
			_interval = float.Parse(animationList [animationNo,1]);
			if (isWalk) {
				Vector3 pos = this.gameObject.transform.position;
				Vector3 movePos = new Vector3 ();
				movePos.x = pos.x;
				movePos.y = pos.y;
				movePos.z = pos.z;
					
				if (animationName == "WALK00_B") {
					movePos.z += 2.5f;
				} else if (animationName == "WALK00_L") {
					movePos.x += 2.5f;
					movePos.z -= 2.5f;
				} else if (animationName == "WALK00_R") {
					movePos.x -= 2.5f;
					movePos.z -= 2.5f;
				} else if (animationName == "WALK00_F") {
					movePos.z -= 2.5f;
				}
				Hashtable table = new Hashtable();	// 事前にハッシュテーブルを用意しておく。 

				table.Add( "x", movePos.x ); 
				table.Add( "y", movePos.y ); 
				table.Add( "z", movePos.z );
				table.Add( "time", _interval ); 
				table.Add("easeType", iTween.EaseType.linear);
				iTween.MoveTo( this.gameObject, table ); 
			}
//			if (animationName.IndexOf ("WALK00") >= 0) {
//				_interval = 10.0f;
//			}
			Debug.Log ("Animation : " + animationName);
			Debug.Log (" _interval : " + _interval);
			// 次の判定までインターバルを置く
			yield return new WaitForSeconds (_interval);
		}
	}


	// 移動メソッド
	IEnumerator moveAvatar(AvatorDirection aD)
	{
		if (AvatorDirection.UP == aD) {
			
		} else if (AvatorDirection.DOWN == aD) {

		} else if (AvatorDirection.LEFT == aD) {

		} else {
		}

		_isMove = true;
		yield return null;
	}

	void stopAatar()
	{
		if (_isMove) {
			StopCoroutine ("moveAvatar");
			_isMove = false;
		}
	}

	void chattering(string animationName)
	{
		
	}
}
