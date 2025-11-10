using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    float Damage { get; }
    float FireRate { get; }
    int Ammo { get; set; }
    int MaxAmmo { get; }
    float AttackRange { get; }
    float Spread { get; }

    void Attack();
    void Reload();

    public event Action onAmmoChange;
    public event Action onAttack;
    public event Action onReload;
    public event Action onLowAmmoAmount;
}
