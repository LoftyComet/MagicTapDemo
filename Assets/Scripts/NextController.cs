using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeys;

public class NextController : MonoBehaviour
{
    // Set Manually
    public Vector3 incre;
    public GameObject player;
    public bool right, left;
    private AcceStimulate AcceSti;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        AcceSti = GetComponent<AcceStimulate>();
        //incre = new Vector3(-0.5f, 0, 0);
        AcceSti.OutEvent.AddListener(AcceSti.SetInvoked);

        if (right)
            AcceSti.OutEvent.AddListener(moveRight);
        else if (left)
            AcceSti.OutEvent.AddListener(moveLeft);

        AcceSti.AfterOutEvent.AddListener(AcceSti.UnInvoked);
        //showKeyboard(current);
    }

    public void moveRight()
    {
        Debug.Log("Move right");
        player.GetComponent<SolverHandler>().AdditionalOffset += incre;
    }

    public void moveLeft()
    {
        Debug.Log("Move right");
        player.GetComponent<SolverHandler>().AdditionalOffset -= incre;
    }

}
