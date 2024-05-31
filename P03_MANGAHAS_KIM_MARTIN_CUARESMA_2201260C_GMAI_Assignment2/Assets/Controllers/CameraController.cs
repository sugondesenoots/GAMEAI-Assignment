using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player; 
    [SerializeField] private float mouseSensitivity = 2.0f; 

    public ShopBotStateManager _stateManager; 

    private float xRotation = 0f; 

    public Transform shopBot; 
    public float detectionRange = 3f; //The range in which the camera detects the shop bot for interaction/looking at shop bot

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _player.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //This condition excludes both the CollectionState & PaymentState 
        //Reason I did this was because during the gameplay, it was annoying to perform the tasks 
        //When I performed the tasks, it would look at the bot, disrupting the experience
        if (_stateManager.currentStateName != "CollectionState" && _stateManager.currentStateName != "PaymentState")
        {
            Vector3 directionToShopBot = (shopBot.position - transform.position).normalized;

            //If within the detection range, rotate the camera to look at the shop bot 
            //This makes it feel more interactive when interacting with the shop bot
            if (Vector3.Distance(transform.position, shopBot.position) <= detectionRange)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToShopBot, Vector3.up);
                transform.rotation = targetRotation;
            }
        }
    }
}
