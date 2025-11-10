using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerModel _model;
    private PlayerView _view;

    public PlayerController(PlayerModel model, PlayerView view)
    {
        _model = model;
        _view = view;
        _model.OnHealthChanged += _view.UpdateHealth;
        _model.OnMoveVectorChange += _view.Move;

        _model.Heal(1);
    }

    public void FixedUpdate()
    {
        _model.MoveInput = Vector3.zero;
        if (_model.MoveInput != Vector3.zero)
        {
            Vector3 moveInDirection = _view.Camera.transform.TransformDirection(_model.MoveInput);
            moveInDirection.y = 0;
            moveInDirection.Normalize();

            Vector3 velocity = moveInDirection * _model.Speed;
            _view.Move(velocity);
        }
        else
        {
            _view.Move(Vector3.zero);
        }
    }
    public void DealDamage(float damage) => _model.TakeDamage(damage);
}
