using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 
    [SerializeField] private Transform target1; 
    [SerializeField] private Transform target2; 
    [SerializeField] private float smoothTime = 0.3f; 

    private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _offset;

    private void Awake()
    {
       if (target1 == null || target2 == null)
        {
            Debug.LogError("Target1 and Target2 must be assigned.");
            enabled = false; 
            return;
        }
        UpdateOffset();
    }

    private void LateUpdate()
    {
        if (target1 == null || target2 == null)
        return;

        Vector3 midpoint = (target1.position + target2.position) / 2;

        Vector3 targetPosition = midpoint + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        transform.LookAt(midpoint);
    }
     private void UpdateOffset()
    {
        Vector3 midpoint = (target1.position + target2.position) / 2;
        _offset = transform.position - midpoint;
    }
}
