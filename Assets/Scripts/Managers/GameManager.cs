using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        // Initialize game state here
    }

    private void Start()
    {
        Debug.Log("GameManager Initialized");
    }
}
