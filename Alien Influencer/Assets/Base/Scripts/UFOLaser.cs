using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLaser : MonoBehaviour
{
    public LayerMask raycastLayer;
    public Vector3 deltaPosition = Vector3.zero;     // Position to cast the ray
    public Vector3 deltaDirection = Vector3.forward; // Direction to cast the ray relative to the object's forward
    public float rayLength = 10f;                    // Length of the ray
    public Color hitColor = Color.green;               // Color when the ray hits something
    public Color missColor = Color.yellow;            // Color when the ray doesn't hit anything

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Set initial properties of the LineRenderer
        lineRenderer.positionCount = 2;    // Only two points for a straight line (start and end of the ray)
        lineRenderer.startWidth = 0.1f;   // Thickness of the line
        lineRenderer.endWidth = 0.1f;
    }

    void FixedUpdate()
    {
        // Calculate the ray's direction
        Vector3 rayDirection = transform.TransformDirection(deltaDirection);

        // Perform the raycast
        RaycastHit hit;
        Vector3 startPos = transform.position + deltaPosition;  // Starting point of the ray
        Vector3 endPos = startPos + rayDirection * rayLength;  // Default end point (if nothing is hit)

        // Check if the ray hits something
        if (Physics.Raycast(startPos, rayDirection, out hit, rayLength, raycastLayer.value))
        {
            endPos = hit.point;  // If the ray hits something, update the end point to the hit location
            lineRenderer.startColor = hitColor;
            lineRenderer.endColor = hitColor;
            BuildingHighlighter.Instance.HighlightObject(hit.collider.gameObject);
        }
        else
        {
            // If the ray doesn't hit anything, use the default end point
            lineRenderer.startColor = missColor;
            lineRenderer.endColor = missColor;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            BuildingHighlighter.Instance.SelectObject();
        }

        // Draw the ray using the LineRenderer
        lineRenderer.SetPosition(0, startPos);  // Set the starting point of the line
        lineRenderer.SetPosition(1, endPos);    // Set the end point of the line
    }
}
