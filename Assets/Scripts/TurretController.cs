using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Animator turretAnimation;
    public int maxAmmo = 5;
    private int currentAmmo;
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
            Destroy(currentTarget);
            currentAmmo--;
        }
    }
}