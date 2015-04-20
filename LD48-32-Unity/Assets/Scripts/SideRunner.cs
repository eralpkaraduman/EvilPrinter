using UnityEngine;
using System.Collections;

public class SideRunner : MonoBehaviour {

	public float speed = 25.0f;
	public float startDistance = 50.0f;
	public bool directionRight = true;
	public Transform spriteTransform;

	private float startPos;
	private float endPos;

	// Use this for initialization
	void Start () {

		startPos = directionRight?-startDistance:startDistance;
		endPos = directionRight?startDistance:-startDistance;

		if (directionRight == false) {
			Vector3 euler = spriteTransform.eulerAngles;
			euler.y = 180;
			spriteTransform.eulerAngles = euler;
		}

		//Reset ();
	}

	void Reset(){
		Vector3 pos = transform.position;
		pos.x = startPos;
		transform.position = pos;
	}
	

	void Update () {

		if (directionRight && transform.position.x >= endPos) {
			Reset();
			return;
		}


		if (directionRight==false && transform.position.x <= endPos) {
			Reset();
			return;
		}


		Vector3 direction = directionRight ? Vector3.right : Vector3.left;

		transform.Translate(direction * Time.deltaTime * speed);
	}
}
