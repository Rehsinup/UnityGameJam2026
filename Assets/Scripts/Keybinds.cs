using UnityEngine.InputSystem;

[System.Serializable]
public class UnstableKey
{
    public Key key;          
    public int remainingUses;    
    public PlayerAction action;     
    public int playerId;        
}
