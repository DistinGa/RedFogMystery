using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;
using System.IO;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Collections.Generic;

[Serializable] public class Atribut
{
  public string Name = "";
  public string Value = "";
}

public class SaveController : MonoBehaviour
{
  [SerializeField] private int firstScene = 1;
  [SerializeField] private Button loadButton = null;
  private string sceneName = "";  
  private List<Atribut> atributs = new List<Atribut>();

  private void Start()
  {    
    DontDestroyOnLoad(this);
    string filePath = Application.dataPath + "/StreamingAssets/Saves/LastScene.xml";
    loadButton.interactable = File.Exists(filePath);    
  }

  private void OnLevelWasLoaded(int level)
  {
    sceneName = SceneManager.GetActiveScene().name;
    string filePath = Application.dataPath + "/StreamingAssets/Saves/" + sceneName + "Data.xml";
    if (File.Exists(filePath))
      Invoke("LoadScene", 0);
    Invoke("LoadHeroesParams", 0);
  }

  public void SaveScene()
  {   
    XmlDocument doc = new XmlDocument();
    XmlNode rootNode = null;
    string filePath = Application.dataPath + "/StreamingAssets/Saves/" + sceneName + "Data.xml";
    if (!File.Exists(filePath))
    {      
      rootNode = doc.CreateElement("Triggers");      
      doc.AppendChild(rootNode);
    }
    else
    {
      doc.Load(filePath);
      rootNode = doc.DocumentElement;
    }
    rootNode.RemoveAll();
    //Save Triggers
    TriggerBase[] triggersBase = FindObjectsOfType<TriggerBase>();
    foreach (var triggerBase in triggersBase)
    {
      AddAtribute("trigger", (triggerBase.CurrentStep).ToString());
      string triggerType = triggerBase.Type.ToString();
      AddAtribute("type", triggerType);
      AddXmlElements(doc, rootNode, "Trigger" + triggerBase.gameObject.name);
    }    
    //Save Character Position    
    GameObject[] savePositionOjects = GameObject.FindGameObjectsWithTag("SavePosition");
    foreach (var savePositionOject in savePositionOjects)
    {
      Vector3 pos = savePositionOject.transform.position;
      AddAtribute("positionX", (pos.x).ToString());
      AddAtribute("positionY", (pos.y).ToString());
      AddXmlElements(doc, rootNode, "SavePosition" + savePositionOject.name);
    }
    //Save background map
    CameraController cameraController = FindObjectOfType<CameraController>();
    string mapName = cameraController.Map.name;
    AddAtribute("MapName", mapName);
    AddXmlElements(doc, rootNode, "MapBackgroundName");
    //
    doc.Save(filePath);

    //  Save Hero Params
    doc = new XmlDocument();
    rootNode = null;
    filePath = Application.dataPath + "/StreamingAssets/Saves/Heroes.xml";
    if (!File.Exists(filePath))
    {
      rootNode = doc.CreateElement("HeroesData");
      doc.AppendChild(rootNode);
    }
    else
    {
      doc.Load(filePath);
      rootNode = doc.DocumentElement;
    }
    rootNode.RemoveAll();
    Hero[] heroes = GameManager.GM.Heroes;
    foreach (var hero in heroes)
    {
      AddAtribute("Hp", (hero.HeroPropetries.Hp).ToString());//!!!!!!!!!!!!!!!!!!!!
      AddAtribute("Mhp", (hero.HeroPropetries.Mhp).ToString());
      AddAtribute("Mp", (hero.HeroPropetries.Mp).ToString());
      AddAtribute("Mmp", (hero.HeroPropetries.Mmp).ToString());
      AddAtribute("Co", (hero.HeroPropetries.Cr).ToString());
      AddAtribute("Mco", (hero.HeroPropetries.Mcr).ToString());
      AddAtribute("Agi", (hero.HeroPropetries.Agi).ToString());
      AddXmlElements(doc, rootNode, "Hero" + hero.HeroPropetries.Name);
    }
    //// Save vagons
    //Party party = FindObjectOfType<Party>();
    //foreach (var vagon in party.Vagons)
    //{
    //  AddAtribute("vagon", vagon.gameObject.name);
    //  AddXmlElements(doc, rootNode, "Vagon");
    //}
    //AddAtribute("gold", party.Gold.ToString());
    //AddXmlElements(doc, rootNode, "Gold");
    //AddAtribute("timeGame", party.TimeGame.ToString());
    //AddXmlElements(doc, rootNode, "TimeGame");
    //// 
    //doc.Save(filePath);
    //Save Inventar    
    doc = new XmlDocument();
    rootNode = null;
    filePath = Application.dataPath + "/StreamingAssets/Saves/InventarData.xml";
    if (!File.Exists(filePath))
    {
      rootNode = doc.CreateElement("InventarData");
      doc.AppendChild(rootNode);
    }
    else
    {
      doc.Load(filePath);
      rootNode = doc.DocumentElement;
    }
    rootNode.RemoveAll();
    Inventar inventar = FindObjectOfType<Inventar>();
    foreach (var itemGroup in inventar.ItemGroups)
    {
      foreach (var itemButton in itemGroup.itemButtons)
      {
        if (itemButton.ThingPropetries.Name != "" && itemButton.ThingPropetries.Name != "Name")
        {
          AddAtribute("Name", (itemButton.ThingPropetries.Name).ToString());
          AddAtribute("Count", (itemButton.ThingPropetries.Count).ToString());
          AddXmlElements(doc, rootNode, "Item");
        }
      }      
    }     
    doc.Save(filePath);
    //
  }

