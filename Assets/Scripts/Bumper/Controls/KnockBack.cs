using UnityEngine;
using System.Collections;
public class Knockback : MonoBehaviour
{
    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10f;
    public float constForce = 5f;
    public float inputForce = 7.5f;

    private Rigidbody rb;
    private Coroutine KnockbackCoroutine;
    public bool IsBeingKnockedBack{ get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public IEnumerator KnockbackAction(Vector3 hitDirection, Vector3 constantForceDirection, float inputDirection)
    {
        IsBeingKnockedBack = true;
        Vector3 _hitForce;
        Vector3 _constantForce;
        Vector3 _knockbackForce;
        Vector3 _combinedForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapsedTme = 0f;
        while (_elapsedTme < knockbackTime)
        {
            _elapsedTme += Time.fixedDeltaTime;
            _knockbackForce = _hitForce + _constantForce;

            if (inputDirection != 0)
            {
                _combinedForce = _knockbackForce + new Vector3(inputDirection, 0f);
            }
            else
            {
                _combinedForce = _knockbackForce;
            }
            rb.velocity = _combinedForce;
            yield return new WaitForFixedUpdate();
        }
        IsBeingKnockedBack = false;
    }
    public void CallKnockBack(Vector3 hitDirection, Vector3 constantForceDirection, float inputDirection)
    {
        KnockbackCoroutine = StartCoroutine(KnockbackAction(hitDirection, constantForceDirection, inputDirection));

    }
   

}
