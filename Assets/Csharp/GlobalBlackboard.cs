using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFramework.Runtime;
using GameTable;

public class GlobalBlackboard : MonoSingleton<GlobalBlackboard>
{
    public ShopTable shopTable;

    public Transform canvas;
}
