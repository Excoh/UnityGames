using UnityEditor;

public class ExampleWindow : EditorWindow
{
    [MenuItem("Window/Example")]
    public static void Init()
    {
        GetWindow(typeof(ExampleWindow));
    }

    private void OnGUI()
    {
    }
}
