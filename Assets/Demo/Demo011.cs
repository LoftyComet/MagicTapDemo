using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo011 : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public TouchBlast TB;
    public GameObject player;
    public Color[] ColorToChange;
    private int ColorIndex;
    // Start is called before the first frame update
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();
        TB = gameObject.GetComponent<TouchBlast>();
        ColorIndex = 0;
        ColorToChange = new Color[6] { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, new Color(0.6666667f, 0.6666667f, 0.6666667f, 1) };
        Acce.OutEvent.AddListener(Acce.SetInvoked);
        Acce.OutEvent.AddListener(BubOpt.Close010Subs);
        TB.BeforeBlastEvent.AddListener(BubOpt.Close01Subs);
    }
    public void ChangeColor()
    {
        player.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange[ColorIndex]);
        if (ColorIndex == 5)
            ColorIndex = 0;
        else
            ColorIndex++;
        gameObject.GetComponent<TouchBlast>().Invoked = false;
    }
}
