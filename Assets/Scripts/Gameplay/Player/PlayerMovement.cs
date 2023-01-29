using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float _speed;
    [SerializeField] private float _mouseSensitivity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 step = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        step = transform.TransformDirection(step);

        _characterController.Move(step * _speed * Time.deltaTime);
    }

    private void Turn()
    {
        Vector3 turnStep = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
        
        transform.Rotate(turnStep * _mouseSensitivity * Time.deltaTime, Space.World);
    }
}
