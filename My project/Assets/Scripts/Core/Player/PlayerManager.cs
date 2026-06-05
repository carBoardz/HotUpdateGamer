using MySinleton;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool.MyAB;
using UnityEngine;

[XLua.LuaCallCSharp]
public class PlayerManager : SingletonMono<PlayerManager>
{
    public GameObject CurrentPlayer { get; private set; }
    public PlayerController CurrentController { get; private set; }

    void SpawnPlayer()
    {
        ABManager.Instance.LoadResAsync("player", "player", typeof(GameObject), (obj) =>
        {
            if (obj != null)
            {
                CurrentPlayer = Instantiate(obj as GameObject);
                CurrentController = CurrentPlayer.GetComponent<PlayerController>();

                EventCenter.Instance.Trigger("OnPlayerSpawned", CurrentPlayer.transform);
            }
        });
    }

    void Awake()
    {
        base.Awake();

        EventCenter.Instance.Register(
        "LoadPlayer",
        new Action(SpawnPlayer),
        owner: this,
        once: false
        );
    }
}