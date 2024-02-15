using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 100f;

    public AudioSource src;

    public AudioClip song;

    public static Vector3 direction; 
    
    public float desiredVelocity = 100f;
    // Start is called before the first frame update
    void Start()
    {
        src.clip = song;
        src.Play(); 
        
        Vector3 force = Vector3.left * speed; 
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Force);
        
    }

    

    
    
    // Update is called once per frame
    void Update()
    {
        
        Rigidbody ball = GetComponent<Rigidbody>();
        float currentVelocity = ball.velocity.magnitude;
        //ball.velocity = speed * ball.velocity.normalized; 
        //Debug.Log($"velocity: {currentVelocity}");
        
        
        
        //right side
        if (transform.position.x >= 15)
        {
            //Debug.Log("BALL RESET");
            
            transform.position = new Vector3(0, 4, 0);
            Vector3 force = Vector3.left * speed; 
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            
            rb.AddForce(force, ForceMode.Force);
            GlobalVariables.PlayerOneScore++; 
            //Paddle.scoreText.color = Color.red;
            
            //Debug.Log("P1: "+GlobalVariables.PlayerOneScore);

        }
        
        //left side
        if (transform.position.x <= -15)
        {
            //Debug.Log("BALL RESET");
            transform.position = new Vector3(0, 4, 0);
            
            Vector3 force = Vector3.right * speed; 
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            
            rb.AddForce(force, ForceMode.Force);
            GlobalVariables.PlayerTwoScore++; 
           //Paddle.scoreText.color = Color.red;
            
            //Debug.Log("P2: "+GlobalVariables.PlayerTwoScore);
        }

    }
}
