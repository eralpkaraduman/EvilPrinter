using UnityEngine;
using System.Collections;

public class Printer : MonoBehaviour {

	public GameObject CursorPlaneObject = null;
	public GameObject Cursor = null;
	public Plane CursorPlane;
	public GameObject dragLine;
	public GameObject body;
	public Transform bodyPivot;

	private Vector3 dragStartPos;
	public Vector3 dragStartPosOffset = Vector3.zero;

	public bool debugMouseCursorVisible = true;


	private class MouseResult : Object{
		public Vector3 mousePos;
		public float distance;
		public Ray ray;

		public MouseResult(Vector3 mousePos, float distance, Ray ray){
			this.mousePos = mousePos;
			this.distance = distance;
			this.ray = ray;
		}
	}

	// Use this for initialization
	void Start () {
		dragStartPos = this.transform.position;
		dragStartPos += dragStartPosOffset;
		CursorPlane = new Plane (CursorPlaneObject.transform.position,Vector3.up);

		Cursor.GetComponent<Renderer>().enabled = debugMouseCursorVisible;
	}

	// Update is called once per frame
	void Update () {
		MouseResult mouseRes = this.mousePos ();

		Cursor.transform.position = mouseRes.mousePos;

		Vector3 mid = (mouseRes.mousePos -dragStartPos)/2;

		//Vector3 dragLinePos = mouseRes.ray.GetPoint (mouseRes.distance / 2);

		dragLine.transform.localPosition = mid;
		dragLine.transform.LookAt (dragStartPos);

		Vector3 dragLineScale = dragLine.transform.localScale;
		dragLineScale.z = Vector3.Distance(dragStartPos,mouseRes.mousePos);
		dragLine.transform.localScale = dragLineScale;

		Vector3 bodyLookTarget = body.transform.position;
		bodyLookTarget.z = mouseRes.mousePos.z;
		bodyLookTarget.x = mouseRes.mousePos.x;
		bodyPivot.LookAt (bodyLookTarget);
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

			dist = hit.distance;
			mousePos = ray.GetPoint(dist);
		}

		return new MouseResult (mousePos,dist,ray);
	}
}
