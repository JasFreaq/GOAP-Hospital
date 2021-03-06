using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientAgent : GAgent
{
    private void Start()
    {
        SubGoal s1 = new SubGoal("IsWaiting", 1, true);
        _goals.Add(s1, 3);
        
        SubGoal s2 = new SubGoal("WasTreated", 1, true);
        _goals.Add(s2, 5);
        
        SubGoal s3 = new SubGoal("ReachedHome", 1, true);
        _goals.Add(s3, 1);
    }
}
