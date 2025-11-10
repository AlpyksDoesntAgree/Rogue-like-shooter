using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyView : MonoBehaviour
{   
    [HideInInspector] public IEnemy Enemy;
    [HideInInspector] public EnemyController EnemyController = new EnemyController();
    private PlayerMVC _playerMVC;
    private PlayerController _playerController;
    private Animator _anim;
    private Transform _player;
    private bool _canAttack = true;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _playerMVC = FindAnyObjectByType<PlayerMVC>();
        _playerController = _playerMVC.Controller;
    }
    private void Update()
    {
        MoveToPlayer();    
    }
    public void PlayRunAnimation()
    {
        _anim.SetBool("isRunning", true);
    }
    public void PlayAttackAnimation()
    {
        if (Enemy is Bomber)
            return;

        _anim.SetBool("isRunning", true);
    }
    private void MoveToPlayer()
    {
        if (Vector3.Distance(transform.position, _player.position) > Enemy.AttackRange && _canAttack)
        {
            transform.position += Vector3.MoveTowards(transform.position, _player.position, Enemy.Speed);
            PlayRunAnimation();
        }
        else
        {
            AttackPlayer();

            if (Vector3.Distance(transform.position, _player.position) <= Enemy.AttackRange)
            {
                _playerController.DealDamage(Enemy.Damage);

                if (Enemy is Bomber)
                    Destroy(gameObject);
            }
        }
    }
    private void AttackPlayer()
    {
        StartCoroutine("AttackCD");
    }
    private IEnumerator AttackCD()
    {
        _canAttack = !_canAttack;
        PlayAttackAnimation();
        float animationLenght = _anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLenght);
        _canAttack = !_canAttack;
    }
}
