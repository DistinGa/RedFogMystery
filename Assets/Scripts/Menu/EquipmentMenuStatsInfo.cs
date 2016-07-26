using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentMenuStatsInfo : MonoBehaviour
{
    public Text HealthCur;
    public Text HealthNew;
    public Text ManaCur;
    public Text ManaNew;
    public Text AttackCur;
    public Text AttackNew;
    public Text DefenceCur;
    public Text DefenceNew;
    public Text MAttackCur;
    public Text MAttackNew;
    public Text MDefenceCur;
    public Text MDefenceNew;
    public Text AgilityCur;
    public Text AgilityNew;

    public void UpdateParams(HeroProperties curProperties, HeroProperties newProperties)
    {
        ResetColor();

        HealthCur.text = curProperties.Hp.ToString();
        HealthNew.text = newProperties.Hp.ToString();
        UpdateColor(HealthCur, HealthNew);

        ManaCur.text = curProperties.Mp.ToString();
        ManaNew.text = newProperties.Mp.ToString();
        UpdateColor(ManaCur, ManaNew);

        AttackCur.text = curProperties.Atk.ToString();
        AttackNew.text = newProperties.Atk.ToString();
        UpdateColor(AttackCur, AttackNew);

        DefenceCur.text = curProperties.Def.ToString();
        DefenceNew.text = newProperties.Def.ToString();
        UpdateColor(DefenceCur, DefenceNew);

        MAttackCur.text = curProperties.Mat.ToString();
        MAttackNew.text = newProperties.Mat.ToString();
        UpdateColor(MAttackCur, MAttackNew);

        MDefenceCur.text = curProperties.Mdf.ToString();
        MDefenceNew.text = newProperties.Mdf.ToString();
        UpdateColor(MDefenceCur, MDefenceNew);

        AgilityCur.text = curProperties.Agi.ToString();
        AgilityNew.text = newProperties.Agi.ToString();
        UpdateColor(AgilityCur, AgilityNew);
    }



    private void UpdateColor(Text _cur, Text _new)
    {
        if (int.Parse(_cur.text) > int.Parse(_new.text))
            _new.color = Color.red;
        if (int.Parse(_cur.text) < int.Parse(_new.text))
            _new.color = Color.green;
    }

    private void ResetColor()
    {
        HealthNew.color = Color.white;
        ManaNew.color = Color.white;
        AttackNew.color = Color.white;
        DefenceNew.color = Color.white;
        MAttackNew.color = Color.white;
        MDefenceNew.color = Color.white;
        AgilityNew.color = Color.white;
    }

}
