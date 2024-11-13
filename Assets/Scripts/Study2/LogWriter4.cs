using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LogWriter4 : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public GameObject temp;
    private string date, filePath, fileName, fileFullPath;
    private GameObject[] Targets, Obstacles;
    private FileStream fs;
    private StreamWriter sw;
    private int caseTime;
    private bool started = false;
    private AcceStudy3 acceStimulate;
    void Start()
    {
        ballSpawner = GetComponent<BallSpawner>();
        date = DateTime.Now.ToString("yyyyMMdd-HH-mm-ss");
        filePath = Application.persistentDataPath + "/Logs/" + date + "/";
        fileName = date + "-" + "errorLoc" + ".csv";
        fileFullPath = filePath + fileName;
        Debug.Log(fileFullPath);
        //acce = gameObject.GetComponent<AcceStudy3>();
        caseTime = 0;
        InitialPath();


        ballSpawner = GetComponent<BallSpawner>();
        ballSpawner.ExpStartEvent.AddListener(() => { started = true; });
        ballSpawner.ExpEndEvent.AddListener(() => { started = false; });
        ballSpawner.RoundOverEvent.AddListener(increCase);
        ballSpawner.RoundOverEvent.AddListener(Write2);
    }
    private void Update()
    {

    }
    public string FormatMsg()
    {
        return caseTime + "," + ballSpawner.indexLoc.x + "," + ballSpawner.indexLoc.y + "," + ballSpawner.indexLoc.z + "," + acceStimulate.a + "," + acceStimulate.v;
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
            sw.WriteLine("Case,X,Y,Z,modeSelected");
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
    }
    public void Write2()
    {
        // New an fs and sw, close an fs and sw after writing
        // because a writer and file stream will lock the memory storage so that other gameobject cannot write
        if (started)
        {
            fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);
            for (int i = 0; i < ballSpawner.errorX.Count; i++)
            {
                string log = caseTime + "," + ballSpawner.errorX[i] + "," + ballSpawner.errorY[i] + "," + ballSpawner.errorZ[i] + "," + ballSpawner.modeSelected.ToString();
                sw.WriteLine(log);
            }


            sw.Close();
            fs.Close();
        }

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
}
