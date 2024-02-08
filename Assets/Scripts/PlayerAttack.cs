using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject         basicShot;
    private bool                                bigShotUnlock;
    [SerializeField] private GameObject         bigShot;
    [SerializeField] private float              basicCooldown;
    private float                               lastTimeBasicAttacked;
    private Vector3                             bigShotScale = Vector3.zero;
    [SerializeField] private float              cooldownUpgradeRatio;

    // Start is called before the first frame update
    private void Start()
    {
        bigShotUnlock = false;
        bigShotScale.x = 0.5f;
        bigShotScale.y = 0.5f;
        ResetBasicCooldown();
    }

    // Update is called once per frame
    private void Update()
    {
        if(CheckBasicCooldown())
        {
            ResetBasicCooldown();
            BasicAttack();
        }

        i
    }

    private bool CheckBasicCooldown()
    {
        return basicCooldown < Time.time - lastTimeBasicAttacked;
    }

    private void ResetBasicCooldown()
    {
        lastTimeBasicAttacked = Time.time;
    }

    private void BasicAttack()
    {
        GameObject obj_basicShot = Instantiate(basicShot, transform.position, Quaternion.identity);
    }

    public void UpgradeBasicCooldown()
    {
        basicCooldown *= cooldownUpgradeRatio;
    }
}
