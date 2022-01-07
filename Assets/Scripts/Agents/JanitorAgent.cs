using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorAgent : GAgent
{
    private void Start()
    {
        SubGoal s1 = new SubGoal("DoneCleaning", 1, false);
        _goals.Add(s1, 3);
    }
}
