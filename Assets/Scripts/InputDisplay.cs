using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDisplay : MonoBehaviour
{
    public AcceStimulate Acce;
    public Key key;
    
    // Start is called before the first frame update
    void Start()
    {
        Acce = GetComponent<AcceStimulate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
