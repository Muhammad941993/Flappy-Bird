using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float PIPE_WIDTH = 7.8f;
    private const float CAMERA_ORTHO_SIZE = 50f;
    private const float PIPE_HEAD_HAFE_HIGHT = 1.87f;
    private const float PIPE_DESTROY_X_POS = -110f;
    private const float PIPE_SPAWN_X_POS = 110f;
    private const float PLAYER_X_POS = 0;

    private List<Pipe> pipes;
    private float pipeSpawnTimer;
    private float pipeSpawnTimerMax;
    private float gapSize;
    private int pipeSpawned;
    public int PlayerScore = 0;
    private Assets.PlayerState playerState;
   

    private enum Difficulty { Easy , Medium , Hard , Expert}

    // Start is called before the first frame update
    void Start()
    {
        Assets.GetInstance().Bird.OnDiead += PlayerDead;
        Assets.GetInstance().Bird.OnStart += StartPlaying;
        playerState = Assets.PlayerState.WaitingToStart;
        pipes = new List<Pipe>();

        pipeSpawnTimerMax = 2f;
      
        SetDifficulty(Difficulty.Easy);

        InvokeRepeating("DestroySounds", 5f, 10f);
    }

    private void StartPlaying(object sender, System.EventArgs e)
    {
        playerState = Assets.PlayerState.Playing;
    }

    private void PlayerDead(object sender, System.EventArgs e)
    {
        playerState = Assets.PlayerState.Died;
    }

    private void Update()
    {
        if(playerState == Assets.PlayerState.Playing)
        {
            PipeTimerSpawning();
            MovePipes();
        }
       
    }
    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                gapSize = 50;
                break;
            case Difficulty.Medium:
                gapSize = 40;
                break;
            case Difficulty.Hard:
                gapSize = 30;
                break;
            case Difficulty.Expert:
                gapSize = 20f;
                break;
        }
    }

    private Difficulty GetDifficulty()
    {
        if(pipeSpawned >= 60) { return Difficulty.Expert; }
        if (pipeSpawned >= 40) { return Difficulty.Hard; }
        if (pipeSpawned >= 20) { return Difficulty.Medium; }
        return Difficulty.Easy;
    }

    // timer for Spawn Pipe every pipeSpawnTimerMax second
    void PipeTimerSpawning()
    {
        pipeSpawnTimer -= Time.deltaTime;
        if(pipeSpawnTimer < 0)
        {
            pipeSpawnTimer = pipeSpawnTimerMax;
            RandoumizeCreate();
        }
    }

    // Hande Randoum size of y pos and gap size

    private void RandoumizeCreate()
    {
        float hightLimit = 10f;
        float minHight = gapSize * 0.5f + hightLimit;
        float totaScreenHight = CAMERA_ORTHO_SIZE * 2;
        float maxHight = totaScreenHight - gapSize * 0.5f - hightLimit;

        float yPos = Random.Range(minHight, maxHight);
        CreatePipeWithGap(yPos, gapSize, PIPE_SPAWN_X_POS);
        pipeSpawned++;
        SetDifficulty(GetDifficulty());

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

        Pipe newPipe = new Pipe(head, body , OnGrond);
       
        pipes.Add(newPipe);
       
    }


    // Moving all The Pipes In level Scene
    private void MovePipes()
    {
        for(int i=0; i<pipes.Count; i++)
        {
            Pipe newPipe = pipes[i];
            bool pipeOnRight = newPipe.GetXPosition() > PLAYER_X_POS;
            newPipe.MovePipe();
            if(pipeOnRight && newPipe.GetXPosition() <= PLAYER_X_POS)
            {
                if (newPipe.OnGround())
                {
                    PlayerScore++;
                    Assets.GetInstance().score.UpdateScoreText();
                    SoundManager.PlaySound(SoundManager.Sound.Score);
                }
               
            }
            if (newPipe.GetXPosition() < PIPE_DESTROY_X_POS)
            {
                newPipe.SelfDestroy();
                pipes.Remove(newPipe);
                i--;
            }
        }
       
    }

   void DestroySounds()
    {
        GameObject[] soundarr = GameObject.FindGameObjectsWithTag("Sound");
       foreach(GameObject i in soundarr)
        {
            Destroy(i);
        }

    }
   
    // pipe class that have pipe data and functions
    private class Pipe 
    {
        private float pipeSpeed = 20;
        private Transform pipeHead;
        private Transform pipeBody;
        private bool onGround;
      
        public Pipe(Transform head, Transform body , bool OnGround)
        {
            pipeHead = head;
            pipeBody = body;
            onGround = OnGround;
        }

        public void MovePipe()
        {
            pipeHead.position += Vector3.left * pipeSpeed * Time.deltaTime;
            pipeBody.position += Vector3.left * pipeSpeed * Time.deltaTime;
        }

        public float GetXPosition()
        {
            return pipeHead.position.x;
        }

        public void SelfDestroy()
        {
            Destroy(pipeBody.gameObject);
            Destroy(pipeHead.gameObject);
        }


        public bool OnGround()
        {
            return onGround;
        }

    }
}
