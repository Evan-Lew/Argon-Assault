using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] private float controlSpeed = 10f;
    [Tooltip("How fast player moves horizontally")] [SerializeField] private float xRange = 5f;
    [Tooltip("How fast player moves horizontally")] [SerializeField] private float yRange = 7f;
    
    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")] [SerializeField] private GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float positionYawFactor = 2f;
    
    [Header("Player input based tuning")]
    [SerializeField] private float controlPitchFactor = -20f;
    [SerializeField] private float controlRollFactor = -20f;
    
    private float xThrow, yThrow;
    
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        
        float xOffSet = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffSet;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        
        float yOffSet = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffSet;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
