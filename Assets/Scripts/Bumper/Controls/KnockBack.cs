using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackForce = 10000f;  
    public float knockbackDuration = 0.1f; 

    private Rigidbody rb; 
    private bool isKnockedBack = false; 

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (!isKnockedBack && collision.gameObject.CompareTag("Player"))
        {
            Vector3 knockbackDirection = transform.position - collision.transform.position;
            ApplyKnockback(knockbackDirection);
            StartCoroutine(ResetKnockback());
        }
    }

    void ApplyKnockback(Vector3 direction)
    {
        
        Vector3 knockbackDirection = direction.normalized;

        
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    
    private System.Collections.IEnumerator ResetKnockback()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
}
