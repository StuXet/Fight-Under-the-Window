using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothFactor;
    public Vector2 minValues;
    public Vector2 maxValue;


    private void LateUpdate()
    {
        SmoothFollow();
    }
    
    void SmoothFollow()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(target.position.x, minValues.x, maxValue.x);
            targetPosition.y = Mathf.Clamp(target.position.y, minValues.y, maxValue.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor);
        }
    }
}
