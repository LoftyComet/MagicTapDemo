using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleC2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Color ColorToChange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlastBehavior()
    {
        gameObject.SendMessageUpwards("ChangePlayer", ColorToChange);
        gameObject.SendMessageUpwards("Inactivate");
    }
}
