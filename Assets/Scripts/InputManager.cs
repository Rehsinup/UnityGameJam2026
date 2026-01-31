using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class UnstableInputManager : MonoBehaviour
{
    public static UnstableInputManager Instance;

    [Header("Keyboard pool (42 touches)")]
    public List<Key> keyboardPool = new List<Key>();

    private List<UnstableKey> activeKeys = new List<UnstableKey>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitPlayer(1);
        InitPlayer(2);
    }

    private void Update()
    {
        foreach (var unstableKey in activeKeys.ToList())
        {
            if (Keyboard.current[unstableKey.key].wasPressedThisFrame)
            {
                TriggerKey(unstableKey);
            }
        }
    }


    void InitPlayer(int playerId)
    {
        AssignNewKey(PlayerAction.Left, playerId);
        AssignNewKey(PlayerAction.Right, playerId);
        AssignNewKey(PlayerAction.Shoot, playerId);
        AssignNewKey(PlayerAction.Skill, playerId);
    }


    void AssignNewKey(PlayerAction action, int playerId)
    {
        List<Key> freeKeys = keyboardPool
            .Where(k => !activeKeys.Any(a => a.key == k))
            .ToList();

        if (freeKeys.Count == 0)
        {
            Debug.LogWarning("Plus aucune touche libre !");
            return;
        }

        Key newKey = freeKeys[Random.Range(0, freeKeys.Count)];

        activeKeys.Add(new UnstableKey
        {
            key = newKey,
            action = action,
            playerId = playerId,
            remainingUses = Random.Range(3, 7)
        });

        Debug.Log($"[P{playerId}] {action} ? {newKey} ({activeKeys.Last().remainingUses} uses)");
    }


    void TriggerKey(UnstableKey unstableKey)
    {
        ExecuteAction(unstableKey.playerId, unstableKey.action);

        unstableKey.remainingUses--;

        if (unstableKey.remainingUses <= 0)
        {
            activeKeys.Remove(unstableKey);
            AssignNewKey(unstableKey.action, unstableKey.playerId);
        }
    }

    // ---------------- PLAYER LINK ----------------

    void ExecuteAction(int playerId, PlayerAction action)
    {
        PlayerCharacter player = FindObjectsOfType<PlayerCharacter>()
            .First(p => p.PlayerId == playerId);

        player.ExecuteAction(action);
    }
}
