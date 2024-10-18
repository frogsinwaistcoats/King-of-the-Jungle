using UnityEngine;

public class AutoPushObjects : MonoBehaviour
{
    public float pushForce = 10f; 
    public float pushDistance = 1f;
    public LayerMask pushableLayer;
    public Vector2 moveInput;

    private Rigidbody rb;
    private Knockback knockback;
    PlayerInputBumper playerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        knockback = GetComponent<Knockback>();
        playerInput = GetComponent<PlayerInputBumper>();

    }
  

    void Update()
    {
        
        PushObjects();

        if (!knockback.IsBeingKnockedBack)
        {
            playerInput.MovePlayer();
        }
    }

    private void OnCollisionEnter(Collision other, Vector3 hitDirection)
    {
   
        knockback.CallKnockBack(hitDirection, Vector3.up, playerInput.Instance.moveInput.x);
    }
    void PushObjects()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushDistance, pushableLayer);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                
                Rigidbody objectRb = collider.GetComponent<Rigidbody>();
                Debug.Log(Vector3.Distance(transform.position, collider.transform.position));
                if (objectRb != null)
                {
                    
                    Vector3 pushDirection = (collider.transform.position - transform.position).normalized; // Calculate direction to push

                    objectRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                }
            }
        }
    }

}
