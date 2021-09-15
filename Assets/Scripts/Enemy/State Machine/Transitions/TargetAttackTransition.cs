using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttackTransition : Transition
{
    private float _awaitTime = 0;

    private void Update()
    {
        if (_awaitTime >= 1.5)
        {
            NeedTransit = true;
        }

        _awaitTime += Time.deltaTime;
    }
}
