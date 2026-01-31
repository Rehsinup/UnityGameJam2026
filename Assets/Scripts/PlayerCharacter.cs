using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public int PlayerId;

    public float laneDistance = 1f;
    private int currentLane = 0;

    [SerializeField] private GameObject Player;
    private Material material;

    private void Start()
    {
        material = Player.GetComponent<Renderer>().material;
    }


    public void ExecuteAction(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.Left:
                Move(-1);
                break;

            case PlayerAction.Right:
                Move(1);
                break;

            case PlayerAction.Shoot:
                Shoot();
                break;

            case PlayerAction.Skill:
                Skill();
                break;
        }
    }


    void Move(int dir)
    {
        currentLane += dir;
        currentLane = Mathf.Clamp(currentLane, -2, 2);

        transform.position = new Vector3(
            currentLane * laneDistance,
            transform.position.y,
            transform.position.z
        );
    }

    void Shoot()
    {
        Debug.Log($"P{PlayerId} SHOOT");
    }

    void Skill()
    {
        Debug.Log($"P{PlayerId} SKILL");
    }
}
