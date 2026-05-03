using UnityEngine;

/// <summary>
/// Stores and manages player ability unlocks.
/// Can be extended with save/load later.
/// </summary>
public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance { get; private set; }

    [Header("Starting Unlocks")]
    [SerializeField] private bool doubleJumpUnlocked;
    [SerializeField] private bool fireballUnlocked;

    public bool DoubleJumpUnlocked => doubleJumpUnlocked;
    public bool FireballUnlocked => fireballUnlocked;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UnlockDoubleJump()
    {
        doubleJumpUnlocked = true;
        Debug.Log("Ability unlocked: Double Jump");
    }

    public void UnlockFireball()
    {
        fireballUnlocked = true;
        Debug.Log("Ability unlocked: Fireball (yes, from the back side)");
    }
}
