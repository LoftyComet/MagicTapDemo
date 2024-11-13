using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBallBehavior2 : MonoBehaviour
{
    AcceStimulate acce;
    S1Clock s1clock;
    bool startCorrectly = false;
    public Material shallowBall;
    public Material oriBall;
    // Start is called before the first frame update
    void Start()
    {
        acce = GetComponent<AcceStimulate>();
        acce.InEvent.AddListener(() =>
        {
            startCorrectly = true;
            gameObject.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().material = shallowBall;
        });
        acce.AfterOutEvent.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetState()
    {
        return startCorrectly;
    }
    public void TureUnstarted()
    {
        startCorrectly = false;
        gameObject.GetComponent<MeshRenderer>().material = oriBall;
        gameObject.SetActive(true);
    }
}
