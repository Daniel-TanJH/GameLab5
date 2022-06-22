using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : Singleton<GameUI>
{
    public override void Awake()
    {
        base.Awake();
        Debug.Log("GameUI awake called");
    }
}
