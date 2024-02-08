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
    [SerializeField] private float              bigCooldown;
    
    private float                               lastTimeBasicAttacked;
    private float                               lastTimeBigAttacked;
    private Vector3                             bigShotScale = Vector3.zero;
    [SerializeField] private float              bigShotScaleUpgradeRatio;
    [SerializeField] private float              basicCooldownUpgradeRatio;

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

        if(bigShotUnlock)
        {
            if(CheckBigCooldown())
            {
                ResetBigCooldown();
                BigAttack();
            }
        }
    }

    private bool CheckBasicCooldown()
    {
        return basicCooldown < Time.time - lastTimeBasicAttacked;
    }
    private bool CheckBigCooldown()
    {
        return bigCooldown < Time.time - lastTimeBigAttacked;
    }

    private void ResetBasicCooldown()
    {
        lastTimeBasicAttacked = Time.time;
    }

    private void ResetBigCooldown()
    {
        lastTimeBigAttacked = Time.time;
    }

    private void BasicAttack()
    {
        GameObject obj_basicShot = Instantiate(basicShot, transform.position, Quaternion.identity);
    }

    private void BigAttack()
    {
        GameObject obj_bigShot = Instantiate(bigShot, transform.position, Quaternion.identity);
        obj_bigShot.transform.localScale = bigShotScale;
    }

    public void UpgradeBasicCooldown()
    {
        basicCooldown *= basicCooldownUpgradeRatio;
    }

    public void UpgradeBigScale()
    {
        bigShotScale *= bigShotScaleUpgradeRatio;
    }

    public void UnlockBigShot()
    {
        bigShotUnlock = true;
    }
}
