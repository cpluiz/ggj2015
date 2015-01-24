﻿using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    //Script que faz com que a câmera acompanhe o movimento do player no eixo Y
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    public void setTarget(Transform player) {
        target = player;
    }

    
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            destination.x = transform.position.x;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}