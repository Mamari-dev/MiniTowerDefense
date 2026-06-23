using System;

[Flags]
public enum TowerAttackTypes
{
    Health = 1 << 0,
    Speed = 1 << 1,
    Armored = 1 << 2,
    Shield = 1 << 3,
}
