using UnityEngine;

public class S1Player : MonoBehaviour
{
    public Color[] ColorToChange;
    private int ColorIndex;
    public AcceStimulate acce;
    public float InAlpha = 0.3f, HesAlpha = 0.6f;

    private void Start()
    {
        acce = gameObject.GetComponentInParent<AcceStimulate>();
        ColorIndex = 0;
        ColorToChange = new Color[6] { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, new Color(0.6666667f, 0.6666667f, 0.6666667f, 1) };
    }

    public void ChangeColor()
    {

        print("Executed");

        gameObject.GetComponent<MeshRenderer>().material.SetVector("_BaseColor", ColorToChange[ColorIndex]);
        if (ColorIndex == 5)
            ColorIndex = 0;
        else
            ColorIndex++;
        
    }

    public void ChangeOpacity()
    {
        Material OpaqueMat = gameObject.GetComponent<MeshRenderer>().material;
        // get the current color of the material
        Color currentColor = OpaqueMat.GetColor("_BaseColor");

        // set the new alpha value of the color
        currentColor.a = 1F;

        // set the modified color back to the material
        OpaqueMat.SetColor("_BaseColor", currentColor);
    }

    public void SetOpacityBack()
    {
        Material OpaqueMat = gameObject.GetComponent<MeshRenderer>().material;
        // get the current color of the material
        Color currentColor = OpaqueMat.GetColor("_BaseColor");

        // set the new alpha value of the color
        currentColor.a = 1F;

        // set the modified color back to the material
        OpaqueMat.SetColor("_BaseColor", currentColor);
    }
}
