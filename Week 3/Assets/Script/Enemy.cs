using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource DeathSound;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        DeathSound = GetComponent<AudioSource>();
    }
    public void Death()
    {
        animator.SetTrigger("Death");
        DeathSound.Play();
    }

    private void Explode()
    {
        Destroy(this.gameObject);
    }
}
