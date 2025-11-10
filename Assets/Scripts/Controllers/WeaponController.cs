using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController
{
    private static int _currentIndex = 0;

    private WeaponView _view;
    private IWeapon _weapon;
    private int _weaponIndex;
    private bool _canShoot = true;
    private Camera _camera;

    private Rifle _rifle = new Rifle();
    private Pistol _pistol = new Pistol();
    private Knife _knife = new Knife();

    public WeaponController(WeaponView view)
    {
        _view = view;
        _weaponIndex = Mathf.Clamp(_currentIndex, 0, 2);
        AddEvent();
    }

    public void Start()
    {
        _camera = Camera.main;
        _view.Swap(_weaponIndex, _weapon);
    }

    public void Update()
    {
        SwapIndex();
        ReloadInput();
        AttackInput();

        _canShoot = _view.CanShoot && _view.CanShootForFireRate;
    }

    //Swap
    private void SwapIndex()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwapWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwapWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwapWeapon(2);

        var scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scroll < 0)
            ScrollWeapon(-1);
        if (scroll > 0)
            ScrollWeapon(1);
    }
    private void ScrollWeapon(int direction)
    {
        int newIndex = _weaponIndex + direction;

        if (newIndex > 2)
            newIndex = 0;
        else if (newIndex < 0)
            newIndex = 2;

        SwapWeapon(newIndex);
    }
    private void SwapWeapon(int index)
    {
        if (index == _weaponIndex) return;

        RemoveEvent();

        _weaponIndex = index;
        _currentIndex = _weaponIndex;

        AddEvent();

        _view.Swap(_weaponIndex, _weapon);
    }

    //Attack
    public void Attack()
    {
        Vector3 rayOrigin = _camera.transform.position;
        Vector3 rayDirection = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        Vector3 cameraUp = _camera.transform.up;
        Vector3 targetPoint = rayOrigin + rayDirection * 75f;

        float randomX = Random.Range(-_weapon.Spread, _weapon.Spread);
        float randomY = Random.Range(-_weapon.Spread, _weapon.Spread);

        Vector3 newDirection = (targetPoint +
            cameraRight * randomX +
            cameraUp * randomY - rayOrigin);

        if (_weapon is Knife)
            newDirection.Normalize();

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, newDirection, out hit, _weapon.AttackRange))
        {
            RaycastHitEnemy(hit);
        }
        Debug.DrawRay(rayOrigin, newDirection, Color.red, 5f);
    }
    private void RaycastHitEnemy(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<EnemyView>().Enemy.DealDamage(_weapon.Damage);
        }
    }
    private void AttackInput()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _canShoot)
        {
            _weapon.Attack();
        }
    }

    //Reload
    private void ReloadInput()
    {
        if (_weaponIndex == 2)
            return;

        if (Input.GetKeyDown(KeyCode.R))
            _weapon.Reload();
    }

    private void AddEvent()
    {
        switch (_weaponIndex)
        {
            case 0: _weapon = _rifle; break;
            case 1: _weapon = _pistol; break;
            case 2: _weapon = _knife; break;
        }

        _weapon.onAmmoChange += _view.UpdateAmmoUI;
        _weapon.onAttack += _view.PlayAttackAnim;
        _weapon.onAttack += Attack;
        _weapon.onReload += _view.PlayReloadAnim;
    }
    private void RemoveEvent()
    {
        _weapon.onAmmoChange -= _view.UpdateAmmoUI;
        _weapon.onAttack -= _view.PlayAttackAnim;
        _weapon.onAttack -= Attack;
        _weapon.onReload -= _view.PlayReloadAnim;
    }
}