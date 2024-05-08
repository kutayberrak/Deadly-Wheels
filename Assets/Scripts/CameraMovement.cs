using System.Collections;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 1.0f;
    public Vector3 offset = new Vector3(-70, 15, -70);
    public float smoothTime = 0.02f;
    private Vector3 velocity = Vector3.zero;

    private bool isMarketCam = true;

    void Update()
    {
        if (isMarketCam)
        {
            MarketCamView();
        }
        else
        {
            GameCamView();
        }
    }

    void MarketCamView()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    /*void GameCamView()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(target.position);
    }*/

    void GameCamView()
    {
        //Vector3 targetPosition = target.position + offset;
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }

    public void ToggleCameraMode()
    {
        StartCoroutine(DelayedToggle());
    }

    private IEnumerator DelayedToggle()
    {
        yield return new WaitForSeconds(3.5f);
        isMarketCam = !isMarketCam;
    }
}
