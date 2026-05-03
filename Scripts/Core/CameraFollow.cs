using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float yOffset = 1.0f;

    private float velocityX;

    private void LateUpdate()
    {
        if (target == null) return;

        float targetX = target.position.x;
        float smoothedX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocityX, smoothTime);

        transform.position = new Vector3(smoothedX, yOffset, transform.position.z);
    }
}
