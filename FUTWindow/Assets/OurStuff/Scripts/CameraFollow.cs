using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector2 minValues;
    public Vector2 maxValue;


    private void LateUpdate()
    {
<<<<<<< Updated upstream
        Vector3 newPos = new Vector3(target.position.x, target.position.y - yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed*Time.deltaTime);
        
=======
        SmoothFollow();
    }
    ////void Follow()
    //{

    //    Vector3 targetPosition = target.position + offset;
    //    Vector3 boundPosition = new Vector3(
    //        Mathf.Clamp(targetPosition.x, minValues.x, maxValue.x),
    //        Mathf.Clamp(targetPosition.y, minValues.y, maxValue.y),
    //        Mathf.Clamp(targetPosition.z, minValues.z, maxValue.z));

    //    Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, 2 * Time.fixedDeltaTime);
    //    transform.position = smoothPosition;
    //}
    void SmoothFollow()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(target.position.x, minValues.x, maxValue.x);
            targetPosition.y = Mathf.Clamp(target.position.y, minValues.y, maxValue.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor);
        }
>>>>>>> Stashed changes
    }
}
