using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerScript : MonoBehaviour {

	[SerializeField] public float moveSpeed;
	private CharacterController controller;

	//private Vector3 forwardDirection;
	//public Quaternion currentRotation;

	public static float MAX_PITCH= 80f;
	public static float MIN_PITCH = -80f;
	private float currentPitch = 0;

	private Vector3 lookDirection;

	public float currentRotationY = 0f;
	public float currentRotationX = 0f;

	private float rotationSpeed = 10f;

	private 

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		lookDirection = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		move();
		rotateWithMouse();
	}

	public void move(){
		//controller.Move(new Vector3(Input.GetK), 0f, Input.GetAxis("Vertical")));

		Vector3 moveDirection = Vector3.Scale(transform.rotation * new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 1f)).normalized;

		if(Input.GetKey(KeyCode.W)){
			controller.Move(moveDirection * moveSpeed);
		}

	}

	public void rotateWithMouse(){

		 yaw(Input.GetAxis("Mouse X") * rotationSpeed);
		 pitch(Input.GetAxis("Mouse Y") * -rotationSpeed);

		if(Input.GetKeyDown(KeyCode.A)){
			yaw(rotationSpeed);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			yaw(rotationSpeed * -1);
		}
		updateLookDirection();
	}

	public void yaw(float eulerAngle){
		
		Quaternion rotU = Quaternion.AngleAxis(eulerAngle, Vector3.up);
		transform.rotation = rotU *transform.rotation;

		//currentRotationY += eulerAngle;
		//currentRotationY = currentRotationY % 360;
		//transform.Rotate(Vector3.up * eulerAngle);
		
	}

	public void pitch(float eulerAngle){
		float newFloat = eulerAngle;
		if(currentPitch + eulerAngle > MAX_PITCH){
			newFloat = MAX_PITCH - currentPitch;
		}
		else if(currentPitch + eulerAngle < MIN_PITCH){
			newFloat = MIN_PITCH - currentPitch;
		}

		currentPitch += newFloat;

		Quaternion rotR = Quaternion.AngleAxis(newFloat, Vector3.right);
		transform.rotation = transform.rotation * rotR;

		/* 
		currentRotationX += eulerAngle;
		currentRotationX = currentRotationX % 360;

		if(currentRotationX >= 90f){
			currentRotationX = 90f;
		}
		else if(currentRotationX <= -90f){
			currentRotationX = -90f;
		}

		transform.Rotate(Vector3.right * eulerAngle);
		*/
	}

	private void updateLookDirection(){
		lookDirection = new Vector3(0f, currentRotationY, 0f);

	}

}
