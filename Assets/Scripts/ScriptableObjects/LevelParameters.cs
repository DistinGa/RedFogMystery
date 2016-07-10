using UnityEngine;

[CreateAssetMenu(fileName = "New LevelParameters", menuName = "Scriptable Objects/LevelParameters", order = 0)]
public class LevelParameters : ScriptableObject {
    public LevelProperties[] Levels;
}

[System.Serializable]
public class LevelProperties : Properties
{
    public int expToLevelUp;//опыт до следуюющего уровня
}
