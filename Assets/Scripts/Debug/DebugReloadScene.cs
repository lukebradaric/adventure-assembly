using UnityEngine.SceneManagement;

namespace AdventureAssembly.Debug
{
    public class DebugReloadScene : DebugKeyPress
    {
        public override void DebugKeyPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}