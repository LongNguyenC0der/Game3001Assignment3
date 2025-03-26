using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneGameMode : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayEffectSound();
            SceneManager.LoadScene(0);
        });
    }
}
