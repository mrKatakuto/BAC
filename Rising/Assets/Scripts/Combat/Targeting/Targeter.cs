using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    // store a list of targets in the players range

    private List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if (target == null)
        {
            return;
        }

        targets.Add(target);

        // second possibility
        //if(!other.TryGetComponent<Target>(out Target target)) {return; }
        //{
        //    target.Remove(Target)
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if(target == null)
        {
            return;
        }

        targets.Remove(target);

    }

    public bool SelectTarget() 
    {
        if(targets.Count == 0) 
        {
            // if we dont have a target
            return false;
        }

        CurrentTarget = targets[0];

        // if we have a target
            return true;

    }

    public void Cancel() 
    {
        CurrentTarget = null;
    }
}
