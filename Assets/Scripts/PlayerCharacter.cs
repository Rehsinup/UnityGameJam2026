using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Lane Settings")]
    public float laneDistance = 1f;
    private int currentLane = 0;

    [Header("Mask Settings")]
    public int MaskIndex = 0;
    public Color[] playerMasks = new Color[2];  
    [SerializeField] private GameObject Player;
    private Material material;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer characterSprite; 
    public Sprite[] maskSprites = new Sprite[2];            

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    [Header("Input Keys")]
    public Key moveLeftKey;
    public Key moveRightKey;
    public Key skillKey; 
    public Key shootKey;

    [Header("TMP Texts")]
    public TMP_Text moveLeftText;
    public TMP_Text moveRightText;
    public TMP_Text swapMaskText;
    public TMP_Text shootText;

    [Header("UI Mask Objects")]
    public GameObject[] maskUIObjects = new GameObject[2];

    public static List<PlayerCharacter> AllPlayers = new List<PlayerCharacter>();

    public Color CurrentMaskColor => playerMasks[MaskIndex];

    void Awake() => AllPlayers.Add(this);
    void OnDestroy() => AllPlayers.Remove(this);

    void Start()
    {
        material = Player.GetComponent<Renderer>().material;
        material.SetColor("_BaseColor", CurrentMaskColor);

        if (characterSprite != null && maskSprites.Length > 0)
            characterSprite.sprite = maskSprites[MaskIndex];

        UpdateMaskUI();
        UpdateBindingsUI();
    }

    public void Move(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, -2, 2);
        transform.position = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);
    }

    public void SwapMask()
    {
        MaskIndex = (MaskIndex == 0) ? 1 : 0;

        if (material != null)
            material.SetColor("_BaseColor", CurrentMaskColor);

        if (characterSprite != null && maskSprites.Length > 1)
            characterSprite.sprite = maskSprites[MaskIndex];

        UpdateMaskUI();
    }

    public void Shoot()
    {
        if (!projectilePrefab) return;
        Vector3 spawnPos = transform.position + Vector3.up;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile p = proj.AddComponent<Projectile>();
        p.Init(CurrentMaskColor, projectileSpeed, true);
    }

    public void UpdateBindingsUI()
    {
        if (moveLeftText != null) moveLeftText.text = $"Left: {moveLeftKey}";
        if (moveRightText != null) moveRightText.text = $"Right: {moveRightKey}";
        if (swapMaskText != null) swapMaskText.text = $"Swap: {skillKey}";
        if (shootText != null) shootText.text = $"Shoot: {shootKey}";
    }

    private void UpdateMaskUI()
    {
        if (maskUIObjects == null || maskUIObjects.Length < 2)
            return;

        for (int i = 0; i < maskUIObjects.Length; i++)
        {
            if (maskUIObjects[i] != null)
                maskUIObjects[i].SetActive(i == MaskIndex);
        }
    }


}
