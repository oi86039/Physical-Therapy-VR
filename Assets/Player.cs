using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject model; //Model to move and rotate
    public float rotateSpeed;

    //Uses laser pointer script, credit to @Moaid_T4

    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.SetWidth(laserWidth, laserWidth);
    }

    // Update is called once per frame
    void Update()
    {
        //Handle Joystick input
        Vector2 move = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick) + OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        move = new Vector2(Mathf.Clamp(move.x, -1.0f, 1.0f), Mathf.Clamp(move.y, -1.0f, 1.0f));
        model.transform.Rotate(model.transform.up * -move.x * rotateSpeed);

        //On point, raycast and have laser pointer
        if (!OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))
        {
            ShootLaserFromTargetPosition(transform.position, Vector3.forward, laserMaxLength);
            laserLineRenderer.enabled = true;
        }
        else
        {
            laserLineRenderer.enabled = false;
        }
    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }
}
