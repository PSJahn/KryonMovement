/*
"KryonMovement": Rigidbody-Based Player Movement Script
Copyright 2022 PSJahn
Not Optimized for First Person Games.
*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float movementSpeed = 1500f;
    [SerializeField]
    private float decreaseSpeed = 15f;
    [SerializeField]
    //This is the direction (x,z) that pressing W should move you towards.
    private Vector2 forwardDirection = new Vector2(0,1);
    [SerializeField]
    private float maxSpeed = 5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector2 sideMovement = new Vector2(0, 0);
        if(Input.GetKey(KeyCode.W))
        {
            sideMovement += forwardDirection;
        }
        if (Input.GetKey(KeyCode.D))
        {
            sideMovement += new Vector2(forwardDirection.y, -forwardDirection.x);
        }
        if (Input.GetKey(KeyCode.S))
        {
            sideMovement -= forwardDirection;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sideMovement -= new Vector2(forwardDirection.y, -forwardDirection.x);
        }
        //Normalize The Vector to get Equally fast side and diagonal movement, multiply with speed and deltaTime
        sideMovement = sideMovement.normalized * movementSpeed * Time.deltaTime;
        rb.AddForce(new Vector3(sideMovement.x, 0, sideMovement.y));
        //The Decreased Velocity
        Vector3 decreaseVelocity = Vector3.MoveTowards(rb.velocity, new Vector3(0, 0, 0), decreaseSpeed * Time.deltaTime);
        //Decrease and Clamp the Velocity
        rb.velocity = new Vector3(Mathf.Clamp(decreaseVelocity.x, -maxSpeed, maxSpeed), rb.velocity.y, Mathf.Clamp(decreaseVelocity.z, -maxSpeed, maxSpeed));
    }
}
