using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the object camera locked (body locked)
/// </summary>
public class Body_Lock : MonoBehaviour
{
    private Transform MainCamera;
    public Vector3 InitDist;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main.transform;
        InitDist = transform.position - MainCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MainCamera.position + InitDist;
    }
}
