using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DynamicInputManager : MonoBehaviour
{
    [Header("Players")]
    public PlayerCharacter player1;
    public PlayerCharacter player2;

    [Header("All possible keys")]
    public List<Key> allKeys;

    [Header("TMP Texts")]
    public TMP_Text[] player1Texts; // Left, Right, Swap, Shoot
    public TMP_Text[] player2Texts;

    private Dictionary<string, Key> player1Bindings = new Dictionary<string, Key>();
    private Dictionary<string, Key> player2Bindings = new Dictionary<string, Key>();

    private Dictionary<Key, int> keyPressCount = new Dictionary<Key, int>();

    private string[] actions = { "MoveLeft", "MoveRight", "SwapMask", "Shoot" };

    void Start()
    {
       
        List<Key> usedKeys = new List<Key>();

        player1Bindings = GenerateRandomBindings(usedKeys);
        usedKeys.AddRange(player1Bindings.Values); 

        player2Bindings = GenerateRandomBindings(usedKeys);

        foreach (var key in allKeys)
            keyPressCount[key] = 0;

        AssignBindings(player1, player1Bindings, player1Texts);
        AssignBindings(player2, player2Bindings, player2Texts);
    }

    void Update()
    {
        HandlePlayerInput(player1Bindings, player1, player1Texts);
        HandlePlayerInput(player2Bindings, player2, player2Texts);
    }

    private Dictionary<string, Key> GenerateRandomBindings(List<Key> usedKeys)
    {
        var bindings = new Dictionary<string, Key>();
        List<Key> keysLeft = new List<Key>(allKeys);

        foreach (var k in usedKeys)
            keysLeft.Remove(k);

        foreach (var action in actions)
        {
            int index = Random.Range(0, keysLeft.Count);
            bindings[action] = keysLeft[index];
            keysLeft.RemoveAt(index); 
        }

        return bindings;
    }

    private void AssignBindings(PlayerCharacter player, Dictionary<string, Key> bindings, TMP_Text[] texts)
    {
        player.moveLeftKey = bindings["MoveLeft"];
        player.moveRightKey = bindings["MoveRight"];
        player.skillKey = bindings["SwapMask"];
        player.shootKey = bindings["Shoot"];
        player.UpdateBindingsUI();
    }

    private void HandlePlayerInput(Dictionary<string, Key> bindings, PlayerCharacter player, TMP_Text[] texts)
    {
        foreach (var action in actions)
        {
            Key key = bindings[action];

            if (Keyboard.current[key].wasPressedThisFrame)
            {
                keyPressCount[key]++;

                if (keyPressCount[key] >= 3)
                {
                    keyPressCount[key] = 0;
                    Key newKey;
                    do
                    {
                        newKey = allKeys[Random.Range(0, allKeys.Count)];
                    }
                    while (bindings.ContainsValue(newKey) ||
                          (player == player1 && player2Bindings.ContainsValue(newKey)) ||
                          (player == player2 && player1Bindings.ContainsValue(newKey)));

                    bindings[action] = newKey;
                    AssignBindings(player, bindings, texts);
                    Debug.Log($"Action {action} de {player.name} remappée à {newKey}");
                }

                switch (action)
                {
                    case "MoveLeft": player.Move(-1); break;
                    case "MoveRight": player.Move(1); break;
                    case "SwapMask": player.SwapMask(); break;
                    case "Shoot": player.Shoot(); break;
                }
            }
        }
    }
}
