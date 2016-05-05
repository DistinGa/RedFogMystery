using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]

[Serializable] public class ChangeTrigger
{
  public int MyTrigger = 1;
  public Quest Quest = null;
  public int NewCurrentStep = 1;
}

public enum TriggerType
{
  Disabled,
  OnPressSpace,
  Automatic,
  Momental
}

public class TriggerBase : MonoBehaviour
{
  public TriggerType Type = TriggerType.OnPressSpace;
  [SerializeField] protected int currentStep = 1;
  [SerializeField] private string textAssetName = "Path";
  [SerializeField] protected ChangeTrigger[] changeTriggers = null;
  private string file = "";
  protected string[,] allBoxes;
  protected DialogPanel dialogPanel = null;
  protected int currentLine = 2;
  private bool hasStartSpeaking = false;
  private bool dialogFinished = false;
  private bool canSpeak = false;
  protected CharacterMoving characterMoving = null;
  protected int[] triggerNumLines = new int[1];//номера строк с которых начинаются триггеры в .csv таблице
  
  public Action<int> OnTriggerAction;
  protected bool isCharacterRight = false;  

  public int CurrentStep
  {
    get { return currentStep;}
    set
    {
      currentStep = value;
      currentLine = triggerNumLines[currentStep];
    }
  }

  protected virtual void Start()
  {
    dialogPanel = FindObjectOfType<DialogPanel>();
    file = Application.dataPath + "/StreamingAssets/" + textAssetName + ".csv";
    if (System.IO.File.Exists(file))
    {
      WriteAllBoxes();
      SetTriggers();
    }
    else
    {
      Debug.LogWarning("File:" + file + "do not found!");
    }
  }

  private void WriteAllBoxes()
  {
    string[] lines = System.IO.File.ReadAllLines(file);
    allBoxes = new string[5, lines.Length + 1];//
    for (var lineNum = 0; lineNum < lines.Length; lineNum++)
    {
      string currentBoxText = "";
      int stolb = 0;
      for (var symbNum = 0; symbNum < lines[lineNum].Length; symbNum++)
      {
        string currentSymbol = lines[lineNum].Substring(symbNum, 1);
        if (currentSymbol == ";")
        {
          ///Запись ячейки
          allBoxes[stolb, lineNum + 1] = currentBoxText;
          currentBoxText = "";
          stolb += 1;
        }
        else
        {
          currentBoxText += currentSymbol;//Запоминаем текст ячейки
          if (symbNum == lines[lineNum].Length - 1)//Последний символ строки
            allBoxes[stolb, lineNum + 1] = currentBoxText;//Запись крайнего стобца
        }
      }
    }
  }

  private void SetTriggers()
  {
    for (int i = 1; i < allBoxes.GetLength(1) - 1; i++)
    {
      if (allBoxes[0, i].Length > 1)
      {
        Array.Resize(ref triggerNumLines, triggerNumLines.Length + 1);
        triggerNumLines[triggerNumLines.Length - 1] = i + 1;
      }
    }
    Array.Resize(ref triggerNumLines, triggerNumLines.Length + 1);
    triggerNumLines[triggerNumLines.Length - 1] = allBoxes.GetLength(1) + 1;
  }

  protected virtual void SetDialog(int line)
  {
    dialogPanel.MainText.text = allBoxes[3 + dialogPanel.CurrentLanguage, line];    
  }

  protected virtual void Update()
  {
    if (Input.GetKeyUp(KeyCode.Space) && Type != TriggerType.Disabled)
    {
      if (hasStartSpeaking)
      {
        currentLine += 1;
        if (currentLine == allBoxes.GetLength(1) || currentLine == triggerNumLines[currentStep + 1] - 1)//
        {
          EndDialog(); 
        }
        else
        {
          SetDialog(currentLine);
        }
      }
      if (canSpeak && !dialogFinished && !hasStartSpeaking)
        StartDialog();
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<CharacterMoving>() != null)
    {
      characterMoving = other.GetComponent<CharacterMoving>();
      isCharacterRight = other.transform.position.x > transform.position.x;
      OnCharacterTriggerEnter();
      if (!hasStartSpeaking && !dialogFinished)
      {
        canSpeak = true;
        if (Type == TriggerType.Automatic)
          StartDialog();
        if (Type == TriggerType.Momental)
          EndDialog();
      }    
    }    
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.GetComponent<CharacterMoving>() != null)
    {
      canSpeak = false;
      dialogFinished = false;
      OnCharacterTriggerExit();
    }
  }

  public virtual void StartDialog()
  {    
    hasStartSpeaking = true;    
  }

  public virtual void EndDialog()
  {    
    hasStartSpeaking = false;
    dialogFinished = true;    
    foreach (var changeTrigger in changeTriggers)
    {
      if (changeTrigger.MyTrigger == CurrentStep)
      {
        changeTrigger.Quest.CurrentStep = changeTrigger.NewCurrentStep;        
        break;
      }
    }    
  }

  protected virtual void OnCharacterTriggerEnter()
  {
  }

  protected virtual void OnCharacterTriggerExit()
  {
  }    
}
