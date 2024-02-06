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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
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
        if (healthPoints < 0)
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
        yield return new WaitForSeconds(invulnerabilityDurationInSec);
        _invulnerable = false;
    }
}
