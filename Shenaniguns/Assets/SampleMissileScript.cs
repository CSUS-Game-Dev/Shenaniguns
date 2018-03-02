using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMissileScript : MonoBehaviour {

	private float speed = 300f;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody>();
		rigidbody.velocity = transform.rotation * new Vector3(0f, 0f, speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
