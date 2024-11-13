using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LogWriter2 : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public GameObject temp;
    private string date, filePath, fileName, fileFullPath;
    private GameObject[] Targets, Obstacles;
    private FileStream fs;
    private StreamWriter sw;
    private int caseTime;
    private bool started = false;
    private AcceStimulate acceStimulate;
    private int ballIndex = -1;
    private int ballKind = 3;
    private int remainTime = 0;
    public GameObject expCamera;
    void Start()
    {
        expCamera = GameObject.Find("CenterEyeAnchor");
        ballSpawner = GetComponent<BallSpawner>();
        date = DateTime.Now.ToString("yyyyMMdd-HH-mm-ss");
        filePath = Application.persistentDataPath + "/Logs/" + date + "/";
        fileName = date + "-" + "trajectory" + ".csv";
        fileFullPath = filePath + fileName;
        Debug.Log(fileFullPath);    
        //acce = gameObject.GetComponent<AcceStimulate>();
        caseTime = 1;
        InitialPath();

        // Start writing once entering, no need to set invoked as true because we want it record every frame inside
        //acce.InEvent.AddListener(Write);
        // Increment the case time after getting the finger out
        //acce.AfterOutEvent.AddListener(increCase);
        Targets = GameObject.FindGameObjectsWithTag("Target");
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var target in Targets)
        {
            target.GetComponent<AcceStimulate>();
        }
        ballSpawner = GetComponent<BallSpawner>();
        ballSpawner.ExpStartEvent.AddListener(() => { 
            started = true;
        });
        ballSpawner.ExpEndEvent.AddListener(() =>
        {
            started = false;
            remainTime = 2;
        });
        ballSpawner.RoundOverEvent.AddListener(increCase);

        temp = GameObject.Find("temp");
        acceStimulate = temp.GetComponent<AcceStimulate>();
    }
    private void FixedUpdate()
    {
        if (started)
        {
            GetBallIndex();
            Write();
        }
        if (remainTime > 0)
        {
            Write();
            remainTime--;
        }
    }
    public string FormatMsg()
    {
        return ballSpawner.caseTime + "," + ballSpawner.indexLoc.x + "," + ballSpawner.indexLoc.y + "," + ballSpawner.indexLoc.z + "," + acceStimulate.a + "," + acceStimulate.v + "," + ballSpawner.modeSelected + "," + ballKind + "," + ballIndex
            + "," + expCamera.transform.position.x + "," + expCamera.transform.position.y + "," + expCamera.transform.position.z
            + "," + expCamera.transform.rotation.x + "," + expCamera.transform.rotation.y + "," + expCamera.transform.rotation.z
            + "," + ballSpawner.thumbLoc.x + "," + ballSpawner.thumbLoc.y + "," + ballSpawner.thumbLoc.z;
    }
    public void InitialPath()
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        if (!File.Exists(fileFullPath))
        {
            // Close the operator after creation
            fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.WriteLine("Case,X,Y,Z,a,v,modeSelected,area_type,index,is_triggered");
            sw.Close();
            fs.Close();
        }
    }
    public void Write()
    {
        // New an fs and sw, close an fs and sw after writing
        // because a writer and file stream will lock the memory storage so that other gameobject cannot write
        fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
        sw = new StreamWriter(fs);
        string log = FormatMsg();
        sw.WriteLine(log);
        sw.Close();
        fs.Close();
        //ballSpawner.index = -1;
        //ballSpawner.area_type = 3;
    }
    public void increCase()
    {
        if (started)
        {
            caseTime++;
            print("ÇÐ»»case");
        }
        
    }
    public string GetFilePath()
    {
        return filePath;
    }
    public string GetFileFullPath()
    {
        return fileFullPath;
    }
    public void GetBallIndex()
    {
        ballIndex = ballSpawner.index;
        ballKind = ballSpawner.area_type;
}
}
