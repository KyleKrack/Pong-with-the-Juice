using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using TMPro;

public class Paddle : MonoBehaviour
{
    
    
    
    public float collisionBallSpeedUp = 1.5f;
    public float unitsPerSecond = 25;
    public float forceToAdd = 400; 
    public AudioSource src;
    public AudioClip sfx1;

    public TextMeshProUGUI scoreText;
    public AudioClip cheering;

    public void AddScorePTwo()
    {
        GlobalVariables.PlayerTwoScore++;
        //scoreText.color = Color.red;
        scoreText.SetText($" {GlobalVariables.PlayerOneScore}  {GlobalVariables.PlayerTwoScore}");
        
        
    }

    public void AddScorePOne()
    {
        GlobalVariables.PlayerOneScore++;
       // scoreText.color = Color.red;
        scoreText.SetText($" {GlobalVariables.PlayerOneScore}  {GlobalVariables.PlayerTwoScore}"); 
       
    }
    
    public void PlaySound()
    {
        src.clip = sfx1;
        src.Play(); 
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        forceToAdd = 400;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //scoreText.color = new Color(255,255,255);
        scoreText.SetText($" {GlobalVariables.PlayerOneScore}  {GlobalVariables.PlayerTwoScore}"); 
        if (this.gameObject.CompareTag("Left"))
        {
            float verticalValue = Input.GetAxis("playerOneButtons");


            Vector3 force = Vector3.forward * (verticalValue * unitsPerSecond);  //(unitsPerSecond * Time.deltaTime)) * 2; 
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(force, ForceMode.Force);
        }

        if (this.gameObject.CompareTag("Right"))
        {
            float verticalValue = Input.GetAxis("playerTwoButtons");


            Vector3 force = Vector3.forward * (verticalValue * unitsPerSecond);  //(unitsPerSecond * Time.deltaTime)) * 2; 
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(force, ForceMode.Force);
            Ball.direction = force.normalized; 
            //Debug.Log("force:"+ force);
            //Debug.Log("force normal:"+ force.normalized);
            //Debug.Log("direction:"+ Ball.direction);
        }
        
        
        
        
    }

    
    
 

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"we hit {other.gameObject.name}");

        //get ref to paddle collider 
        BoxCollider bc = GetComponent<BoxCollider>();
        Bounds bounds = bc.bounds;
        float maxZ = bounds.max.z; //max
        float minZ = bounds.min.z; //min
        
        
        //value is distance between maxZ-minZ
        //ratio = (value - min)/(max-min); 
        //translated value = ratio * (targetMax - targetMin) + targetMax
        
        float bouncePos = other.transform.position.z - maxZ ; 
        
        float ratio = (bouncePos+2)/2;

            //Vector3 currentVelocity = other.relativeVelocity;
        //float newSpeed = currentVelocity.magnitude * collisionBallSpeedUp;
        //Vector3 currentVelocity = other.relativeVelocity;
        //float newSign = -Math.Sign(currentVelocity.x);
        //float newRotSign = -newSign;;

        // Change the velocity between -60 to 60 degrees based on where it hit the paddle
        //float newSpeed = currentVelocity.magnitude * collisionBallSpeedUp;
        
        
        
        
        
        
        //Debug.Log($"maxZ = {maxZ}, minZ = {minZ}");
        //Debug.Log($"bounce pos = {bouncePos}");
        //Debug.Log($"bounce pos = {bouncePos}");
        //Debug.Log($"ratio = {ratio}");
        //Debug.Log($"interp = {interpolatedValue}");
        
        if (other.gameObject.CompareTag("Ball"))
        {
            if (this.gameObject.CompareTag("Right"))
            {
                var paddleBounds = GetComponent<BoxCollider>().bounds;
                float maxPaddleHeight = paddleBounds.max.z;
                float minPaddleHeight = paddleBounds.min.z;

                // Get the percentage height of where it hit the paddle (0 to 1) and then remap to -1 to 1 so we have symmetry
                float pctHeight = (other.transform.position.z - minPaddleHeight) / (maxPaddleHeight - minPaddleHeight);
                float bounceDirection = (pctHeight - 0.5f) / 0.5f;
                // Debug.Log($"pct {pctHeight} + bounceDir {bounceDirection}");

                // flip the velocity and rotation direction
                Vector3 currentVelocity = other.relativeVelocity;
                float newSign = -Math.Sign(currentVelocity.x);
                float newRotSign = -newSign;;

                // Change the velocity between -60 to 60 degrees based on where it hit the paddle
                float newSpeed = currentVelocity.magnitude * collisionBallSpeedUp;
                Vector3 newVelocity = new Vector3(newSign, 0f, 0f) * newSpeed;
                newVelocity = Quaternion.Euler(0f, newRotSign * 60f * bounceDirection, 0f) * newVelocity;
                other.rigidbody.velocity = newVelocity;

                PlaySound();
                
                
            }
            if (this.gameObject.CompareTag("Left"))
            {
                var paddleBounds = GetComponent<BoxCollider>().bounds;
                float maxPaddleHeight = paddleBounds.max.z;
                float minPaddleHeight = paddleBounds.min.z;

                // Get the percentage height of where it hit the paddle (0 to 1) and then remap to -1 to 1 so we have symmetry
                float pctHeight = (other.transform.position.z - minPaddleHeight) / (maxPaddleHeight - minPaddleHeight);
                float bounceDirection = (pctHeight - 0.5f) / 0.5f;
                // Debug.Log($"pct {pctHeight} + bounceDir {bounceDirection}");

                // flip the velocity and rotation direction
                Vector3 currentVelocity = other.relativeVelocity;
                float newSign = -Math.Sign(currentVelocity.x);
                float newRotSign = -newSign;;

                // Change the velocity between -60 to 60 degrees based on where it hit the paddle
                float newSpeed = currentVelocity.magnitude * collisionBallSpeedUp;
                Vector3 newVelocity = new Vector3(newSign, 0f, 0f) * newSpeed;
                newVelocity = Quaternion.Euler(0f, newRotSign * 60f * bounceDirection, 0f) * newVelocity;
                other.rigidbody.velocity = newVelocity;
                PlaySound(); 
            }
            
        }
    }
}
