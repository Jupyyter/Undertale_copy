using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//because of this, i can set variable directly within the editor
public class BattleButton
{
    public GameObject instance;

    public Sprite spriteActive;

    public Sprite spriteInactive;

    public Context context;
}
