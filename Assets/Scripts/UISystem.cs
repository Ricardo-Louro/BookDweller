using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    private GameObject                      playerInstance;
    [SerializeField] private Slider         XPBar;
    [SerializeField] private TMP_Text       LVLCounter;
    [SerializeField] private TMP_Text       Score;
    [SerializeField] private Slider         HPBar;

    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private Slider BasicShotSlider;
    [SerializeField] private Slider BigShotSlider;

    private ScoreSystem                     _scoreSystem;
    private PlayerXP                        _playerXp;
    private PlayerStats                     _playerStats;

    private void Start()
    {
        playerInstance = FindObjectOfType<PlayerMovement>().gameObject;
        _scoreSystem = FindObjectOfType<ScoreSystem>();
        
        _playerXp = playerInstance.GetComponent<PlayerXP>();
        _playerStats = playerInstance.GetComponent<PlayerStats>();
    }

    public void UpdateXPUI()
    {
        XPBar.maxValue = _playerXp.ExperienceGoal;
        XPBar.value = _playerXp.CurrentXp;

        LVLCounter.text = $"LVL {_playerStats.LVL}";
    }

    public void UpdateScoreUI()
    {
        Score.text = $"Score: {_scoreSystem.GetScore()}";
    }

    public void UpdateHPBar()
    {
        HPBar.value = _playerStats.HP;
        HPBar.maxValue = _playerStats.MaxHP;
    }
}
