using UnityEngine;
using System.Collections;

public class PaperShooter : MonoBehaviour {

	public float maxForce = 2200;
	public float minForce = 800;

	public GameObject paperPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shoot(float percentageDrag){

		GameObject paper = GameObject.Instantiate (paperPrefab);
		paper.transform.localPosition = this.transform.position;
		paper.transform.rotation = this.transform.rotation;

		float forceMultiplier = (maxForce - minForce) * percentageDrag + minForce; 
		Rigidbody r = (Rigidbody)paper.GetComponent(typeof(Rigidbody));
		r.AddRelativeForce (Vector3.forward * forceMultiplier);


	}
}
