using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DragonPlus.Config.Common;
using Framework;
using TMGame;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GuideComponent
{
    public InGameModeBase Mode { get; private set; }

    public GuideComponent(InGameModeBase mode)
    {
        Mode = mode;
    }

    public void Init()
    {

    }

    public void Dispose()
    {

    }
}
