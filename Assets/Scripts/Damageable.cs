using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private GameOver gameOver;
    private Spawner spawner;
    private ScoreSystem scoreSys;
    [SerializeField] private Stats stats;
    [SerializeField] private float invulnerabilityDurationInSec = 0.5f;
    [SerializeField] private float invulnerabilityBlinkInterval = 0.1f;
    [SerializeField] private PlayAudio HurtAudio;

    private GameObject _collidingObject;
    private bool _invulnerable;
    private SpriteRenderer _spriteRenderer;
    
    private UISystem _uiSystem;
    private ScoreSystem _scoreSystem;
    private PlayerXP _xpSystem;

    private void Start()
    {
        scoreSys = FindObjectOfType<ScoreSystem>();
        gameOver = FindObjectOfType<GameOver>();
        spawner = FindObjectOfType<Spawner>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _xpSystem = FindObjectOfType<PlayerXP>();
        _scoreSystem = FindObjectOfType<ScoreSystem>();
        _uiSystem = FindObjectOfType<UISystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _collidingObject = other.gameObject;
        Damaging incomingDamaging = _collidingObject.GetComponent<Damaging>();
        
        if (_invulnerable && incomingDamaging is null) return;
        
        GetHit(incomingDamaging.DamagePerHit);
        PlayAttackSound(incomingDamaging.gameObject);
    }
    
    private void GetHit(int damageReceived)
    {
        stats.HP -= damageReceived;

        if (name == "Player")
        {
            _uiSystem.UpdateHPBar();
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Hurt");
            
        }
        
        if(HurtAudio is not null) HurtAudio.Play();
        
        if (stats.HP <= 0)
        {
            
            if(tag is "Enemy")
            {
                ComputeScore();
                ComputeXp();
                spawner.DeductHordeValue((stats as EnemyStats).SpawnValue);
            }
            
            if(tag is "Player")
            {
                gameOver.EndGame("Main Menu", scoreSys.GetScore());
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

    private void PlayAttackSound(GameObject attacker)
    {
        var attackerAudio = attacker.GetComponentInChildren<PlayAudio>();
        if (attackerAudio is not null) attackerAudio.Play();
    }
}
