using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof(Animator))]

public class UnityChanContrrol : MonoBehaviour {
	private Animator anim;						// Animatorへの参照
	private AnimatorStateInfo currentState;		// 現在のステート状態を保存する参照
	private AnimatorStateInfo previousState;	// ひとつ前のステート状態を保存する参照
	public bool _random = false;				// ランダム判定スタートスイッチ
	public float _threshold = 0.5f;				// ランダム判定の閾値
	public float _interval = 2f;				// ランダム判定のインターバル
	//private float _seed = 0.0f;					// ランダム判定用シード
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// ↑キー/スペースが押されたら、ステートを次に送る処理
		if (Input.GetKeyDown ("up") || Input.GetButton ("Jump")) {
			// ブーリアンNextをtrueにする
			anim.SetBool ("Next", true);
		
		}

		// ↓キーが押されたら、ステートを前に戻す処理
		if (Input.GetKeyDown ("down")) {
			// ブーリアンBackをtrueにする
			anim.SetBool ("Back", true);
		}

		// "Next"フラグがtrueの時の処理
		if (anim.GetBool ("Next")) {
			// 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			if (previousState.nameHash != currentState.nameHash) {
				anim.SetBool ("Next", false);
				previousState = currentState;				
			}
		}

		// "Back"フラグがtrueの時の処理
		if (anim.GetBool ("Back")) {
			// 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			if (previousState.nameHash != currentState.nameHash) {
				anim.SetBool ("Back", false);
				previousState = currentState;
			}
		}

	}

	void changeAnimation(string Animation)
	{
	}

	void randamAnimation ()
	{
	}



}
