using UnityEngine;
using System.Collections;

public class Printer : MonoBehaviour {

	public GameObject CursorPlaneObject = null;
	public GameObject Cursor = null;
	public Plane CursorPlane;

	public Vector3 dragStartPos = Vector3.zero;


	// Use this for initialization
	void Start () {
		CursorPlane = new Plane (CursorPlaneObject.transform.position,Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.transform.position = MousePos ();
	}

	void OnMouseDown(){

	}

	void OnMouseUp(){

	}

	Vector3 MousePos(){

		float dist;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit = new RaycastHit();

		if (CursorPlaneObject.GetComponent<Collider>().Raycast (ray,out hit, 3000.0f)) {

			return ray.GetPoint(hit.distance);
		}
		return dragStartPos;


		Vector3 mousePoint = Camera.main.ScreenToViewportPoint (Input.mousePosition);
		return mousePoint;
	}
}
