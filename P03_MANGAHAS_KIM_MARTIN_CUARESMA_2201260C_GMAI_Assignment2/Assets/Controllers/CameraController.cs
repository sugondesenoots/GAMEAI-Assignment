using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSensitivity = 2.0f;

    public ShopBotStateManager _stateManager;

    private float xRotation = 0f;

    public Transform shopBot;
    public float detectionRange = 3f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (_stateManager.currentStateName != ("CollectionState"))
        {
            Vector3 directionToShopBot = (shopBot.position - transform.position).normalized;

            if (Vector3.Distance(transform.position, shopBot.position) <= detectionRange)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToShopBot, Vector3.up);
                transform.rotation = targetRotation;
            }
        }
    }
}