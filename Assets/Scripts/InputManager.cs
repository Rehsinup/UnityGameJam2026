using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamicInputManager : MonoBehaviour
{
    public List<Key> allKeys; 
    private Dictionary<string, Key> player1Bindings = new Dictionary<string, Key>();
    private Dictionary<string, Key> player2Bindings = new Dictionary<string, Key>();

    private Dictionary<Key, int> keyPressCount = new Dictionary<Key, int>();

    private string[] actions = { "MoveLeft", "MoveRight", "Shoot", "ChangeMask" };

    void Start()
    {
        player1Bindings = GenerateRandomBindings();
        player2Bindings = GenerateRandomBindings();

        foreach (var key in allKeys)
            keyPressCount[key] = 0;
    }

    void Update()
    {
        HandlePlayerInput(player1Bindings, 1);
        HandlePlayerInput(player2Bindings, 2);
    }

    private Dictionary<string, Key> GenerateRandomBindings()
    {
        var bindings = new Dictionary<string, Key>();
        List<Key> keysLeft = new List<Key>(allKeys);

        foreach (var action in actions)
        {
            int index = Random.Range(0, keysLeft.Count);
            bindings[action] = keysLeft[index];
            keysLeft.RemoveAt(index); 
        }
        return bindings;
    }

    private void HandlePlayerInput(Dictionary<string, Key> bindings, int playerID)
    {
        foreach (var action in actions)
        {
            Key key = bindings[action];
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                Debug.Log($"Player {playerID} {action} pressed on key {key}");

                keyPressCount[key]++;
                if (keyPressCount[key] >= 3)
                {
                    keyPressCount[key] = 0;

                    Key newKey;
                    do
                    {
                        newKey = allKeys[Random.Range(0, allKeys.Count)];
                    } while (bindings.ContainsValue(newKey)); 
                    bindings[action] = newKey;

                    Debug.Log($"Action {action} du Player {playerID} remappée à {newKey}");
                }
            }
        }
    }
}
