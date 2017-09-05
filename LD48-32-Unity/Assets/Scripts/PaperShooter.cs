using UnityEngine;
using System.Collections;

public class PaperShooter : MonoBehaviour {

	public float maxForce = 1650;
	public float minForce = 500;
	public AudioSource motorAudioSource;
	public AudioClip[] motorSounds;

	public AudioSource shootAudioSource;


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

		int soundIndex = Random.Range (0, motorSounds.Length);
		motorAudioSource.clip = motorSounds [soundIndex];
		motorAudioSource.PlayDelayed (0.2f);

		shootAudioSource.Play ();
	}
}
