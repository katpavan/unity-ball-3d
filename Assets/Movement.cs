using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private float horizontalTimer;
    private float maxHorizontalTimer = 0.2f;
    private float verticalInput; 

	//Mobile Variables
	private Vector2 startPosition;
	private Vector2 deltaSwipe; //cover the difference between the start position and the ending position of the finger

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.forward * 20; //Vector3.forward is the same as 0, 0, 1
    }

    // Update is called once per frame
    void Update()
    {	
		horizontalInput = verticalInput = 0; //need to reset every frame
		UpdateMobile();
    }

	private void UpdateMobile(){
		// https://docs.unity3d.com/ScriptReference/TouchPhase.html
		// Began	A finger touched the screen.
		// Moved	A finger moved on the screen.
		// Stationary	A finger is touching the screen but hasn't moved.
		// Ended	A finger was lifted from the screen. This is the final phase of a touch.
		// Canceled	The system cancelled tracking for the touch.
		//at least one finger on the screen
		if (Input.touches.Length > 0){
			//only use first touch of input
			Touch touch = Input.GetTouch(0);
			//A finger touched the screen.
			if(touch.phase == TouchPhase.Began){
				startPosition = touch.position; //grab the position when the player touches the screen
			}else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
				startPosition = Vector2.zero; //reset it after the swipe is done
			}else if (touch.phase == TouchPhase.Moved){
				deltaSwipe = Vector2.zero; //reset the deltaSwipe
				if (startPosition != Vector2.zero) deltaSwipe = touch.position - startPosition; //get how long the swipe currently is
				//get the length of the vector with sqrMagnitude. It's faster to do it this way based on unity docs
				if (deltaSwipe.sqrMagnitude > 5000)
				{
					//get direction of swipe
					float x = deltaSwipe.x;
					float y = deltaSwipe.y;
					
					// if x is larger then horizontal movement 
					if (Mathf.Abs(x) >= Mathf.Abs(y)) //we prefer the x axis when they're equal
					{
						if (x > 0){ // right
							horizontalInput = 1;
						}else //left
						{
							horizontalInput = -1;
						}
					}else {// if y is larger then vertical movement
						if (y > 0){ // up
							verticalInput = 1;
						}else //down
						{
							verticalInput = -1;
						}
					}
					startPosition = deltaSwipe = Vector2.zero;
				}
			}
		}	
		HorizontalMovement();
        VerticalMovement();
	}

    private void UpdatePC(){
        horizontalInput = ((Input.GetKeyDown("d") ? 1 : 0) + (Input.GetKeyDown("a") ? -1 : 0));
        verticalInput = ((Input.GetKeyDown("w") ? 1 : 0) + (Input.GetKeyDown("s") ? -1 : 0));

        HorizontalMovement();
        VerticalMovement();
    }

   private void HorizontalMovement(){
        horizontalTimer -= Time.deltaTime;

        if (horizontalTimer < 0){
            rb.velocity = new Vector3(horizontalInput * 24, rb.velocity.y, rb.velocity.z);
            if (horizontalInput != 0) horizontalTimer = 0.2f;
        }

   }

   private void VerticalMovement(){
        if (verticalInput != 0) rb.velocity = new Vector3(rb.velocity.x, verticalInput * 10, rb.velocity.z);
   }
}
