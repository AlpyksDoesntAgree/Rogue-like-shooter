using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _guns;
    [SerializeField] private Text _ammoText;
    [HideInInspector] public bool CanShoot = true;
    [HideInInspector] public bool CanShootForFireRate = true;
    private GameObject _currentWeapon;
    private Animator _anim;
    private IWeapon _weaponScript;
    public void Swap(int weaponIndex, IWeapon weapon)
    {
        _currentWeapon = _guns[weaponIndex];
        _weaponScript = weapon;

        for (int i = 0; i < _guns.Count; i++)
        {
            _guns[i].SetActive(i == weaponIndex);
        }

        _anim = _currentWeapon.GetComponent<Animator>();
        PlayTakeAnim();
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        if (_weaponScript != null)
            _ammoText.text = $"{_weaponScript.Ammo}|{_weaponScript.MaxAmmo}";
    }

    public void PlayAttackAnim()
    {
        _anim.Play("Shoot");
        StartCoroutine("WaitForFireRate");
    }
    public void PlayReloadAnim()
    {
        if (_weaponScript is Knife)
            return;

        _anim.Play("Reload");
        StartCoroutine("WaitForReload");
    }
    public void PlayTakeAnim()
    {
        _anim.Play("Take");
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForEndOfFrame();
        CanShoot = false;
        AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        CanShoot = true;
    }
    private IEnumerator WaitForFireRate()
    {
        CanShootForFireRate = false;
        yield return new WaitForSeconds(_weaponScript.FireRate);
        CanShootForFireRate = true;
    }
    private IEnumerator WaitForAttackAnimation()
    {
        float animationLength = _anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        _anim.Play("Idle");
    }
}