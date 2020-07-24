﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent (typeof (PlayerContoller))]
[RequireComponent (typeof (GunController))]
public class Player : LivingEntity
{
    public float speed = 7;
    Camera viewCamera;
    PlayerContoller controller;
    GunController gunController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerContoller>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movements
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * speed;
        controller.Move(moveVelocity);

        // Look Input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero) ;
        float raydistance;

        if (groundPlane.Raycast(ray, out raydistance))
        {
            Vector3 point = ray.GetPoint(raydistance);
            //Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }

        //weapon Input
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }

        
    }
}
