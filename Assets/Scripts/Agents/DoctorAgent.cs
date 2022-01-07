using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorAgent : GAgent
{
    private void Start()
    {
        SubGoal s1 = new SubGoal("Paperwork", 1, false);
        _goals.Add(s1, 1);

        SubGoal s2 = new SubGoal("Rested", 1, false);
        _goals.Add(s2, 3);
        
        SubGoal s3 = new SubGoal("Relieved", 1, false);
        _goals.Add(s3, 5);

        StartCoroutine(GetTiredRoutine());
        StartCoroutine(FillBladderRoutine());
    }

    private IEnumerator GetTiredRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15f, 30f));

            _beliefs.AddState("Exhausted", 0);
        }
    }
    
    private IEnumerator FillBladderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(30f, 60f));

            _beliefs.AddState("NeedRelief", 0);
        }
    }
}
