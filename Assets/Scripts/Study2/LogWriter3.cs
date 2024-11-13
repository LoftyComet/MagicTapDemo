using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LogWriter3 : MonoBehaviour
{
    public BallSpawner ballSpawner;
    private string date, filePath, fileName, fileFullPath;
    private GameObject[] Targets, Obstacles;
    private FileStream fs;
    private StreamWriter sw;
    private int caseTime;
    private bool started = false;
    void Start()
    {
        ballSpawner = GetComponent<BallSpawner>();
        date = DateTime.Now.ToString("yyyyMMdd-HH-mm-ss");
        filePath = Application.persistentDataPath + "/Logs/" + date + "/";
        fileName = date + "-" + "time" + ".csv";
        fileFullPath = filePath + fileName;
        Debug.Log(fileFullPath);
        //acce = gameObject.GetComponent<AcceStimulate>();
        caseTime = 0;
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
        ballSpawner.ExpStartEvent.AddListener(() =>
        {
            started = true;
        });
        ballSpawner.ExpEndEvent.AddListener(() =>
        {
            started = false;
        });
        ballSpawner.RoundOverEvent.AddListener(Write);
    }

    public string FormatMsg()
    {
        string ans = caseTime + "," + ballSpawner.errorTimes + "," + ballSpawner.errorInTimes + "," + ballSpawner.usedTime + "," + ballSpawner.triggerCondition + "," + ballSpawner.initialBall + "," + ballSpawner.rotationDirection + "," + ballSpawner.modeSelected + "," + ballSpawner.TargetEnd;
        ans += "," + ballSpawner.largeCirclePoint.x;
        ans += "," + ballSpawner.largeCirclePoint.y;
        ans += "," + ballSpawner.largeCirclePoint.z;
        foreach (var temp in ballSpawner.tatgetPos)
        {
            ans += "," + temp.x;
            ans += "," + temp.y;
            ans += "," + temp.z;
        }
        return ans;
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
            sw.WriteLine("Case,errorTimes,errorInTimes,usedTime,triggerCondition,start,direction(0 for ),modeSelected,ballIndex,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ,targetX,targetY,targetZ");
            sw.Close();
            fs.Close();
        }
    }
    public void Write()
    {
        // New an fs and sw, close an fs and sw after writing
        // because a writer and file stream will lock the memory storage so that other gameobject cannot write
        if (started)
        {
            caseTime++;
            fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);
            string log = FormatMsg();
            sw.WriteLine(log);
            sw.Close();
            fs.Close();
        }

    }
    public void increCase()
    {
        caseTime++;
    }
    public string GetFilePath()
    {
        return filePath;
    }
    public string GetFileFullPath()
    {
        return fileFullPath;
    }
}
