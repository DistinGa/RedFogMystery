using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
  [SerializeField] [Tooltip("Имя сцены которую следует загрузить")] private string sceneName = "scene name";
  [SerializeField] [Tooltip("Где окажется Герой, когда снова придёт в эту сцену")] private Transform returnPosition = null;
  [SerializeField] [Tooltip("Интервал в секундах до загрузки новой сцены")] private float interval = 0;

  public void OnEventAction()
  {
    Invoke("LoadNewScene", interval);
  }

  private void LoadNewScene()
  {
    if (returnPosition != null)
      FindObjectOfType<CharacterMoving>().transform.position = returnPosition.position;
    else
      Debug.LogWarning("Дмитрий! Объект " + gameObject.name + " returnPosition не назначен!");

    SaveController saveController = FindObjectOfType<SaveController>();
    if (saveController != null)
      saveController.SaveScene();
    else
      Debug.LogWarning("Сохранения не записаны. Необходимо загружать игру из сцены StartMenu");
    SceneManager.LoadScene(sceneName);
  }
}
