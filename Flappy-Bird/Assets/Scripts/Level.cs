using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float PIPE_WIDTH = 7.8f;
    private const float CAMERA_ORTHO_SIZE = 50f;
    private const float PIPE_HEAD_HAFE_HIGHT = 1.87f;

    private List<Pipe> pipes;
    // Start is called before the first frame update
    void Start()
    {
        pipes = new List<Pipe>();
       
        CreatePipeWithGap(30, 10, -10);
        CreatePipeWithGap(50, 30, 20);
        CreatePipeWithGap(70, 50, 60);

    }

    private void Update()
    {
        MovePipes();
    }



    /*
     Create Two Pipe on top and Grond 
    With Specific y Position For Gap And Gap Size

    */
    void CreatePipeWithGap(float GapPosY , float GapSize , float GapPosX)
    {
        CreatePipe(GapPosY - GapSize * 0.5f, GapPosX, true);
        CreatePipe(2f * CAMERA_ORTHO_SIZE - GapPosY - GapSize * 0.5f, GapPosX, false);
    }

    /*
     Create Pipe heade And body
    and Decide if pipe on ground or top
     **/
    void CreatePipe(float yhight , float xPosition , bool OnGrond)
    {
        // create the head and locate it position

        Transform head = Instantiate(Assets.GetInstance().PfPipeHead);
        float headYPos;
        if (OnGrond)
        {
            headYPos = -CAMERA_ORTHO_SIZE + yhight - PIPE_HEAD_HAFE_HIGHT;
        }
        else
        {
            headYPos = CAMERA_ORTHO_SIZE - yhight + PIPE_HEAD_HAFE_HIGHT;
        }
        head.position = new Vector3(xPosition,headYPos);


        // create the body and locate it position and box collider2d

        Transform body = Instantiate(Assets.GetInstance().PfPipeBody);
        float bodyYPos;
        if (OnGrond)
        {
            bodyYPos = -CAMERA_ORTHO_SIZE;
        }
        else
        {
            bodyYPos = CAMERA_ORTHO_SIZE;
            body.localScale = new Vector3(1, -1, 1);
        }

        body.position = new Vector3(xPosition, bodyYPos);
        SpriteRenderer bodySprite = body.GetComponent<SpriteRenderer>();
        bodySprite.size = new Vector2(PIPE_WIDTH, yhight);
        BoxCollider2D bodyCollider = body.GetComponent<BoxCollider2D>();
        bodyCollider.size = new Vector2(PIPE_WIDTH, yhight);
        bodyCollider.offset = new Vector2(0, yhight * .5f);

        Pipe newPipe = new Pipe(head, body);
        pipes.Add(newPipe);
    }


    // Moving all The Pipes In level Scene
    private void MovePipes()
    {
        foreach(Pipe i in pipes)
        {
            i.MovePipe();
        }
    }
}
