using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMVC : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private List<Transform> _spawnPos  = new List<Transform>();
    private EnemyController _controller;

    private void Awake()
    {
        _controller.Enemies = _enemies;
        _controller.SpawnPos = _spawnPos;

        _controller.SpawnEnemies();
    }
}
