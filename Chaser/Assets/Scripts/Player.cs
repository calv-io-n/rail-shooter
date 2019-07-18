using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In meters per sec")] [SerializeField] float speed = 4f;

    [Tooltip("Limit")] [SerializeField] float xLimit = 23.0f;
    [Tooltip("Limit")] [SerializeField] float yLimit = 23.0f;
    [SerializeField] float yLimitmin = 0.0f;

    [SerializeField] GameObject[] weapon;

    // Local Rotation
    // transform.localRotation

    float xThrow, yThrow;

    [SerializeField] float positionPitchFactor = -5.0f;
    [SerializeField] float controlPitchFactor = -5.0f;
    [SerializeField] float positionYawFactor = -5.0f;
    [SerializeField] float controlYawFactor = -5.0f;
    [SerializeField] float positionRollFactor = -5.0f;
    [SerializeField] float controlRollFactor = -5.0f;

    bool isPlayerAlive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive)
        {
            Attack();
            ProcessTranslation();
            ProcessRotation();
        }
    }

    void OnPlayerDeath()
    {
        isPlayerAlive = false;
    }

     private void ProcessRotation()
    {
        float pitchPos = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yawPos = transform.localPosition.x * positionYawFactor;
        float rollPos = xThrow * controlRollFactor;

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
        float yPos = Mathf.Clamp(rawYPos, yLimitmin, yLimit);
        float xPos = Mathf.Clamp(rawXPos, -xLimit, xLimit);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);

    }

    void Attack()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            foreach(GameObject attack in weapon)
            {
                attack.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject attack in weapon)
            {
                attack.SetActive(false);
            }
        }
    }
}
