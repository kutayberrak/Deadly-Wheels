using System.Collections;
using UnityEngine;


public class ZombieBehaviour : MonoBehaviour
{
    public Animator animator;
    private bool hit = false; 
    public int deathCounter = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit && (collision.gameObject.CompareTag("Vehicle") || collision.gameObject.CompareTag("Police")))
        {
            hit = true;
            animator.SetTrigger("isHit");
            deathCounter += 1;
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    public IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}