using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerScript : MonoBehaviour {

	[SerializeField] public float moveSpeed;
	private CharacterController controller;

	//private Vector3 forwardDirection;
	//public Quaternion currentRotation;

	public static float MAX_X_ROTATION = 90f;
	public static float MIN_X_ROTATION = -90f;
	private float currentPitch = 0;

	private Vector3 lookDirection;

	public float currentRotationY = 0f;
	public float currentRotationX = 0f;

	private float rotationSpeed = 10f;

	private 

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		//currentRotation = Quaternion.identity;
		
		lookDirection = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		move();
		rotateWithMouse();
	}

	public void move(){
		//controller.Move(new Vector3(Input.GetK), 0f, Input.GetAxis("Vertical")));

		if(Input.GetKey(KeyCode.W)){
			controller.Move(transform.rotation * new Vector3(0f, 0f, moveSpeed * Time.deltaTime));
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
		if(currentPitch + eulerAngle > 90f){
			newFloat = 90f - currentPitch;
		}
		else if(currentPitch + eulerAngle < -90f){
			newFloat = -90f - currentPitch;
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
