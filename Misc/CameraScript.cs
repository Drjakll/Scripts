using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Intend to attach to the main camera
/// </summary>
public class CameraScript : MonoBehaviour {
    private float RadiusHorz;
    private float Radius;
    private float angleRespectToParentHorz;
    private float angleRespectToParentVert;
    public float RotateSpeed;
    public float zoomSpeed;
    public GameObject focusPoint;

	void Start () {
        
        RotateSpeed = 12.50f;
        zoomSpeed = 600.0f;
        RadiusHorz = (new Vector2(transform.position.x - focusPoint.transform.position.x, transform.position.z - focusPoint.transform.position.z)).magnitude;

        angleRespectToParentHorz = Mathf.Acos((transform.position.x - focusPoint.transform.position.x) / RadiusHorz);
        if(transform.position.z - focusPoint.transform.position.z < 0)
        {
            angleRespectToParentHorz = 2 * Mathf.PI - angleRespectToParentHorz;
        }

        Radius = (new Vector2(transform.position.y - focusPoint.transform.position.y, RadiusHorz)).magnitude;

        angleRespectToParentVert = Mathf.Acos(RadiusHorz / Radius);
        if(transform.position.y - focusPoint.transform.position.y < 0)
        {
            angleRespectToParentVert = 2 * Mathf.PI - angleRespectToParentVert;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 movementDelta;
        movementDelta = Vector3.zero;

        float zoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed;
        if(zoom != 0)
        {
            transform.Translate(0, 0, zoom, Space.Self);
            RadiusHorz = (new Vector2(transform.position.x - focusPoint.transform.position.x, transform.position.z - focusPoint.transform.position.z)).magnitude;
            Radius = (transform.position - focusPoint.transform.position).magnitude;
            if(Radius > 40.0f || Radius < 5.0f)
            {
                transform.Translate(0, 0, -zoom, Space.Self);
            }
            transform.LookAt(focusPoint.transform);
        }

        if (Input.GetMouseButton(2))
        {
            float HorzRotation = Input.GetAxis("Mouse X") * Time.deltaTime * RotateSpeed;
            float VertRotation = Input.GetAxis("Mouse Y") * Time.deltaTime * RotateSpeed;
            CircularMotionX(-HorzRotation);
            CircularMotionY(-VertRotation);
        }
    }

    void CircularMotionX(float Displacement)
    {
        angleRespectToParentHorz = (angleRespectToParentHorz + Displacement) % (2.0f * Mathf.PI);
        float newX = Mathf.Cos(angleRespectToParentHorz) * RadiusHorz;
        float newZ = Mathf.Sin(angleRespectToParentHorz) * RadiusHorz;
        Vector3 difference = (transform.position - focusPoint.transform.position);
        float deltaX = newX - difference.x;
        float deltaZ = newZ - difference.z;
        transform.Translate(deltaX, 0, deltaZ, Space.World);
        transform.LookAt(focusPoint.transform);
    }

    void CircularMotionY(float Displacement)
    {
        angleRespectToParentVert = (angleRespectToParentVert + Displacement);
        if(angleRespectToParentVert < 0.0f || angleRespectToParentVert > Mathf.PI / 2.0f)
        {
            angleRespectToParentVert -= Displacement;
        }
        float newY = Mathf.Sin(angleRespectToParentVert) * Radius;

        RadiusHorz = Mathf.Cos(angleRespectToParentVert) * Radius;

        float newX = Mathf.Cos(angleRespectToParentHorz) * RadiusHorz;
        float newZ = Mathf.Sin(angleRespectToParentHorz) * RadiusHorz;
        Vector3 difference = (transform.position - focusPoint.transform.position);
        float deltaY = newY - difference.y;
        float deltaX = newX - difference.x;
        float deltaZ = newZ - difference.z;
        transform.Translate(deltaX, deltaY, deltaZ, Space.World);
        transform.LookAt(focusPoint.transform);
    }
}
