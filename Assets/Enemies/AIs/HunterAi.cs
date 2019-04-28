using UnityEngine;
using System.Collections;

public class HunterAi : Ai
{
    Vector3 targetOffset;
    float reaction;
    float turnRate;

    void Awake()
    {
        targetOffset = Random.insideUnitCircle * 2;
        reaction = Random.Range(0.3f, 3f);
        turnRate = Random.Range(0.5f, 2f);
    }
    void FixedUpdate()
    {
        Vector2 dir = -(Vector2)(transform.position - Player.transform.position + targetOffset);
        Debug.Log(dir.magnitude);

        if (dir.magnitude > reaction) {
            float attackAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation, Quaternion.AngleAxis(attackAngle, Vector3.forward), turnRate * Time.deltaTime);
        }
        else
        {
            
        }


        _rb.velocity = ship.transform.up.normalized * My.Movementspeed;
    }
}
