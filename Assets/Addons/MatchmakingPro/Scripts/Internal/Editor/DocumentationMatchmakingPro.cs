using UnityEngine;
using UnityEditor;
using MFPSEditor;
using UnityEditor.SceneManagement;
using MFPS.Addon.MatchMakingPro;

public class DocumentationMatchmakingPro : TutorialWizard
{
    //required//////////////////////////////////////////////////////
    public string FolderPath = "mfps2/editor/matchmaking-pro/";
    public NetworkImages[] m_ServerImages = new NetworkImages[]
    {
        new NetworkImages{Name = "img-0.png", Image = null},
        new NetworkImages{Name = "img-1.png", Image = null},
        new NetworkImages{Name = "img-2.png", Image = null},
        new NetworkImages{Name = "img-3.png", Image = null},
        new NetworkImages{Name = "img-4.png", Image = null},
    };
    public Steps[] AllSteps = new Steps[] {
    new Steps { Name = "Integration", StepsLenght = 0 , DrawFunctionName = nameof(GetStartedDoc)},
    new Steps { Name = "Customize", StepsLenght = 0, DrawFunctionName = nameof(SecondSection) },
    new Steps { Name = "Open Selector", StepsLenght = 0, DrawFunctionName = nameof(OpenSelectorDoc) },
    };
    private readonly GifData[] AnimatedImages = new GifData[]
   {
        new GifData{ Path = "name.gif" },
   };

    public override void OnEnable()
    {
        base.OnEnable();
        base.Initizalized(m_ServerImages, AllSteps, FolderPath, AnimatedImages);
        Style.highlightColor = ("#c9f17c").ToUnityColor();
        allowTextSuggestions = true;
    }

    public override void WindowArea(int window)
    {
        AutoDrawWindows();
    }
    //final required////////////////////////////////////////////////

    void GetStartedDoc()
    {
        DrawText("The integration is one-click process, you simply have to open the <b>MainMenu</b> scene in the editor and click in the <b>Integrate</b> button below.");
        Space(10);
        using(new CenteredScope())
        {
            if (EditorSceneManager.GetActiveScene().name != "MainMenu")
            {
                if (DrawButton("Open Main Menu"))
                {
                    OpenScene("Assets/MFPS/Scenes/MainMenu.unity");
                }
            }
            Space(10);
            if (DrawButton("Integrate"))
            {
                Integrate();
            }
        }
    }

    void SecondSection()
    {
        DrawText("You can customize the whole matchmaking UI, including the game mode selection list, by default the Matchmaking Pro UI is located in the MainMenu scene hierarchy in: <b>Lobby > Canvas [Default Menu] > Overall Windows > MatchmakingPro</b>, enable the first child <b>Content</b> and customize as please.");
        DrawServerImage("img-0.png");
        Space(20);
        DrawSuperText("Altought you can modify the game mode item template there too, the background image of each game mode is assigned in runtime, in order to modify these images simply change the texture in the\n<?link=asset:Assets/Addons/MatchmakingPro/Resources/MatchmakingProSettings.asset>MatchmakingProSettings</link> > GameModes > <i>*foldout the game mode*</i> > <b>Background Image</b>.");
        DrawServerImage("img-1.png");

    }

    void OpenSelectorDoc()
    {
        DrawText("By default after the integration, the matchmaking pro game mode selector menu opens when you click on the MFPS default Play button.\n \nIn the case you have modified this button or you want to add another button to open this menu, all you have to do is add a new <b>onClick</b> listener in your button inspector > in the new field drag the <b>Matchmaking Pro</b> instance which is located in <b>Lobby > Canvas [Default Menu] > Overall Windows > MatchmakingPro</b> in the hierarchy, then point the listener to the function <b>bl_MatchmakingPro.SetActive</b> > and check the toggle box.");
        DrawServerImage("img-2.png");
    }

    static void Integrate()
    {
        // check if the opened scene is the MainMenu scene
        if(EditorSceneManager.GetActiveScene().name != "MainMenu")
        {
            Debug.LogWarning("This is not the MainMenu scene, the integration has to be run in the MainMenu scene.");
            return;
        }

        if(bl_LobbyUI.Instance == null)
        {
            Debug.LogWarning("This is not the MFPS MainMenu scene, the integration has to be run in the MFPS MainMenu scene.");
            return;
        }

        if(bl_LobbyUI.Instance.GetComponentInChildren<bl_MatchmakingPro>(true) != null)
        {
            Debug.Log("Matchmaking Pro has been integrated already.");
            return;
        }

        var prefab = AssetDatabase.LoadAssetAtPath("Assets/Addons/MatchmakingPro/Prefabs/Main/MatchmakingPro.prefab", typeof(GameObject)) as GameObject;
        var parent = bl_LobbyUI.Instance.AddonsButtons[16].transform;
        var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        instance.transform.SetParent(parent, false);

        EditorUtility.SetDirty(instance);
        Selection.activeGameObject = instance;
        EditorGUIUtility.PingObject(instance.gameObject);
        Debug.Log("Matchmaking Pro has been integrated.");

    }

    [MenuItem("MFPS/Tutorials/Matchmaking Pro")]
    [MenuItem("MFPS/Addons/Matchmaking Pro/Documentation")]

    static void Open()
    {
        GetWindow<DocumentationMatchmakingPro>();
    }
}
[MFPSManagerTab(ADDON_NAME)]
public class MFPSMatchmakingProTab : MFPSManagerTabData
{
    const string ADDON_NAME = "Matchmaking Pro";

    public override Data GetData()
    {
        return new Data()
        {
            Title = ADDON_NAME,
            BodyDrawFunc = GetMethodInfoOf(nameof(Body)),
        };
    }

    public static void Body(bl_MFPSManagerWindow window)
    {
        window.DrawTitleText(ADDON_NAME);
        window.DrawEditorOf(bl_MatchmakingProSettings.Instance);
    }
}