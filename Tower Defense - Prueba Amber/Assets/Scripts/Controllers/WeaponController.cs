using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon model;
    [SerializeField] private Color trajectoryColor;
    [SerializeField] private GameObject hitEffectPrefab;

    private Camera mainCamera;
    private Vector3 worldPositionMouse;
    private Collider hitObject;

    private float nextTimetoFire;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1 / model.fireRate;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hitData, 1000))
        {
            worldPositionMouse = hitData.point;
            hitObject = hitData.collider;
            CalculateTrajectory(worldPositionMouse);
        }
    }

    private void CalculateTrajectory(Vector3 worldPosMouse)
    {
        EventsController.current.DrawWeaponTrajectory(worldPosMouse, trajectoryColor);
    }

    private void Shoot()
    {
        if (!ReferenceEquals(hitObject, null))
        {
            GameObject hit = Instantiate(hitEffectPrefab, worldPositionMouse, Quaternion.identity, transform);
            Destroy(hit, 2f);
            EventsController.current.ReceiveDamage(hitObject, model.bulletToUse);
        }
    }
}
