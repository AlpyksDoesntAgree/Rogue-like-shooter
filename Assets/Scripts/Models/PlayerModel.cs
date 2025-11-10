using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    //move
    private float _speed = 2f;
    private Vector3 _moveInput;
    //hp
    private float _health = 100f;
    private float _maxHealth = 100f;
    private bool _isDead = false;

    public float Health
    {
        get => _health;
        private set
        {
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            OnHealthChanged?.Invoke(_health, _maxHealth);

            if(_health <= 0)
            {
                _isDead = true;
                OnPlayerDeath?.Invoke();
            }
        }
    }
    public float Speed 
    { 
        get
        {
            if (_isDead)
                return 0;

            float finalSpeed = _speed;
            if (Health <= 30f) finalSpeed *= 0.7f;
            return finalSpeed;
        }
        private set => _speed = value; 
    }
    public Vector3 MoveInput
    {
        get
        {
            if (_isDead) return Vector3.zero;
            else return _moveInput;
        }
        set 
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            
            _moveInput = new Vector3(horizontal, 0, vertical);
            OnMoveVectorChange?.Invoke(_moveInput);
        }
    }

    public event Action<float, float> OnHealthChanged;
    public event Action OnPlayerDeath;
    public event Action<Vector3> OnMoveVectorChange;
    public void TakeDamage(float damage) => Health -= damage;
    public void Heal(float healAmount) => Health += healAmount;
}
