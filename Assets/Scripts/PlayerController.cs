using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    [SerializeField] float jumpForce;

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    [SerializeField] GameObject bullet;

    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent <Rigidbody>() ;
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        onGround = false;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        if (onGround)
        {
            Debug.Log("Jumped");
            Debug.Log(rb.velocity);
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }

    void OnFire()
    {
        Debug.Log("Firing");
        Instantiate(bullet, transform);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }
}
