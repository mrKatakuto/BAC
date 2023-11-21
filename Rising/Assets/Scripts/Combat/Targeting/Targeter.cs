using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetCroup;

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
        target.OnDestroyed += RemoveTarget;

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

        RemoveTarget(target);
    }

    public bool SelectTarget() 
    {
        if(targets.Count == 0) 
        {
            // if we dont have a target
            return false;
        }

        CurrentTarget = targets[0];

        // add targets into cinemachinetargetgroup
        cineTargetCroup.AddMember(CurrentTarget.transform, 1f, 2f);

        // if we have a target
            return true;

    }

    public void Cancel() 
    {
        if(CurrentTarget == null) 
        {
            return;
        }

        cineTargetCroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target) 
    {
        if(CurrentTarget == target) 
        {
            cineTargetCroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