  private void AddXmlElements(XmlDocument d, XmlNode node, string name)
  {    
    XmlElement e = d.CreateElement(name);
    foreach (var atribut in atributs)
    {
      XmlAttribute atr = d.CreateAttribute(atribut.Name);
      atr.Value = atribut.Value;
      e.SetAttributeNode(atr);
    }    
    node.AppendChild(e);
    atributs.Clear();
  }

  private void AddAtribute(string name, string value)
  {
    Atribut newAtr = new Atribut();
    newAtr.Name = name;
    newAtr.Value = value;
    atributs.Add(newAtr);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S))
    {
      SaveScene();
    }
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadScene();
    }
  }

  public void LoadScene()
  {    
    XmlDocument doc = new XmlDocument();
    XmlNodeList elemList;
    string filePath = Application.dataPath + "/StreamingAssets/Saves/" + sceneName + "Data.xml";
    doc.Load(filePath);
    //LoadTriggers
    TriggerBase[] triggersBase = FindObjectsOfType<TriggerBase>();
    foreach (var triggerBase in triggersBase)
    {
      triggerBase.gameObject.SetActive(false);
      elemList = doc.GetElementsByTagName("Trigger" + triggerBase.gameObject.name);
      for (int i = 0; i < elemList.Count; i++)
      {
        triggerBase.gameObject.SetActive(true);
        triggerBase.CurrentStep = Int32.Parse(elemList[i].Attributes["trigger"].Value);

        if (elemList[i].Attributes["type"].Value == "Disabled")
          triggerBase.Type = TriggerType.Disabled;
        if (elemList[i].Attributes["type"].Value == "OnPressSpace")
          triggerBase.Type = TriggerType.OnPressSpace;
        if (elemList[i].Attributes["type"].Value == "Automatic")
          triggerBase.Type = TriggerType.Automatic;
        if (elemList[i].Attributes["type"].Value == "Momental")
          triggerBase.Type = TriggerType.Momental;
      }
    }    
    //Load Save Position Objects position
    GameObject[] savePositionOjects = GameObject.FindGameObjectsWithTag("SavePosition");
    foreach (var savePositionOject in savePositionOjects)
    {
      elemList = doc.GetElementsByTagName("SavePosition" + savePositionOject.name);
      for (int i = 0; i < elemList.Count; i++)
      {
        float x = Convert.ToSingle(elemList[i].Attributes["positionX"].Value, new CultureInfo("en-US"));
        float y = Convert.ToSingle(elemList[i].Attributes["positionY"].Value, new CultureInfo("en-US"));
        savePositionOject.transform.position = new Vector3(x, y, 0);
      }
    }
    //Load Map Background
    CameraController cameraController = FindObjectOfType<CameraController>();
    elemList = doc.GetElementsByTagName("MapBackgroundName");
    for (int i = 0; i < elemList.Count; i++)
    {
      string mapName = elemList[i].Attributes["MapName"].Value;
      cameraController.Map = GameObject.Find(mapName);
      cameraController.TuneMap(cameraController.Map);
    }      
  }

  private void LoadHeroesParams()
  {
    string filePath = Application.dataPath + "/StreamingAssets/Saves/Heroes.xml";
    XmlDocument doc = new XmlDocument();
    if (File.Exists(filePath))
    {      
      XmlNodeList elemList = null;
      doc.Load(filePath);
      Hero[] heroes = GameManager.GM.Heroes;
      foreach (var hero in heroes)
      {
        elemList = doc.GetElementsByTagName("Hero" + hero.HeroPropetries.Name);
        for (int i = 0; i < elemList.Count; i++)
        {
          hero.HeroPropetries.Hp = Convert.ToSingle(elemList[i].Attributes["Hp"].Value, new CultureInfo("en-US"));
          hero.HeroPropetries.Mhp = Convert.ToSingle(elemList[i].Attributes["Mhp"].Value, new CultureInfo("en-US"));
          hero.HeroPropetries.Mp = Convert.ToSingle(elemList[i].Attributes["Mp"].Value, new CultureInfo("en-US"));
          hero.HeroPropetries.Mmp = Convert.ToSingle(elemList[i].Attributes["Mmp"].Value, new CultureInfo("en-US"));
          hero.HeroPropetries.Cr = Convert.ToSingle(elemList[i].Attributes["Co"].Value, new CultureInfo("en-US"));
          hero.HeroPropetries.Mcr = Convert.ToSingle(elemList[i].Attributes["Mco"].Value, new CultureInfo("en-US"));
          hero.HeroPropetries.Agi = Convert.ToSingle(elemList[i].Attributes["Agi"].Value, new CultureInfo("en-US"));
        }
      }
      ////Load vagons
      //Party party = FindObjectOfType<Party>();      
      //elemList = doc.GetElementsByTagName("Vagon");
      //for (int i = 0; i < elemList.Count; i++)
      //{
      //  party.Connect(elemList[i].Attributes["vagon"].Value);         
      //}
      //elemList = doc.GetElementsByTagName("Gold");
      //for (int i = 0; i < elemList.Count; i++)
      //{
      //  party.Gold = Int32.Parse(elemList[i].Attributes["gold"].Value);
      //}
      //elemList = doc.GetElementsByTagName("TimeGame");
      //for (int i = 0; i < elemList.Count; i++)
      //{
      //  party.TimeGame = Convert.ToSingle(elemList[i].Attributes["timeGame"].Value, new CultureInfo("en-US"));
      //}
      //
    }
    ///////////////////////Save current Scene
    doc = new XmlDocument();
    XmlNode rootNode = null;
    filePath = Application.dataPath + "/StreamingAssets/Saves/LastScene.xml";
    if (!File.Exists(filePath))
    {
      rootNode = doc.CreateElement("LastSceneData");
      doc.AppendChild(rootNode);
    }
    else
    {
      doc.Load(filePath);
      rootNode = doc.DocumentElement;
    }
    rootNode.RemoveAll();
    AddAtribute("lastScene", sceneName);
    AddXmlElements(doc, rootNode, "LastScene");
    doc.Save(filePath);
    //Load Inventar
    LoadInventar();
  }

  private void LoadInventar()
  {
    Inventar inventar = FindObjectOfType<Inventar>();
    BaseOfInventar baseOfInventar = FindObjectOfType<BaseOfInventar>();
    string filePath = Application.dataPath + "/StreamingAssets/Saves/InventarData.xml";
    if (File.Exists(filePath))
    {
      XmlDocument doc = new XmlDocument();
      XmlNodeList elemList = null;
      doc.Load(filePath);
      elemList = doc.GetElementsByTagName("Item");
      for (int i = 0; i < elemList.Count; i++)
      {
        foreach (var item in baseOfInventar.Items)
        {
          if (elemList[i].Attributes["Name"].Value == item.Name)
          {            
            item.Count = Int32.Parse(elemList[i].Attributes["Count"].Value); 
            inventar.AddItem(item);
          }
        }
      }
    }
  }

  public void PressButtonNewGame()
  {
    DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/StreamingAssets/Saves/");

    foreach (FileInfo file in dirInfo.GetFiles())
    {
      file.Delete();
    }
    SceneManager.LoadScene(firstScene);
  }  

  public void PressButtonLoadGame()
  {
    string filePath = Application.dataPath + "/StreamingAssets/Saves/LastScene.xml";
    XmlDocument doc = new XmlDocument();
    doc.Load(filePath);
    XmlNodeList elemList = doc.GetElementsByTagName("LastScene");
    
    for (int i = 0; i < elemList.Count; i++)
    {      
      string lastSceneName = elemList[i].Attributes["lastScene"].Value;      
      SceneManager.LoadScene(lastSceneName);
    }
  }
}


