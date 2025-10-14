using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Setting")]
    //The transform (Usually the player) that the camera will follow
    public Transform target;

    [Header("Follow Setting")]
    //Offset from the target position so the camera is not exactly centered on the player
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    //Smoothness factor for camera movement; smaller value = snappier camera
    [Range(0.01f, 1f)] public float smoothTime = 0.2f;

    //Whether the camera's position is
    public bool followX = true;

    public bool followY = true;

    [Header("Bounds (World Units)")]
    //Enable or disable camera bounds clamping
    public bool useBounds = false;

    //Minimum world coordinates for the camera's position (bottom left corner)
    public Vector2 minBounds;

    //Maximum world coordinates for the camera's position (top right corner)
    public Vector2 maxBounds;

    [Header("Pixel Perfect Settings")]
    //Whether to snap camera's position to pixel grid (useful for pixel art games)
    public bool pixelPerfectSnap = false;

    //Pixels per unit (excludes your sprite PPU, eg., 16 or 32)
    public float pixelsPerUnit = 10f;

    //Internal variable to keep track of camera velocity for smoothDamp
    private Vector3 velocity = Vector3.zero;

    //variables for camera shake effect
    private Vector3 shakeOffset = Vector3.zero; //offset applied for shaking
    private float shakeDuration = 0f; //how long shake lasts
    private float shakeMagnitude = 0.1f; //Intensity of shake
    private float shakeDampingSpeed = 1.0f; //How fast shake fades out

    //called every frame after all update() calls, to make camera follow smooth
    void LateUpdate()
    {
        //if not target assigned, do nothing
        if (target == null) return;

        //Calculate the desired camera position based on target position plus offset
        Vector3 desiredPosition = target.position + offset;

        //Optionally lock movement on X-axis
        if (!followX)
            desiredPosition.x = transform.position.x;

        //Optionally lock movement on y-axis
        if (!followY)
            desiredPosition.y = transform.position.y;

        //Smoothly move the camera towards the desired position using SmoothDamp
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        //If bounds are enabled, clamp the camera inside the defined world rectangle
        if (useBounds)
        {
            //Get the main camera component (should be orthographic for 2D)
            Camera cam = Camera.main;
            if (cam.orthographic)
            {
                //Calculate half the camera's height based on orthographic size
                float camHeight = cam.orthographicSize;

                //Calculate half the camera's width using aspect ration
                float camWidth = camHeight * cam.aspect;

                //Clamp X position between 

                smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x + camWidth, maxBounds.x - camWidth);

                //
                smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y + camHeight, maxBounds.y - camHeight);
            }
        }
        if (shakeDuration > 0)
        {
            shakeOffset = Random.insideUnitSphere * shakeMagnitude;

            shakeOffset.z = 0;

            shakeDuration -= Time.deltaTime * shakeDampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            shakeOffset = Vector3.zero;
        }

        smoothedPosition += shakeOffset;

        if (pixelPerfectSnap)
            smoothedPosition = PixelPerfect(smoothedPosition);

        transform.position = smoothedPosition;
    }

    private Vector3 PixelPerfect(Vector3 position)
    {
        float snappedX = Mathf.Round(position.x * pixelsPerUnit) / pixelsPerUnit;

        float snappedY = Mathf.Round(position.y * pixelsPerUnit) / pixelsPerUnit;

        float snappedZ = position.z;

        return new Vector3(snappedX, snappedY, snappedZ);
    }

    public void ShakeCamera(float duration, float magnitude, float dampingSpeed = 1f)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeDampingSpeed = dampingSpeed;
    }
}
