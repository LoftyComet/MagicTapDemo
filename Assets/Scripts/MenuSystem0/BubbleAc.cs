using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAc : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveDist;
    void Start()
    {
        MoveDist = 0.1f;
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.position -= new Vector3(0, 0, MoveDist);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlastBehavior()
    {
        gameObject.SendMessageUpwards("CloseSubs");
    }
}
