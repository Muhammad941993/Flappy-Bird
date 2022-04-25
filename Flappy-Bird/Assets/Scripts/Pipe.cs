using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// pipe class that have pipe data and functions
public class Pipe 
{
    private float pipeSpeed =10;
    private Transform pipeHead;
    private Transform pipeBody;
    public Pipe(Transform head , Transform body)
    {
        pipeHead = head;
        pipeBody = body;
    }
   
    public void MovePipe()
    {
        pipeHead.position += Vector3.left * pipeSpeed * Time.deltaTime;
        pipeBody.position += Vector3.left * pipeSpeed * Time.deltaTime;
    }

}
