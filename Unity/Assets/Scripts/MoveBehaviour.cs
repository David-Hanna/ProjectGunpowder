using UnityEngine;
using System.Collections;

public class MoveBehaviour : MonoBehaviour {

	public abstract class MoveState {

		public Transform ownerTransform		{ get; set; }
		public float speed 					{ get; set; }
		public string stateName				{ get; set; }

		public MoveState (Transform transform, float initialSpeed) {

			ownerTransform = transform;
			speed = initialSpeed;
		}

		public abstract void Update ();
	}

	public class MoveState_NoMove : MoveState {

		public MoveState_NoMove(Transform transform, float initialSpeed) : base(transform, initialSpeed) {
			stateName = "NoMove";
		}

		public override void Update() {}
	}

	public class MoveState_PlayerControlled : MoveState {

		public MoveState_PlayerControlled(Transform transform, float initialSpeed) : base(transform, initialSpeed) {
			stateName = "PlayerControlled";
		}

		public override void Update () {

			float horizontal = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
			float vertical = Input.GetAxis ("Vertical") * speed * Time.deltaTime;

			ownerTransform.Translate (new Vector3 (horizontal, vertical, 0.0f));
		}
	}

	public string initialState;
	public float speed;

	private MoveState currentMoveState;

	void Start () {
		ChangeState (initialState, speed);
	}

	void Update () {
		currentMoveState.Update ();
	}

	public string GetCurrentState() {
		return currentMoveState.stateName;
	}

	public void ChangeState(string newState) {
		
		ChangeState (newState, currentMoveState.speed);
	}

	public void ChangeState(string newState, float speed) {

		switch (newState) {

		case "NoMove":
			currentMoveState = new MoveState_NoMove(transform, speed);
			break;
		case "PlayerControlled":
			currentMoveState = new MoveState_PlayerControlled(transform, speed);
			break;
		default:
			throw new UnityException("\n--------------------------------------\nUnknown 'MoveState' passed to 'MonoBehaviour': " + newState + "\n--------------------------------------");
		}
	}
}
