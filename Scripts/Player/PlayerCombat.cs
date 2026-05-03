using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Melee")]
    [SerializeField] private Transform meleePoint;
    [SerializeField] private float meleeRange = 0.8f;
    [SerializeField] private int meleeDamage = 1;
    [SerializeField] private float meleeCooldown = 0.45f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Fireball")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireballCooldown = 1.0f;

    private float meleeTimer;
    private float fireballTimer;

    private void Update()
    {
        meleeTimer -= Time.deltaTime;
        fireballTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J) && meleeTimer <= 0f)
        {
            DoMeleeAttack();
        }

        if (Input.GetKeyDown(KeyCode.K) && fireballTimer <= 0f)
        {
            TryShootFireball();
        }
    }

    private void DoMeleeAttack()
    {
        meleeTimer = meleeCooldown;

        Collider2D[] hits = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(meleeDamage);
            }
        }
    }

    private void TryShootFireball()
    {
        if (AbilityManager.Instance == null || !AbilityManager.Instance.FireballUnlocked)
        {
            Debug.Log("Fireball locked: attend more questionable master classes.");
            return;
        }

        fireballTimer = fireballCooldown;
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (meleePoint == null) return;
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}
