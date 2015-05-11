using UnityEngine;
using System.Collections;

public class Printer : MonoBehaviour {

	public static int maxKillCount = 30;
	public static float maxKillTime = 25.0f;

	public GameObject CursorPlaneObject = null;
	public GameObject Cursor = null;
	public Plane CursorPlane;
	public GameObject dragLine;
	public GameObject body;
	public Transform bodyPivot;
	public Transform cameraPivot;
	public Transform bodyLeanPivot;
	public PaperShooter shooter;

	public GameObject introUI;

	public float refillTime = 0.02f;

	private float lastShootTime = 0.0f;
	private float timeElapsed = 0.0f;

	private MeshCollider cursorPlaneCollider;

	public float maxDragDist = 12;
	public float minDragDist = 2;

	public bool started = false;

	public float maxLean = 15;

	float percent_drag = 0;

	public int killCount = 0;


	private Vector3 dragStartPos;
	//public Vector3 dragStartPosOffset = Vector3.zero;

	public bool debugMouseCursorVisible = true;

	private bool dragging = false;
	private bool canShoot = false;
	public bool refilling = false;

	private GUIStyle labelStyle;

	public static Printer instance;

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

		Printer.instance = this;

		labelStyle = new GUIStyle ();
		labelStyle.fontSize = 20;
		labelStyle.fontStyle = FontStyle.Bold;
		labelStyle.alignment = TextAnchor.MiddleCenter;

		lastShootTime = 0;
		timeElapsed = 0;

		cursorPlaneCollider = (MeshCollider)CursorPlaneObject.GetComponent<MeshCollider> ();

		dragStartPos = this.transform.position;
		//dragStartPos += dragStartPosOffset;
		CursorPlane = new Plane (CursorPlaneObject.transform.position,Vector3.up);

		Cursor.GetComponent<Renderer>().enabled = debugMouseCursorVisible;
	}

	// Update is called once per frame
	void Update () {

		if(started)timeElapsed += Time.deltaTime;

		// check game

		if (timeElapsed >= maxKillTime) {
			if (killCount >= maxKillCount) {
				Application.LoadLevel ("WIN");
			} else {
				Application.LoadLevel ("Lost");
			}
			return;
		} else if (killCount >= maxKillCount) {
			Application.LoadLevel ("WIN");
			return;
		}

		if (timeElapsed > lastShootTime + refillTime) {
			refilling = false;
		} else {
			refilling = true;
		}

		MouseResult mouseRes = this.mousePos ();

		float dragDistance = Vector3.Distance(dragStartPos,mouseRes.mousePos);

		percent_drag = (dragDistance - minDragDist) / (maxDragDist - minDragDist);
		percent_drag = Mathf.Max (0, percent_drag);
		percent_drag = Mathf.Min (1, percent_drag);

		// dragline
		Cursor.transform.position = mouseRes.mousePos;
		Vector3 mid = (mouseRes.mousePos -dragStartPos)/2;
		dragLine.transform.localPosition = mid;
		dragLine.transform.LookAt (dragStartPos);
		Vector3 dragLineScale = dragLine.transform.localScale;
		dragLineScale.z = dragDistance;
		dragLine.transform.localScale = dragLineScale;

		// aim
		Vector3 bodyLookTarget = body.transform.position;
		bodyLookTarget.z = mouseRes.mousePos.z;
		bodyLookTarget.x = mouseRes.mousePos.x;

		float lean = 0;

		canShoot = false;

		if (dragging) {
			canShoot = true;
			lean = -percent_drag*maxLean;
		} else {
			Vector3 forward = bodyPivot.transform.position;
			forward.z -= 10;
			bodyLookTarget = forward;
		}

		if (dragDistance > minDragDist) {

			canShoot = true;

			dragLine.GetComponent<Renderer> ().enabled = dragging;
			bodyPivot.LookAt (bodyLookTarget);

			// lean
			Vector3 leanRotation = bodyLeanPivot.eulerAngles;
			leanRotation.x = lean;
			bodyLeanPivot.transform.eulerAngles = leanRotation;

		} else {
			canShoot = false;
		}
	}

	void OnMouseDown(){


		if (!started) {
			GameObject.Destroy(introUI);
		}
		started = true;

		if (refilling)
			return;

		//if (!canShoot)return;


		//mousePos().mousePos
		cursorPlaneCollider.enabled = true;
		dragging = true;
	}

	void OnGUI() {

		GUI.color = Color.red;

		if (refilling) {

			GUI.Label (new Rect (0, Screen.height-200, Screen.width, 20), "REFILLING!...", labelStyle);
		}

		GUI.Label (new Rect (0, 0, Screen.width, 30), "time: " + Mathf.Round(timeElapsed) + ", kill: " + killCount,labelStyle);
	}

	void OnMouseUp(){

		cursorPlaneCollider.enabled = false;



		if (canShoot) {
			shooter.shoot(percent_drag);
			lastShootTime = timeElapsed;
		}

		dragging = false;
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
