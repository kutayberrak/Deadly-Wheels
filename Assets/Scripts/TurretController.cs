using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Animator turretAnimation;
    public int maxAmmo = 5;
    public int currentAmmo;
    public AudioSource turretAudio;

    private GameObject currentTarget;
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && currentAmmo > 0)
        {
            currentTarget = other.gameObject;
            turretAnimation.SetTrigger("FireBullet");
        }
    }
    void FireBullet()
    {
        if (currentTarget != null)
        {
            ZombieBehaviour zombieBehaviour = currentTarget.GetComponent<ZombieBehaviour>();
            Animator targetAnimator = currentTarget.GetComponent<Animator>();

            targetAnimator.SetTrigger("isHit");
            turretAudio.Play();
            StartCoroutine(zombieBehaviour.DestroyAfterAnimation());

            currentAmmo--;
        }
    }
}