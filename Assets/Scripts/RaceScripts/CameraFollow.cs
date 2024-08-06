using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 
    [SerializeField] private Transform target; 
    [SerializeField] private float smoothTime = 0.3f; 

    private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _offset;

    private void Awake()
    {
       _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}
