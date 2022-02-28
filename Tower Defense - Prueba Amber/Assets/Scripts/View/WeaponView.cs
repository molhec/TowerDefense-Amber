using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    
    private Camera mainCamera;

    private void Start()
    {
        lineRenderer.gameObject.SetActive(true);
        EventsController.current.OnDrawWeaponTrajectory += DrawTrajectory;
    }

    private void OnDestroy()
    {
        lineRenderer.gameObject.SetActive(false);
        EventsController.current.OnDrawWeaponTrajectory -= DrawTrajectory;
    }

    private void DrawTrajectory(Vector3 worldPositionMouse, Color trajectoryColor)
    {
        lineRenderer.startColor = trajectoryColor;
        lineRenderer.endColor = trajectoryColor;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, worldPositionMouse);
    }
}
