using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Springy : MonoBehaviour
{
    [SerializeField] private Transform guide;
    [SerializeField] float springConstant = 2;
    [SerializeField] float restLength = 10;
    Renderer myRenderer;
    static readonly int Spring = Shader.PropertyToID("_Spring");
    

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3 springForce = guide.position - transform.position;
        float mag = springForce.magnitude;
        float k = Mathf.Min(restLength, mag - restLength);
        springForce.Normalize();
        springForce = springForce * (-1 * k * springConstant * Time.deltaTime);
        guide.position += springForce;
        myRenderer.sharedMaterial.SetVector(Spring, new Vector4(springForce.x, springForce.y, 0,0));
    }
}
