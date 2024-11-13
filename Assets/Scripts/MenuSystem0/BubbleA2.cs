using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleA2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveDist;
    public Vector3 RecDist;
    public Color ColorToChange;
    void Start()
    {
        MoveDist = 0.1f;
        RecDist = new Vector3(0, 0.05f, -0.1f);
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.position -= new Vector3(0, 0, MoveDist);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BlastBehavior()
    {
        gameObject.SendMessageUpwards("Move", RecDist);
        gameObject.SendMessageUpwards("ChangePlayer", ColorToChange);
        gameObject.SendMessageUpwards("InactivateChild", 1);
    }
}
