using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip fireballSound; 
    private Animator anim; 
    private PlayerMovement playerMovement;

    private float cooldownTimer = Mathf.Infinity;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer > attackCooldown && playerMovement.canAttack())
           Attack();


        cooldownTimer += Time.deltaTime;
    }


    private void Attack()
    {
        if(fireballSound != null)
            SoundManager.instance.PlaySound(fireballSound);
            
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireBalls[FindFireball()].transform.position = firePoint.position; 
        fireBalls[FindFireball()].GetComponent<Projectile>().SetDirection(MathF.Sign(transform.localScale.x));
    }

    private int FindFireball() {

        for (int i = 0; i < fireBalls.Length; i++) {
            if(!fireBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
