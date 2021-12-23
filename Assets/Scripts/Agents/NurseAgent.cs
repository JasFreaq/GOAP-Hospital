using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseAgent : GAgent
{
    private void Start()
    {
        SubGoal s1 = new SubGoal("TreatPatient", 1, true);
        _goals.Add(s1, 3);
    }
}
