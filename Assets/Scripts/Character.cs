using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int Health { get; set; }
    protected bool isDead = false;
    protected event EventHandler OnCharacterDying;
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            isDead = true;
            OnCharacterDying?.Invoke(this, EventArgs.Empty);
        }
    }
}
