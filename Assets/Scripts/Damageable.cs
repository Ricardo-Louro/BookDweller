using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;

    private Damaging _incomingDamaging;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other is null) return;
        try
        {
            _incomingDamaging = other.GetComponent<Damaging>();
        }
        catch (Exception e)
        {
            Console.WriteLine("Colliding obj must be missing a Damaging Script");
            throw;
        }
    }
}
