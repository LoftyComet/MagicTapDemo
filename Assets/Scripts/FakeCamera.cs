using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Imitate the transform movement of the main camera (but not the rotation)
/// </summary>
public class FakeCamera : MonoBehaviour
{
    void Start()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
