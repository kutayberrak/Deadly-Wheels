using System.Collections;
using UnityEngine;


public class ZombieBehaviour : MonoBehaviour
{
    public Animator animator;
    private bool hit = false; 

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit && collision.gameObject.CompareTag("Vehicle"))
        {
            hit = true;
            animator.SetTrigger("isHit");

            StartCoroutine(DestroyAfterAnimation());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}