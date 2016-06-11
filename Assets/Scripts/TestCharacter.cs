using UnityEngine;
using System.Collections;

public class TestCharacter
{
    public string Name { get; set; }
    public string Class { get; set; }
    public string Image { get; set; }
    public int Level { get; set; }
    public int CurHP { get; set; }
    public int MaxHP { get; set; }
    public int CurMP { get; set; }
    public int MaxMP { get; set; }


    public TestCharacter(string _name, string _class, int _level, int _curHP, int _maxHP, int _curMP, int _maxMP)
    {
        Name = _name;
        Class = _class;
        Level = _level;
        CurHP = _curHP;
        MaxHP = _maxHP;
        CurMP = _curMP;
        MaxMP = _maxMP;
    }
}