using UnityEngine;
using System.Collections;

public class HunterAi : Ai
{
    Vector3 targetOffset;
    public float offsetModifier = 0.6f;
    public float turnRateModifier = 1f;
    float reaction;
    float turnRate;

    void Awake()
    {
        targetOffset = Random.insideUnitCircle * offsetModifier;
        reaction = Random.Range(0.3f, 3f);
        turnRate = Random.Range(0.5f, 2f) * turnRateModifier;
    }
    void FixedUpdate()
    {
        var target = Player != null ? PlayerObject : Target;
        Vector2 dir = -(Vector2)(transform.position - target.transform.position + targetOffset);

        if (dir.magnitude > reaction) {
            float attackAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation, Quaternion.AngleAxis(attackAngle, Vector3.forward), turnRate * Time.deltaTime);
        }
        else
        {
            My.weapon.Shoot(dir);
        }


        _rb.velocity = ship.transform.up.normalized * My.Movementspeed;
    }
}
