using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LogWriter_ob : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public GameObject temp;
    private string date, filePath, fileName, fileFullPath;
    private List<GameObject> Obstacles = new List<GameObject> { };
    private FileStream fs;
    private StreamWriter sw;
    private int caseTime;
    private bool started = false;
    private AcceStudy3 acceStimulate;
    void Start()
    {
        ballSpawner = GetComponent<BallSpawner>();
        date = DateTime.Now.ToString("yyyyMMdd-HH-mm-ss");
        caseTime = 1;
        ballSpawner = GetComponent<BallSpawner>();
        ballSpawner.ExpStartEvent.AddListener(() => {
            started = true;
        });
        ballSpawner.ExpEndEvent.AddListener(() =>
        {
            started = false;
            
        });
        ballSpawner.RoundOverEvent.AddListener(() =>
        {
            Write();
            increCase();
        });

        temp = GameObject.Find("temp");
        acceStimulate = temp.GetComponent<AcceStudy3>();
    }
    private void Update()
    {
        if (started)
        {
            GetBallIndex();

        }
    }
    public string FormatMsg()
    {
        return caseTime + "," + ballSpawner.indexLoc.x + "," + ballSpawner.indexLoc.y + "," + ballSpawner.indexLoc.z + "," + acceStimulate.a + "," + acceStimulate.v + "," + ballSpawner.modeSelected;
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
            sw.WriteLine("Case,X,Y,Z,a,v,index");
            sw.Close();
            fs.Close();
        }
    }
    public void Write()
    {
        filePath = Application.persistentDataPath + "/Logs/" + date + "/ob_loc/";
        fileName = date + "-" + caseTime.ToString() + ".csv";
        fileFullPath = filePath + fileName;
        Debug.Log(fileFullPath);
        //acce = gameObject.GetComponent<AcceStudy3>();
        
        InitialPath();

        // Start writing once entering, no need to set invoked as true because we want it record every frame inside
        //acce.InEvent.AddListener(Write);
        // Increment the case time after getting the finger out
        //acce.AfterOutEvent.AddListener(increCase);

        Obstacles = ballSpawner.obstacleObjects;
        

        // New an fs and sw, close an fs and sw after writing
        // because a writer and file stream will lock the memory storage so that other gameobject cannot write
        fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
        sw = new StreamWriter(fs);
        foreach (var Obstacle in Obstacles)
        {
            Vector3 Ob_pos = Obstacle.transform.position;
            string log = caseTime + "," + Ob_pos.x + "," + Ob_pos.y + "," + Ob_pos.z + "," + Obstacles.IndexOf(Obstacle);
            sw.WriteLine(log);
        }
        
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
        
    }
}
