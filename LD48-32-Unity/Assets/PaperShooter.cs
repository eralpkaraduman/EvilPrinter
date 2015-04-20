using UnityEngine;
using System.Collections;

public class PaperShooter : MonoBehaviour {

	public GameObject paperPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shoot(){

		GameObject paper = GameObject.Instantiate (paperPrefab);
		paper.transform.localPosition = this.transform.position;
		paper.transform.rotation = this.transform.rotation;


	}
}
