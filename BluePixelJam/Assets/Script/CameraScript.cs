using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    //Script que faz com que a câmera acompanhe o movimento do player no eixo Y
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    public Transform target;
    [SerializeField]
    private float rightBound, leftBound, topBound, bottomBound;

    public void setTarget(Transform player) {
        target = player;
    }

    public void setBounds(Bounds mapBounds, Transform geraTile) {
        GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>().forceStart();
        float vertExtent = Camera.main.camera.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        Vector3 limitCenter = new Vector3(mapBounds.extents.x, mapBounds.extents.y);
        Vector3 point = camera.WorldToViewportPoint(geraTile.position+limitCenter);
        Vector3 delta = geraTile.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        mapBounds.center = point + delta;
        leftBound = (float)(horzExtent - mapBounds.size.x / 2.0f);
        rightBound = (float)(mapBounds.size.x / 2.0f - horzExtent);
        bottomBound = (float)(vertExtent - mapBounds.size.y / 2.0f);
        topBound = (float)(mapBounds.size.y / 2.0f - vertExtent);
    }
    
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position;// +delta;
            destination.x = Mathf.Clamp(destination.x, leftBound, bottomBound);
            destination.y = Mathf.Clamp(destination.y, rightBound, topBound);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}