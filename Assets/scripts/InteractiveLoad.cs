using UnityEngine.SceneManagement;

public class InteractiveLoad : HoloToolkit.Examples.InteractiveElements.Interactive
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
