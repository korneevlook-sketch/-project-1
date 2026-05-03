using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class TongueAttack : MonoBehaviour
{
    [SerializeField] private float extendDistance = 5f;
    [SerializeField] private float extendDuration = 0.2f;
    [SerializeField] private float retractDuration = 0.2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask playerLayer;

    private Vector3 initialScale;
    private Vector3 initialPosition;
    private bool isAttacking;

    private void Awake()
    {
        initialScale = transform.localScale;
        initialPosition = transform.localPosition;
    }

    public void ExecuteAttack(Vector3 targetWorldPosition)
    {
        if (isAttacking) return;
        StartCoroutine(AttackRoutine(targetWorldPosition));
    }

    private IEnumerator AttackRoutine(Vector3 targetWorldPosition)
    {
        isAttacking = true;

        Vector3 direction = (targetWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        float timer = 0f;
        while (timer < extendDuration)
        {
            timer += Time.deltaTime;
            float t = timer / extendDuration;
            float scaleX = Mathf.Lerp(initialScale.x, initialScale.x + extendDistance, t);
            transform.localScale = new Vector3(scaleX, initialScale.y, initialScale.z);

            DealDamageIfHit();
            yield return null;
        }

        timer = 0f;
        while (timer < retractDuration)
        {
            timer += Time.deltaTime;
            float t = timer / retractDuration;
            float scaleX = Mathf.Lerp(initialScale.x + extendDistance, initialScale.x, t);
            transform.localScale = new Vector3(scaleX, initialScale.y, initialScale.z);
            yield return null;
        }

        transform.localScale = initialScale;
        transform.localPosition = initialPosition;
        isAttacking = false;
    }

    private void DealDamageIfHit()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().bounds.size, transform.eulerAngles.z, playerLayer);
        if (hit != null)
        {
            Debug.Log($"Player hit by tongue for {damage} damage.");
            // Integrate with PlayerHealth when added.
        }
    }
}
