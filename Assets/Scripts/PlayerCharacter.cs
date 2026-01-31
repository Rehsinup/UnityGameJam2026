using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Lane Settings")]
    public float laneDistance = 1f;
    private int currentLane = 0;

    [Header("Mask Settings")]
    public int MaskIndex;
    [SerializeField] private GameObject Player;
    private Material material;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    [Header("Input Settings")]
    public int playerID = 1;
    public List<Key> allKeys;

    private Dictionary<string, Key> actionBindings = new Dictionary<string, Key>();
    private Dictionary<Key, int> keyPressCount = new Dictionary<Key, int>();
    private string[] actions = { "MoveLeft", "MoveRight", "Mask1", "Mask2", "Mask3", "Mask4", "Shoot" };

    public static HashSet<Key> usedKeys = new HashSet<Key>();

    void Start()
    {
        material = Player.GetComponent<Renderer>().material;

        actionBindings = GenerateRandomBindings();

        foreach (var key in allKeys)
            keyPressCount[key] = 0;

        PrintBindings();
    }

    void Update()
    {
        HandleInput();
    }

    private Dictionary<string, Key> GenerateRandomBindings()
    {
        var bindings = new Dictionary<string, Key>();

        foreach (var action in actions)
        {
            Key newKey;
            do
            {
                newKey = allKeys[Random.Range(0, allKeys.Count)];
            } while (usedKeys.Contains(newKey));

            bindings[action] = newKey;
            usedKeys.Add(newKey);
        }

        return bindings;
    }

    private void HandleInput()
    {
        foreach (var action in actions)
        {
            Key key = actionBindings[action];
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                keyPressCount[key]++;
                if (keyPressCount[key] >= 3)
                {
                    keyPressCount[key] = 0;
                    RemapSingleAction(action);
                }

                switch (action)
                {
                    case "MoveLeft":
                        Move(-1);
                        break;
                    case "MoveRight":
                        Move(1);
                        break;
                    case "Mask1":
                        SetMask(0, Color.red);
                        break;
                    case "Mask2":
                        SetMask(1, Color.green);
                        break;
                    case "Mask3":
                        SetMask(2, Color.blue);
                        break;
                    case "Mask4":
                        SetMask(3, Color.yellow);
                        break;
                    case "Shoot":
                        Shoot();
                        break;
                }
            }
        }
    }

    private void Move(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, -2, 2);
        transform.position = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);
    }

    private void SetMask(int index, Color color)
    {
        MaskIndex = index;
        material.SetColor("_Base_color", color);
    }

    private void Shoot()
    {
        if (!projectilePrefab) return;

        Vector3 spawnPos = transform.position + Vector3.up; 
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Projectile p = proj.AddComponent<Projectile>();
        p.Init(material.color, projectileSpeed, true);

    }

    private void RemapSingleAction(string action)
    {
        Key oldKey = actionBindings[action];
        usedKeys.Remove(oldKey);

        Key newKey;
        do
        {
            newKey = allKeys[Random.Range(0, allKeys.Count)];
        } while (usedKeys.Contains(newKey));

        actionBindings[action] = newKey;
        usedKeys.Add(newKey);

        Debug.Log($"Player {playerID} action {action} remappée de {oldKey} à {newKey}");
        PrintBindings();
    }

    private void PrintBindings()
    {
        string bindingsStr = $"Player {playerID} bindings:";
        foreach (var kvp in actionBindings)
        {
            bindingsStr += $" {kvp.Key} -> {kvp.Value};";
        }
        Debug.Log(bindingsStr);
    }
}
