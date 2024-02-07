using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private float invulnerabilityDurationInSec = 0.5f;
    [SerializeField] private float invulnerabilityBlinkInterval = 0.1f;

    private GameObject _collidingObject;
    private bool _invulnerable;
    private SpriteRenderer _spriteRenderer;
    
    private UISystem _uiSystem;
    private ScoreSystem _scoreSystem;
    private PlayerXP _xpSystem;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _xpSystem = FindObjectOfType<PlayerXP>();
        _scoreSystem = FindObjectOfType<ScoreSystem>();
        _uiSystem = FindObjectOfType<UISystem>();
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

        if (name == "Player")
        {
            _uiSystem.UpdateHPBar();
        }
        
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
        _uiSystem.UpdateScoreUI();
    }

    private void ComputeXp()
    {
        _xpSystem.GainXP(GetComponent<EnemyStats>().ScoreValue);
        _uiSystem.UpdateXPUI();
    }
}
