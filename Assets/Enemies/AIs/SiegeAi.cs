using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeAi : Ai
{
    float turnRate;
    bool isFiring = false;
    float chargeTime = 10;
    

    void Awake()
    {
        turnRate = 0.5f;
    }

    void FixedUpdate()
    {
        _rb.velocity = Vector2.zero;
        if (Target == null || isFiring) return;

        Vector2 dir = -(Vector2)(transform.position - Target.transform.position);
        float attackAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        ship.transform.rotation = Quaternion.Slerp(ship.transform.rotation, Quaternion.AngleAxis(attackAngle, Vector3.forward), turnRate * Time.deltaTime);

        if (dir.magnitude > 7)
        {
            _rb.velocity = ship.transform.up.normalized * My.Movementspeed;
        } else
        {
            isFiring = true;
            StartCoroutine(Charge(dir));
        }
    }

    IEnumerator Charge(Vector2 dir)
    {
        var ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(chargeTime);
        My.weapon.Shoot(dir);
        ps.Stop();
        yield return new WaitForSeconds(2);

        isFiring = false;

    }
}
