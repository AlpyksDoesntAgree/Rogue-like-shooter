using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMVC : MonoBehaviour
{
    [SerializeField] private WeaponView _weaponView;
    private WeaponController _weaponController;
    private Rifle _rifle;
    private Pistol _pistol;
    private Knife _knife;
    void Start()
    {
        Init();
        _weaponController.Start();
    }
    void Update()
    {
        _weaponController.Update();
    }
    private void Init()
    {
        _pistol = new Pistol();
        _rifle = new Rifle();
        _knife = new Knife();

        _weaponController = new WeaponController(_weaponView);
    }
}
