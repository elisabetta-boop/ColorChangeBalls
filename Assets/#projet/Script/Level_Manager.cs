using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.AI;



public class Level_Manager : MonoBehaviour
{
    public static Level_Manager instance;
    public SphereData data;
    
   
    // [Serializable]
    // public static class SerializedTransformExtention
    // {
    //     public static void DeserialTransform(this SerializedTransform _serializedTransform, Transform _transform)
    //     {
    //         _transform.localPosition = new Vector3(_serializedTransform._position[0], _serializedTransform._position[1], _serializedTransform._position[2]);
    //     }
    // }

    // [Serializable]
    // public static class TransformExtention
    // {
    //     public static void SetTransformEX(this Transform original, Transform copy)
    //     {
    //         original.position = copy.position;
    //     }
    // }

    void awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

