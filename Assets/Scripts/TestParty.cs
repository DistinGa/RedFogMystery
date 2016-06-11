using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestParty
{
    public List<TestCharacter> Characters;

    // Use this for initialization
    public TestParty()
    {
        Characters = new List<TestCharacter>();
        Characters.Add(new TestCharacter("Геханд", "Воин", 7, 47, 113, 10, 17));
        Characters.Add(new TestCharacter("Гертруда", "Целитель", 5, 88, 89, 90, 90));
        Characters.Add(new TestCharacter("Арнольд", "Рыцарь", 2, 149, 150, 5, 10));
        Characters.Add(new TestCharacter("Себастиан", "Маг", 9, 45, 55, 150, 200));
    }
}
