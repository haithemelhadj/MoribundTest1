using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    public Inputs inputsScript;

    private void Awake()
    {
        inputsScript = GetComponent<Inputs>();
    }

    private void Update()
    {
        CheckForLedge();
    }



    public Transform ray1Position;
    public Transform ray2Position;
    public void CheckForLedge()
    {
        //send 2 raycast in front of the player if the lower hits and the upper doesn't then he's close to a ladge 
        //if player is close to a laedge add an up force and null horizontal input for milliseconds
    }

    public float bumpForce;
    public void LedgeBump()
    {

    }
}
