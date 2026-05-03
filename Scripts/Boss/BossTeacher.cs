using UnityEngine;
using System.Collections;

public class BossTeacher : EnemyBase
{
    [Header("Boss Attack")]
    [SerializeField] private TongueAttack tongueAttack;
    [SerializeField] private Transform player;
    [SerializeField] private float phaseOneCooldown = 2.2f;
    [SerializeField] private float phaseTwoCooldown = 1.2f;
    [SerializeField] private float attackRange = 12f;

    [Header("Phase Threshold")]
    [SerializeField] private float phaseTwoHealthPercent = 0.5f;

    private bool inAttackLoop;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AttackLoop());
    }

    private IEnumerator AttackLoop()
    {
        inAttackLoop = true;

        while (inAttackLoop)
        {
            if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                tongueAttack.ExecuteAttack(player.position);
            }

            float cooldown = IsPhaseTwo() ? phaseTwoCooldown : phaseOneCooldown;
            yield return new WaitForSeconds(cooldown);
        }
    }

    private bool IsPhaseTwo()
    {
        return currentHealth <= Mathf.CeilToInt(maxHealth * phaseTwoHealthPercent);
    }

    protected override void Die()
    {
        inAttackLoop = false;
        StopAllCoroutines();
        Debug.Log("Boss defeated: Linguistics teacher has run out of arguments.");
        base.Die();
    }
}
