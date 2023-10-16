using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private Vector3 spawnPos;

    private float movementX;
    private float movementY;
    [SerializeField] float jumpForce = 0;
    [SerializeField] float bounceForce = 0;
    public float speed = 0;
    public float maxSpeed = 10;
    public float threshold;
    
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    //[SerializeField] GameObject bullet;

    // onGround is for checking if the Player is on the Ground
    bool onGround;
    bool canBounce;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent <Rigidbody>() ;
        count = 0;
        spawnPos = transform.position;
        onGround = false;
        canBounce = false;

        SetCountText();
        winTextObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        
        // Check if velocity goes over maxSpeed
        // If yes, clamp/restrict velocity to the set maxSpeed
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        // Check if Player is below threshold (y position)
        // If yes, respawn Player at the position it originally spawned in
        if (transform.position.y < threshold)
        {
            transform.position = spawnPos;
        }

        // Check is Player is on a BouncePad
        // If yes, Player will bounce/automatically jump while on the BouncePad
        if (canBounce)
        {
            rb.AddForce(new Vector3(0, bounceForce, 0), ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Player can jump if on Ground object
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        // Player can bounce if on BouncePad object
        if (collision.gameObject.CompareTag("BouncePad"))
        {
            canBounce = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onGround = false;
        canBounce = false;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        // Only jump if Player is on the Ground
        // Prevents infinite jumping in the air
        if (onGround)
        {
            Debug.Log("Jumped");
            Debug.Log(rb.velocity);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Acceleration);
        }
    }

    /*
    void OnFire()
    {
        Debug.Log("Firing");
        Instantiate(bullet, transform);
    }
    */

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }
}
