using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private float invulnerabilityDurationInSec = 0.5f;
    [SerializeField] private float invulnerabilityBlinkInterval = 0.1f;

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
        stats.HP -= damageReceived;
        print($"{name} got damaged! Current HP: {stats.HP}");
        if (stats.HP <= 0)
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
        
        for (float i = 0; i < invulnerabilityDurationInSec; i += invulnerabilityBlinkInterval)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            
            yield return new WaitForSeconds(invulnerabilityBlinkInterval);
        }
        
        _invulnerable = false;
        _spriteRenderer.enabled = true;
    }
}
