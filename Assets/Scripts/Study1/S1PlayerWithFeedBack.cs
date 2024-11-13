using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1PlayerWithFeedBack : MonoBehaviour
{
    public Color[] ColorToChange;
    private int ColorIndex;
    public float InAlpha = 0.3f, HesAlpha = 0.6f;
    private Color Red = new Color32(255, 0, 0, 255);
    private Color Green = new Color32(0, 255, 0, 255);
    private Color Blue = new Color32(0, 0, 255, 255);
    private Color Yellow = new Color32(255, 255, 0, 255);
    private Color Cyan = new Color32(0, 255, 255, 255);
    private Color Grey = new Color32(128, 128, 128, 255);

    private void Start()
    {
        ColorIndex = 0;
        ColorToChange = new Color[6] {Red, Green, Blue, Yellow, Cyan, Grey};
    }

    public void ChangeColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange[ColorIndex]);
        print(ColorIndex);
        if (ColorIndex == 5)
            ColorIndex = 0;
        else
            ColorIndex++;
    }

    public void ChangeOpacityWhenHes()
    {
        ChangeColor(); 
        Material OpaqueMat = gameObject.GetComponent<MeshRenderer>().material;
        Color OpaqueMatColor = OpaqueMat.color;
        OpaqueMatColor.a = InAlpha;
    }

    public void ChangeOpacityWhenIn()
    {
        ChangeColor();
        Material OpaqueMat = gameObject.GetComponent<MeshRenderer>().material;
        Color OpaqueMatColor = OpaqueMat.color;
        OpaqueMatColor.a = HesAlpha;
    }
}
