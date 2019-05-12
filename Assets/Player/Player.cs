using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Unit
{
    public Weapon Weapon { get; set; }
    public float RepairPerRound { get; set; }

    private Rigidbody2D _rb;

    public GameObject _shipObject;
    public GameObject _canonArm;

    private Vector2 _movement = new Vector2(0, 0);
    void Start()
    {
        Weapon = new PlayerCanon(_canonArm);
        _rb = GetComponent<Rigidbody2D>();
        RepairPerRound = 15f;
    }

    void Update()
    {
        if (UpgradeManager.state_IsInUpgradeMenu) return;

        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        _rb.velocity = _movement * Movementspeed;

        if (_movement != Vector2.zero)
        {
            float movementAngle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg - 90;
            _shipObject.transform.rotation = Quaternion.Slerp(_shipObject.transform.rotation, Quaternion.AngleAxis(movementAngle, Vector3.forward), 20 * Time.deltaTime);
        }

        Vector3 attackDir = -(Vector2)(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float attackAngle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 90;
        _canonArm.transform.rotation = Quaternion.Slerp(_canonArm.transform.rotation, Quaternion.AngleAxis(attackAngle, Vector3.forward), 20 * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            Shoot(attackDir);
        }

        HandleUiStats.Instance.handleArmorChange();
    }

    private void Shoot(Vector2 dir)
    {
        Weapon.Shoot(dir);
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    public override void ReduceHp(float hp)
    {
        base.ReduceHp(hp);
        HandleUiStats.Instance.handleHpChange();
    }
}
