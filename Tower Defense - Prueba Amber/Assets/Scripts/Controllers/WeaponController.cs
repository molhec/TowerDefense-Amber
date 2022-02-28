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
        // Calculate how often a weapon can be shot
        if (Input.GetAxis("Fire1") > 0 && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1 / model.fireRate;
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Calculate the beginning and end of the line renderer, the end gets also used to determine if we hit an Enemy
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hitData, 1000))
        {
            hitObject = hitData.collider;
            if(!hitObject.CompareTag("Player"))
            {
                worldPositionMouse = hitData.point;
                CalculateTrajectory(worldPositionMouse);
            }
        }
    }

    private void CalculateTrajectory(Vector3 worldPosMouse)
    {
        // Comunicate to WeaponViewer to Draw the Trajectory
        EventsController.current.DrawWeaponTrajectory(worldPosMouse, trajectoryColor);
    }

    private void Shoot()
    {
        if (!ReferenceEquals(hitObject, null))
        {
            float distance = Vector3.Distance(transform.position, worldPositionMouse);
            if (distance <= model.maxDistanceToRegisterDamage)
            {
                // Spawn the hit particle system
                GameObject hit = Instantiate(hitEffectPrefab, worldPositionMouse, Quaternion.identity, transform);
                Destroy(hit, 2f);
                // Comunicate to the hit Enemy and inflict damage
                EventsController.current.ReceiveDamage(hitObject, model.bulletToUse);
            }
        }
    }
}
