using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    //Script que faz com que a câmera acompanhe o movimento do player no eixo Y
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    public Transform target;

    public void setTarget(Transform player) {
        target = player;
    }

    public void setBounds(Bounds mapBounds) {
        transform.position = new Vector3(mapBounds.extents.x, mapBounds.extents.y, transform.position.z);
    }
    
    void FixedUpdate()
    {
        if (target && Time.timeScale>0)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.4599f, 0.4599f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}