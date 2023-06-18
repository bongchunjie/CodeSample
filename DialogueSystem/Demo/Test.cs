using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Test : MonoBehaviour
{
    public SequenceEventExecutor executor;

    private void Start()
    {
        executor.Init(OnFinishedEvent);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            executor.Execute();
        }
    }

    void OnFinishedEvent(bool success)
    {
        Debug.Log(success);
    }
}
