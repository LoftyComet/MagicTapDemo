using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBc : MonoBehaviour
{
    // Start is called before the first frame update
    public float RecoverTarget;
    void Start()
    {
        RecoverTarget = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BlastBehavior()
    {
        // If the AcceStimulate interaction is canceled, disable the interaction
        gameObject.SendMessageUpwards("SetCloseSti", true);
        gameObject.SetActive(false);
        gameObject.GetComponent<TouchBlast>().UpdateDissolve(RecoverTarget);
    }         
}
