
using System.Numerics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public UnityEngine.Vector3 offset; 

    private void LateUpdate()
    {
        UnityEngine.Vector3 desiredPosition = target.position + offset;
        UnityEngine.Vector3 smoothedPosition = UnityEngine.Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }


}
