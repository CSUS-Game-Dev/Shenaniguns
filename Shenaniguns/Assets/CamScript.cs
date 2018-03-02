using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]

public class ThirdPersonCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    private const float DIST_MAX = 15.0f;
    private const float DIST_MIN = 5.0f;

    [Header("Transforms")]
    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    [Header("CameraSettings")]
    public Vector3 distance;
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    public float sensivityX = 4.0f;
    public float sensivityY = 1.0f;

    public float lerpSpeed = 1.0f;

    public float hideFloat = 0.5f;

    public float rotateSpeed = 0.75f;
    public float scrollTimes;

    public LayerMask wallLayers;
    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensivityX;
        currentY += Input.GetAxis("Mouse Y") * sensivityY;

        currentX = Mathf.Repeat(currentX, 360.0f);
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    void LateUpdate()
    {

        if (!lookAt || !camTransform)
        {
            return;
        }

        Vector3 camDir = new Vector3(0, 0, -distance.z);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * camDir;

        camTransform.LookAt(lookAt.position);
        Vector3 dir = camTransform.position - lookAt.position;

        Vector3 eulerAngleAxis = new Vector3(0, currentX);
        Quaternion newRotation = Quaternion.Slerp(lookAt.localRotation, Quaternion.Euler(eulerAngleAxis), Time.deltaTime * rotateSpeed);
        lookAt.localRotation = newRotation;

        


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            distance.z -= 0.2f * scrollTimes;

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            distance.z += 0.2f *scrollTimes;

        }

        distance.z = Mathf.Clamp(distance.z, DIST_MIN, DIST_MAX);

        WallCheck();



    }

    void WallCheck()
    {
        if(!lookAt || !camTransform)
        {
            return;
        }

        RaycastHit hit;

        Transform mainCamT = camTransform;
        Vector3 mainCamPos = mainCamT.position;
        Vector3 pivotPos = lookAt.position;

        Vector3 start = pivotPos;
        Vector3 dir = mainCamPos - pivotPos;

        if (Physics.SphereCast(start, cam.nearClipPlane, dir, out hit, distance.z, wallLayers))
        {
            MoveCamUp(hit, pivotPos, dir, mainCamT);
        }
    }
    void MoveCamUp(RaycastHit hit, Vector3 lookatPos, Vector3 dir, Transform camTrans) // during collision move cam out of the way
    {
        float hitDist = hit.distance;
        Vector3 sphereCenter = lookatPos + (dir.normalized * hitDist);
        camTrans.position = sphereCenter;
    } 

    void PositionCam(Vector3 cameraPos) //for thirdperson shoulder shifting
    {
        if(!camTransform)
        {
            return;
        }
        Vector3 camPos = camTransform.localPosition;
        Vector3 newPos = Vector3.Lerp(camPos, cameraPos, Time.deltaTime * lerpSpeed);
        camTransform.position = newPos;

    }

    void CheckMeshRenderer() //to make mesh invisible upon excessive zoom
    {
        if(!camTransform)
        {
            return;
        }
    }
}
