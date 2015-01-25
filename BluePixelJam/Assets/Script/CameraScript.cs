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
    private Bounds bounds;
    [SerializeField]
    private float leftBound, rightBound, bottomBound, topBound;
    [SerializeField]
    private float minX, minY, maxX, maxY;

    public void setTarget(Transform player) {
        target = player;
    }

    public void setBounds(Bounds mapBounds, float tileSize) {
        bounds = mapBounds;
        transform.position = new Vector3(mapBounds.extents.x, mapBounds.extents.y, transform.position.z);
        float vertExtent = Camera.main.camera.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        minX = bounds.min.x; minY = bounds.min.y; maxX = bounds.max.x; maxY = bounds.max.y;
        leftBound = (float)(horzExtent - (bounds.min.x))+tileSize/2;
        rightBound = (float)((bounds.max.x) - horzExtent) - tileSize / 2;
        bottomBound = (float)(vertExtent - (bounds.min.y)) + tileSize / 2;
        topBound = (float)((bounds.max.y) - vertExtent)-tileSize /2;
    }
    
    void FixedUpdate()
    {
        if (target && Time.timeScale>0)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.4599f, 0.4599f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            destination.x = Mathf.Clamp(destination.x, leftBound, rightBound);
            destination.y = Mathf.Clamp(destination.y, bottomBound, topBound);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}