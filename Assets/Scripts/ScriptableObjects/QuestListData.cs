using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class QuestListData
{
  [SerializeField]
  string description;
  public string Description { get {return description; } set { description = value;} }
  
  [SerializeField]
  string questid;
  public string Questid { get {return questid; } set { questid = value;} }
  
  [SerializeField]
  // 0 - �� ����
  // >0 - ����, ��������� �� ����������� ������ ����������
  // <0 - �������� � ����������� �����������
  int result;
  public int Result { get {return result; } set { result = value;} }
  
}