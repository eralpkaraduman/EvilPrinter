using UnityEngine;
using System.Collections;

public class Printer : MonoBehaviour {

	public GameObject CursorPlaneObject = null;
	public GameObject Cursor = null;
	public Plane CursorPlane;

	public Vector3 dragStartPos = Vector3.zero;


	private class MouseResult : Object{
		public Vector3 mousePos;
		public float distance;

		public MouseResult(Vector3 mousePos, float distance){
			this.mousePos = mousePos;
			this.distance = distance;
		}
	}

	// Use this for initialization
	void Start () {
		CursorPlane = new Plane (CursorPlaneObject.transform.position,Vector3.up);
	}

	// Update is called once per frame
	void Update () {
		MouseResult mouseRes = this.mousePos ();

		Cursor.transform.position = mouseRes.mousePos;
	}

	void OnMouseDown(){

	}

	void OnMouseUp(){

	}

	MouseResult mousePos(){

		float dist = 0.0f;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		Vector3 mousePos = dragStartPos;

		RaycastHit hit = new RaycastHit();

		if (CursorPlaneObject.GetComponent<Collider>().Raycast (ray,out hit, 3000.0f)) {

			mousePos = ray.GetPoint(hit.distance);
		}

		//Vector3 mousePoint = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		//return mousePoint;

		return new MouseResult (mousePos,dist);
	}
}
