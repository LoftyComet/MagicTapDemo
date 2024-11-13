using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes the text follow the camera facing angles horizontally
/// </summary>
public class FollowCamera : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject MainCamera;
    Vector3 CameraAngles;
    Quaternion Quaternion;
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        CameraAngles = MainCamera.transform.rotation.eulerAngles;
        CameraAngles.z = 0;
        Quaternion.eulerAngles = CameraAngles;
        gameObject.GetComponent<RectTransform>().rotation = Quaternion;
    }
}
