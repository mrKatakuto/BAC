using System.Collections;
using System.Collections.Generic;
using System.Data;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetCroup;

    private Camera mainCamera;

    // store a list of targets in the players range
    private List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

    private void Start() 
    {
        mainCamera = Camera.main;
    }

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

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach(Target target in targets) 
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            // the nearest target to the center will be selected
            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            //sqrmagnitude how big the vector is

            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null)
        {
            return false;
        }

        CurrentTarget = closestTarget;

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
