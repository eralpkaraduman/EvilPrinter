using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public Transform printerBodyPivot;
	public Transform cameraStrifePivot;

	private float maxBodyRotaionMoveAngle = 35;
	private float maxStrifeAngle = 6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float normalizedAngle = printerBodyPivot.rotation.eulerAngles.y - 180;
		int sign = normalizedAngle < 0 ? -1 : 1;
		normalizedAngle = Mathf.Abs (normalizedAngle);
		normalizedAngle = Mathf.Min (maxBodyRotaionMoveAngle, normalizedAngle);

		float strifeAngle = maxStrifeAngle / maxBodyRotaionMoveAngle * normalizedAngle;
		strifeAngle *= sign;

		Vector3 strifeEuler = cameraStrifePivot.eulerAngles;
		strifeEuler.y = strifeAngle + 180;
		cameraStrifePivot.eulerAngles = strifeEuler;

	}
}
