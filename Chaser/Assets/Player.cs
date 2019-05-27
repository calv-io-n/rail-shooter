using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In meters per sec")] [SerializeField] float speed = 4f;

    [Tooltip("Limit")] [SerializeField] float xLimit = 23.0f;
    [Tooltip("Limit")] [SerializeField] float yLimit = 23.0f;

    // Local Rotation
    // transform.localRotation

    float xThrow, yThrow;

    [SerializeField] float positionPitchFactor = -5.0f;
    [SerializeField] float controlPitchFactor = -5.0f;
    [SerializeField] float positionYawFactor = -5.0f;
    [SerializeField] float controlYawFactor = -5.0f;
    [SerializeField] float positionRollFactor = -5.0f;
    [SerializeField] float controlRollFactor = -5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        
    }

    private void ProcessRotation()
    {
        float pitchPos = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yawPos = transform.localPosition.x * positionYawFactor;
        float rollPos = transform.localPosition.x * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitchPos, yawPos, rollPos);
    }

    private void ProcessTranslation()
    {
        // Using the CrossPlatform manager as a middle layer and using player input that is framerate independent 
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        
        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        // If offset is called
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        // Clamp Movement to Set of Values
        float yPos = Mathf.Clamp(rawYPos, -yLimit, yLimit);
        float xPos = Mathf.Clamp(rawXPos, -xLimit, xLimit);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);

    }
}
