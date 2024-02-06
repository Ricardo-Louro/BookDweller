using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private int damagePerHit = 1;
    
    public int DamagePerHit { get; private set; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        DamagePerHit = damagePerHit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
