using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Stats stats;
    private ScoreSystem _scoreSystem;
    private PlayerXP _xpSystem;
    [SerializeField] private float invulnerabilityDurationInSec = 0.5f;
    [SerializeField] private float invulnerabilityBlinkInterval = 0.1f;

    private GameObject _collidingObject;
    private bool _invulnerable;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _xpSystem = FindObjectOfType<PlayerXP>();
        _scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            _collidingObject = other.gameObject;
            Damaging incomingDamaging = _collidingObject.GetComponent<Damaging>();
            if(!_invulnerable) GetHit(incomingDamaging.DamagePerHit);
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
            
            if (name != "Player")
            {
                ComputeScore();
                ComputeXp();
            }
            
            Destroy(gameObject);
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

    private void ComputeScore()
    {
        _scoreSystem.AddScore(GetComponent<EnemyStats>().ScoreValue);
    }

    private void ComputeXp()
    {
        _xpSystem.GainXP(GetComponent<EnemyStats>().ScoreValue);
    }
}
