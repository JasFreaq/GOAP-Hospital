using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseAgent : GAgent
{
    private void Start()
    {
        SubGoal s1 = new SubGoal("TreatPatient", 1, false);
        _goals.Add(s1, 3);
        
        SubGoal s2 = new SubGoal("Rested", 1, false);
        _goals.Add(s2, 1);

        StartCoroutine(GetTiredRoutine());
    }

    private IEnumerator GetTiredRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15f, 30f));

            _beliefs.AddState("Exhausted", 0);
        }
    }
}
