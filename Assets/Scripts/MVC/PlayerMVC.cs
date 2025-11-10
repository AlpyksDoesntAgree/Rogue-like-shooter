using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMVC : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [HideInInspector] public PlayerController Controller;
    private PlayerModel _model;
    

    private void Awake()
    {
        Init();

        if (_model == null)
            Debug.Log("Model didnt load");
        if (Controller == null)
            Debug.Log("Controller didnt load");
    }

    private void FixedUpdate()
    {
        Controller.FixedUpdate();
    }
    private void Init()
    {
        _model = new PlayerModel();
        Controller = new PlayerController(_model, _playerView);
    }
}
