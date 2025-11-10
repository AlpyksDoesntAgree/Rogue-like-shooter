using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController 
{
    public List<GameObject> Enemies = new List<GameObject>();
    public List<Transform> SpawnPos = new List<Transform>();
    private int _randomIndex;
    
    public void SpawnEnemies()
    {
        for(int i = 0; i < SpawnPos.Count; i++)
        {
            GameObject.Instantiate(GenerateEnemy(), SpawnPos[i].position, Quaternion.identity);
        }
    }
    private GameObject GenerateEnemy()
    {
        _randomIndex = Random.Range(0,3);
        GameObject obj = Enemies[_randomIndex];

        switch ( _randomIndex )
        {
            case 0: obj.GetComponent<EnemyView>().Enemy = new Warrior(); break;
            case 1: obj.GetComponent<EnemyView>().Enemy = new Ranger(); break;
            case 2: obj.GetComponent<EnemyView>().Enemy = new Bomber(); break;
        }

        return obj;
    }
}
