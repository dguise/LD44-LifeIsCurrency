using UnityEngine;
using System.Collections;

public class TargetUnit : Unit
{
    protected override void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.GameOver();
    }
}
