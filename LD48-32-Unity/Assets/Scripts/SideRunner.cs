using UnityEngine;
using System.Collections;

public class SideRunner : MonoBehaviour {

	public float speed = 5.0f;
	public float endPos = 24.0f;
	public float startPos = -24.0f;


	// Use this for initialization
	void Start () {
		Reset ();
	}

	void Reset(){
		Vector3 pos = transform.position;
		pos.x = startPos;
		transform.position = pos;
	}
	

	void Update () {

		if (transform.position.x >= endPos) {
			Reset();
			return;
		}

		transform.Translate(Vector3.right * Time.deltaTime * speed);
	}
}
