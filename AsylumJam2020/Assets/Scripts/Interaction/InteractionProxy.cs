using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionProxy : InteractionBase
{
    [SerializeField] private InteractionBase _linkedInteraction;

    public override InteractionBase GetInteraction() { return _linkedInteraction; }
}
