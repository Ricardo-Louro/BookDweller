using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject         basicShot;
    private bool                                bigShotUnlock;
    [SerializeField] private GameObject         bigShot;
    private bool waterAoeUnlock;
    [SerializeField] private GameObject waterAoe;
    [SerializeField] private float              basicCooldown;
    private int basicLevel = 1;
    private int aoeLevel = 2;
    [SerializeField] private float              bigCooldown;
    [SerializeField] private float waterAoeCooldown;
    
    private float                               lastTimeBasicAttacked;
    private float                               lastTimeBigAttacked;
    private float lastTimeWaterAoeAttacked;
    private Vector3                             bigShotScale = Vector3.zero;
    private Vector3 waterAoeScale = Vector3.one;
    [SerializeField] private float              bigShotScaleUpgradeAmount;
    [SerializeField] private PlayAudio bigShotAudio;
    [SerializeField] private PlayAudio basicShotAudio;
    [SerializeField] private PlayAudio waterAoeAudio;

    // Start is called before the first frame update
    private void Start()
    {
        bigShotUnlock = false;
        waterAoeUnlock = false;
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

        if (waterAoeUnlock)
        {
            if(CheckAoeCooldown())
            {
                ResetAoeCooldown();
                AoeAttack();
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

    private bool CheckAoeCooldown()
    {
        return waterAoeCooldown < Time.time - lastTimeWaterAoeAttacked;
    }

    private void ResetBasicCooldown()
    {
        lastTimeBasicAttacked = Time.time;
    }

    private void ResetBigCooldown()
    {
        lastTimeBigAttacked = Time.time;
    }

    private void ResetAoeCooldown()
    {
        lastTimeWaterAoeAttacked = Time.time;
    }

    private void BasicAttack()
    {
        GameObject obj_basicShot = Instantiate(basicShot, transform.position, Quaternion.identity);
        basicShotAudio.Play();
    }

    private void BigAttack()
    {
        GameObject obj_bigShot = Instantiate(bigShot, transform.position, Quaternion.identity);
        obj_bigShot.transform.localScale = bigShotScale;
        bigShotAudio.Play();
    }

    private void AoeAttack()
    {
        GameObject obj_WaterAoe = Instantiate(waterAoe, transform.position, Quaternion.identity);
        obj_WaterAoe.transform.localScale = waterAoeScale;
        waterAoeAudio.Play();
    }

    public void UpgradeBasicCooldown()
    {
        basicLevel++;
        basicCooldown = 1f / basicLevel;
    }

    public void UpgradeBigScale()
    {
        bigShotScale += Vector3.one * bigShotScaleUpgradeAmount;
    }

    public void UpgradeAoe()
    {
        aoeLevel++;
        waterAoeCooldown = 5f * (1 - 0.05f * aoeLevel);

    }

    public void UnlockBigShot()
    {
        bigShotUnlock = true;
    }

    public void UnlockWaterAoe()
    {
        waterAoeUnlock = true;
    }
}
