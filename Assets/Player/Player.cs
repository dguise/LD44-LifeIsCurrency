﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Unit
{
    public Weapon Weapon { get; set; }
    private Rigidbody2D _rb;

    private Vector2 _movement = new Vector2(0, 0);
    void Start()
    {
        Weapon = new PlayerCanon(gameObject);
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        _rb.velocity = _movement;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Weapon.Shoot();
    }
}
