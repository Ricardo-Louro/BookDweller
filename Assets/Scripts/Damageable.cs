using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;
    [SerializeField] private float invulnerabilityDurationInSec;

    private Damaging _incomingDamaging;
    private bool _invulnerable;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            _incomingDamaging = other.GetComponent<Damaging>();
            if(!_invulnerable) GetHit(_incomingDamaging.DamagePerHit);
        }
        catch (Exception)
        {
            
        }
    }
    
    private void GetHit(int damageReceived)
    {
        healthPoints -= damageReceived;
        print($"{name} got damaged!");
        if (healthPoints <= 0)
        {
            print($"{name} is out of HP!");
            return;
        }
        StartInvulnerability();
    }

    private void StartInvulnerability()
    {
        if (!_invulnerable) StartCoroutine(Invulnerable());
    }

    private IEnumerator Invulnerable()
    {
        _invulnerable = true;
        
        for (float i = 0; i < invulnerabilityDurationInSec; i += 0.15f)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            
            yield return new WaitForSeconds(0.15f);
            
        _invulnerable = false;
        _spriteRenderer.enabled = true;
        
        }
    }
}
