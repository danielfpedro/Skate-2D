using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector2 offset;
    public float zCamera = -2f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + (Vector3)offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, zCamera);
    }

}
