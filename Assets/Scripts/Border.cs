using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log($"we hit {other.gameObject.name}");

        //get ref to paddle collider 
        BoxCollider bc = GetComponent<BoxCollider>();
        Bounds bounds = bc.bounds;
        float maxX = bounds.max.x; //max
        float minX = bounds.min.x; //min
        
        
        //value is distance between maxZ-minZ
        //ratio = (value - min)/(max-min); 
        //translated value = ratio * (targetMax - targetMin) + targetMax
        
        float bouncePos = other.transform.position.x - maxX/(maxX-minX) ; 
        
        float ratio = (bouncePos+2)/2;

        
        
        
        
        //Debug.Log($"maxZ = {maxZ}, minZ = {minZ}");
        //Debug.Log($"bounce pos = {bouncePos}");
        //Debug.Log($"bounce pos = {bouncePos}");
        //Debug.Log($"ratio = {ratio}");
        //Debug.Log($"interp = {interpolatedValue}");
        
        if (other.gameObject.CompareTag("Ball"))
        {
            
                float interpolatedValue = ratio * -60;
        
                Quaternion rotation = Quaternion.Euler(0f,interpolatedValue ,0f);
                Vector3 bounchDirection = rotation * Vector3.left;
                Debug.Log("direction:"+ bounchDirection);
                Rigidbody rb = other.rigidbody; 
                //forceToAdd += 100; 
                rb.AddForce(bounchDirection * 500f,ForceMode.Force); 
                //Debug.Log($"force = {forceToAdd}");
                //PlaySound();
                
            
        }
    }
    
    
    
    
    
}
