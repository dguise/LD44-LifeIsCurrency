using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    /*
     States:
     1. Approach target:
            if siege unit, avoid player
            if drone, seek out player if close
     2. Shoot target

     */
    protected Rigidbody2D _rb;
    [SerializeField] protected GameObject ship;
    protected GameObject Target;
    protected GameObject PlayerObject;
    protected Player Player;
    protected Enemy My;

    private void Start()
    {
        Target = GameManager.Target;
        Player = GameManager.Player;
        PlayerObject = Player.gameObject;
        My = GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = (GameManager.Player.transform.position - transform.position).normalized * 1;
    }
}
