using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bird : MonoBehaviour
{
    public event EventHandler OnDiead;
    public event EventHandler OnStart;
    bool moving = false;
    public float speed =50;
    private Rigidbody2D birdRB;
    private Assets.PlayerState playerState;
    // Start is called before the first frame update
    void Start()
    {
        birdRB = GetComponent<Rigidbody2D>();
        birdRB.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame


    void Update()
    {
       

        switch (playerState)
        {
            case Assets.PlayerState.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    moving = true;
                    birdRB.bodyType = RigidbodyType2D.Dynamic;
                    playerState = Assets.PlayerState.Playing;
                    Jump();
                    if (OnStart != null) OnStart(this, EventArgs.Empty);

                }
                break;
            case Assets.PlayerState.Playing:
                Jump();
                break;

        }
    }


    // Control the bird jup with rigid body phisyics
    // take input with mouse and keyboard
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && moving)
        {
            birdRB.velocity = Vector2.up * speed;
            SoundManager.PlaySound(SoundManager.Sound.Jumb);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        birdRB.bodyType = RigidbodyType2D.Static;
       if(OnDiead != null) OnDiead(this,EventArgs.Empty);
        SoundManager.PlaySound(SoundManager.Sound.Lose);
        moving = false;
    }
}
