using UnityEngine;
using System.Collections.Generic;


public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<Gravity> GravityObjectList;
    // orbit
    [SerializeField] bool planets = false;
    [SerializeField] int orbitSpeed = 1000; 
        private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(GravityObjectList == null)
        {
            GravityObjectList = new List<Gravity>(); 
        }
        GravityObjectList.Add(this);

        if (!planets)
        {
            rb.AddForce(Vector3.left * orbitSpeed); 
        }

    }
    private void FixedUpdate()
    {
        foreach (var obj in GravityObjectList)
        {
            if (obj != this)
            {
                Attract(obj);
            }
            
        }
    }

    void Attract (Gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;
        float distrance = direction.magnitude;
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distrance, 2));
        Vector3 finalForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(finalForce);
    }
}
