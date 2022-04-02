using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Player setting for speed and jump 
    public float velocity = 0;
    public float jumpHeight = 10.0f;

    // Count display
    public TextMeshProUGUI countText;
    // Victory sign
    public GameObject winTextObject;


    // Player
    private Rigidbody rb;
    // Player coordinate
    private float movementX;
    private float movementY;
    // Count
    private int count;
    // Jump variable
    private bool canJump;
    private bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        // Init count display
        SetCountText(); 
        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }

    // Movement of the player
    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector =  movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            canJump = true;
            doubleJump = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            canJump = false;
            doubleJump = true;
        }
    }

    // Jump of the player
    private void OnJump(InputValue jumpValue)
    {
        Debug.Log("Jump Pressed");
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

		if (count >= 12) 
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
		}
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (canJump)
            {
                rb.velocity = Vector3.up * jumpHeight;
                //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
            if (doubleJump)
            {
                rb.velocity = Vector3.up * jumpHeight;
                //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                doubleJump = false;
            }
        } 
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * velocity);
    }

    // Trigger when player devour pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            // Increment count
            count = count + 1;
            // Update count display
            SetCountText();
        }
    } 
}
