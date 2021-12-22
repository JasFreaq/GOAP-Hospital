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
    }
}
