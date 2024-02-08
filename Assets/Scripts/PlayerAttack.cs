using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float cooldown;
    private float lastTimeAttacked;
    [SerializeField] private float bulletScaleRatio;
    private Vector3 bulletScale = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        bulletScale.x = 0.5f;
        bulletScale.y = 0.5f;
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
        GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
        obj.transform.localScale = bulletScale;
    }

    public void UpgradeCooldown()
    {
        cooldown *= 0.5f;
    }
}
