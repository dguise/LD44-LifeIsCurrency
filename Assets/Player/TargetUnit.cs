using UnityEngine;
using System.Collections;

public class TargetUnit : Unit
{
    protected override void Die()
    {
        GameManager.Instance.GameOver();
    }
}
