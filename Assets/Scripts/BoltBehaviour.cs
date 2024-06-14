using DG.Tweening;
using UnityEngine;

public class BoltBehaviour : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0,360,0), 1.5f,RotateMode.LocalAxisAdd).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
        transform.DOMoveY(transform.localPosition.y + 3,1).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Vehicle") || other.transform.parent.CompareTag("Police"))
        {
            DOTween.Kill(transform);
            other.GetComponentInParent<Collectible>().IncreaseBoltCount();
            Destroy(gameObject);
        }
    }
}
