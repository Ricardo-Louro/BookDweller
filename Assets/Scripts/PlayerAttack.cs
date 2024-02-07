using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float cooldown;
    private float lastTimeAttacked;

    // Start is called before the first frame update
    private void Start()
    {
        ResetCooldown();
    }

    // Update is called once per frame
    private void Update()
    {
        if(CheckCooldown())
        {
            ResetCooldown();
            Attack();
        }
    }

    private bool CheckCooldown()
    {
        return cooldown < Time.time - lastTimeAttacked;
    }

    private void ResetCooldown()
    {
        lastTimeAttacked = Time.time;
    }

    private void Attack()
    {
        Instantiate(bullet, transform);
    }
}
