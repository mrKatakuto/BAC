using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract base class for other classes, wird nicht instanziiert
public abstract class State
{
    public abstract void Enter();

    public abstract void Tick(float deltaTime);

    public abstract void Exit();

}
