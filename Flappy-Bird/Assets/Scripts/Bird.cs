using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed =50;
    private Rigidbody2D birdRB;
    // Start is called before the first frame update
    void Start()
    {
        birdRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Jump();
    }


    // Control the bird jup with rigid body phisyics
    // take input with mouse and keyboard
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            birdRB.velocity = Vector2.up * speed;
        }
    }
}
