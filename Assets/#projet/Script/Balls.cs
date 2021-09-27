using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SphereData
{
    public  bool isRed;
    public Vector3 position;
    public float x;
    public float y;
    public float z;
}

[RequireComponent(typeof(NavMeshAgent))]
public class Balls : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<Targets> targetPoints = new List<Targets>();
    public int indexNextDestination = 0;
    private Vector3 actualDestination;
    public bool isRed;
    private Renderer colorSphere;

    private string dataPath;




    void Start()
    {
        if (File.Exist(dataPath))
        {
            Load();
        }
        else
        {
            isRed = true;
        }
        colorSphere = GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = UnityEngine.Random.Range(1,100);
        agent.speed = UnityEngine.Random.Range(1f,6f);
        NextDestination(false);
        dataPath = Application.persistentDataPath + "/sphere.dat" + gameObject.name + ".dat";
    }


    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            NextDestination();
        }
    }
    private void NextDestination(bool change=true)
    {

        if(change)
            {
                changeColor();
                Save();
            }
        int oldIndex = indexNextDestination;

        while(oldIndex == indexNextDestination && targetPoints.Count >1)
        {
            indexNextDestination = UnityEngine.Random.Range(0,targetPoints.Count);
        }
        actualDestination = targetPoints[indexNextDestination].GivePoint();
        agent.SetDestination(actualDestination);

        if(indexNextDestination >= targetPoints.Count)
        {
            indexNextDestination =0;
        }

    }



    private void OnDrawGizmos()
    {
        if (agent != null)
        {
            Gizmos.DrawSphere(transform.position + Vector3.up *2,
         0.05f + (100-agent.avoidancePriority)*0.01f);
        }

    }

    private void changeColor()
    {
        if (!isRed)
        {
        Debug.Log("ciriciao");
        colorSphere.material.SetColor("_Color", Color.red);
        isRed= true;
        }
        else
        {
        Debug.Log("ciricacaoMeravijiao");
        colorSphere.material.SetColor("_Color", Color.green);
        isRed = false;
        }
    }
    public void Save()
    {
        FileStream file = File.Create(dataPath);
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            SphereData sd = new SphereData();
            sd.isRed = isRed;
            sd.position = transform.position;
            // sd.position.x = transform.position;
            // sd.position.y = transform.position;
            // sd.position.z = transform.position;
            bf.Serialize(file, sd);
        }
        finally
        {
            file.Close();
        }
    }

    public void Load()
    {
        FileStream file = File.Open(dataPath, FileMode.Open);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            dataPath = sd.Deserialize(file) as SphereData;
            isRed = sd.isRed;
            Vector3 position = new Vector3(sd); // (sd.x, sd.y, sd.z)
            transform.position = position;
        }
        finally
        {
            file.Close();
        }

    }
}
