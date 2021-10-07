using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private Animator _animator;

    private int _isWalkingHash;
    private int _isRunningHash;

    private PlayerInput _input;

    private Vector2 _currentMovement;
    private bool _movementPressed;
    private bool _runPressed;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.CharacterControls.Movement.performed += ctx =>
        {
            _currentMovement = ctx.ReadValue<Vector2>();
            _movementPressed = true;
        };
        _input.CharacterControls.Movement.canceled += ctx => _movementPressed = false;
        
        _input.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.Run.canceled += ctx => _runPressed = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        if (_movementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        
        if (!_movementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        

        if ((_movementPressed && _runPressed) && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }
        
        if ((!_movementPressed || !_runPressed) && isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }
        
    }

    void HandleRotation()
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(_currentMovement.x, 0, _currentMovement.y);
        
        var targetRotation = Quaternion.LookRotation(newPosition);
       
// Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
    }

    private void OnEnable()
    {
        _input.CharacterControls.Enable();
    }
    
    private void OnDisable()
    {
        _input.CharacterControls.Disable();
    }
}
