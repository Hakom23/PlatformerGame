using System.Collections;
using System.Collections.Generic;
using Platformer;
using UnityEngine;

public class CustomBehavior : MonoBehaviour
{
    protected GameManager _gameManager;
    public virtual void Init(GameManager gm)
    {
        _gameManager = gm;
    }
}
