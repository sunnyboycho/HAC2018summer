using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState {

    protected UnitScript unitScript;

    public abstract void Action();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public UnitState(UnitScript unitScript)
    {
        this.unitScript = unitScript;
    }
}
