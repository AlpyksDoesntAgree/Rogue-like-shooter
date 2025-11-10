using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Image _targetImage;
    [HideInInspector] public Camera Camera;
    private Rigidbody _rb;
    

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Camera = Camera.main;
    }
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        _targetImage.fillAmount = currentHealth / maxHealth;
    }
    public void Move(Vector3 velocity)
    {
        _rb.velocity = velocity;
    }
}
