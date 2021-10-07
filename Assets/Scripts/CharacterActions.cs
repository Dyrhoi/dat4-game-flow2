using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterActions : MonoBehaviour
{
    private Animator _animator;
    private int _pickItemHash;
    
    private PlayerInput _input;

    private BoxCollider _collider;

    private List<Collider> _colliders = new List<Collider>();

    private GameState _gameState;
    
    private void Awake()
    {
        _input = new PlayerInput();
        
        _input.CharacterControls.Actions.performed += ctx =>
        {
            _animator.SetBool(_pickItemHash, true);
            PickUpItem();
        };
        _input.CharacterControls.Actions.canceled += ctx => _animator.SetBool(_pickItemHash ,false);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _pickItemHash = Animator.StringToHash("pickItem");

        _collider = GetComponent<BoxCollider>();

        _gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }

    void PickUpItem()
    {
        Debug.Log("Got " + _colliders.Count + " coins");
        foreach (BoxCollider fCollider in _colliders)
        {
            _gameState.PickUpCoin(fCollider.gameObject);
        }
        
        Debug.Log("New score: " + _gameState.GetCoins());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            if (!_colliders.Contains(other))
                _colliders.Add(other);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            if (_colliders.Contains(other))
                _colliders.Remove(other);
        }
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
