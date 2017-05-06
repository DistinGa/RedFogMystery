using UnityEngine;

[AddComponentMenu("Cut Scenes/Сменить локацию")]
public class CSGoToScene : CSEvent
{
    [SerializeField]
    [Tooltip("Имя сцены которую следует загрузить")]
    private string sceneName = "scene name";
    [SerializeField]
    [Tooltip("Где окажется Герой на новой сцене")]
    private Vector3 newPosition;
    [SerializeField]
    [Tooltip("Интервал в секундах до загрузки новой сцены")]
    private float interval = 0;

    public override void OnEventAction()
    {
        Invoke("LoadNewScene", interval);
    }

    private void LoadNewScene()
    {
        GameManager.GM.ChangeScene(sceneName, newPosition);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0);
        Gizmos.DrawWireSphere(newPosition, 15);
    }
}
