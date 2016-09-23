//Интерфейс сохраняемых объектов.
//Сохранение производится в формате JSon.
public interface ISave
{
    void Awake();
    void OnDestroy();
    SavedData GetDataToSave();
    void SetSavedData(string str);
    int GetObjID();
}
