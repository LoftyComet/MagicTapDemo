using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;


public class BubbleB2 : MonoBehaviour
{
    public GameObject CloseB;
    public GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("PlayerB2");
        foreach (Transform t in transform)
        {
            if (t.gameObject.tag == "B2C")
            {
                CloseB = t.gameObject;
                CloseB.SetActive(false);
            }
        }
        gameObject.GetComponent<AcceStimulate>().Acce = true;
    }

    public void HesBehavior()
    {
        CloseB.SetActive(true);
    }

    public void ExeBehavior()
    {
        // If the AcceStimulate is canceled, then the execution is stopped
        if (gameObject.GetComponent<AcceStimulate>().CloseSti)
        {
            return;
        }
        Material material = Player.GetComponent<MeshRenderer>().material;
        if (material.GetColor("Color_") == Color.red)
        {
            material.SetColor("Color_", Color.blue);
        }
        else
        {
            material.SetColor("Color_", Color.red);
        }
        // Hide CloseB after normally executed of 01-Hesitated-0
        CloseB.SetActive(false);
    }
}
