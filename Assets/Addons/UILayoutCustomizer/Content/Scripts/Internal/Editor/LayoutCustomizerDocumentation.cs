using MFPSEditor;
using UnityEditor;
using UnityEngine;

public class LayoutCustomizerDocumentation : TutorialWizard
{
    //required//////////////////////////////////////////////////////
    public string FolderPath = "layout-customizer/editor/";

    public NetworkImages[] m_ServerImages = new NetworkImages[]
    {
        new NetworkImages{Name = "img-0.png", Image = null},
        new NetworkImages{Name = "img-1.png", Image = null},
        new NetworkImages{Name = "img-2.png", Image = null},
        new NetworkImages{Name = "img-3.png", Image = null},
        new NetworkImages{Name = "img-4.png", Image = null},
    };

    public Steps[] AllSteps = new Steps[] {
    new Steps { Name = "Get Started", StepsLenght = 0 , DrawFunctionName = nameof(GetStartedDoc)},
    new Steps { Name = "Active Edit Mode", StepsLenght = 0, DrawFunctionName = nameof(SecondSection) },
    new Steps { Name = "Add Layout", StepsLenght = 0, DrawFunctionName = nameof(AddLayoutDoc) },
    new Steps { Name = "UI Mapped", StepsLenght = 0, DrawFunctionName = nameof(UIMappedDoc) },
    new Steps { Name = "Modify Layout Mark", StepsLenght = 0, DrawFunctionName = nameof(ModifiableMarkDoc) },
    new Steps { Name = "Events", StepsLenght = 0, DrawFunctionName = nameof(EventsDoc) },
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

    private void GetStartedDoc()
    {
        DrawText("<b><size=18>MFPS INTEGRATION</size></b>\n\n1. Open a scene map <i>(you have to do this for each map scene)</i>\n\n2.With the scene opened, go to the Unity top menu under: <b>MFPS ➔ Addons ➔ Layout Customizer ➔ Integrate.</b>\n\n3. Save the scene changes and you all set.\n\n<b><size=18>IN-GAME USAGE</size></b>\n\nIn order to open the layout customizer menu in game, <b>open the pause menu ➔ Settings ➔ Controls ➔ Custom Layout ➔ Click on the Customize button.</b>\n\n");
        DrawHyperlinkText("<b><size=18>INTEGRATION:</size></b>\n\n<b>1</b>. In the scene where you want to use the Layout Customizer, drag the prefab <link=asset:Assets/UILayoutCustomizer/Content/Prefabs/Main/Layout Customizer.prefab>Layout Customizer</link> in the hierarchy.\n\n<b>2</b>. In each UI Element that you wanna make editable in-runtime, attach the script <b>bl_ModifiableLayout.cs</b> and adjust the properties of it as needed.\n\n<b>3</b>. In order to Active the customization system in runtime, you have to decide how you wanna call the function in the Layout Customizer ➔ bl_LayoutCustomizer ➔ ActiveCustomizer();, for more detail on this check the '<b>Active Edit Mode</b>' section.");
    }

    private void SecondSection()
    {
        DrawText("Once the <i>Layout Customizator</i> has been integrated into your scene and you have attached the <i>bl_ModifiableLayout.cs</i> script to the UI Rectransforms that you want to make modifiable in-runtime, you have to let the system know when to enter in Edit mode to allow modify the layouts, e.g <i>when the player press a button in the menu, press a key, etc...</i>\n \nUsually is with a button, you create a button in your menu/pause menu that will allow the player to edit the layouts/UI,\nso let's take that scenario.\n \nThe easier way, in this case, is by adding the ActiveCustomizer() callback in the Button component ➔ OnClick list.");
        DrawServerImage(0);
        DownArrow();
        DrawText("Or by code you can do it with:");
        DrawCodeText("public bl_LayoutCustomizer layoutCustomizer;\n \n    private void Update()\n    {\n        if (Input.GetKeyDown(KeyCode.O))\n        {\n            layoutCustomizer.ActiveCustomizer();\n        }\n    }");
    }

    private void AddLayoutDoc()
    {
        DrawSuperText("Is called <?underline=>Layout</underline> in this documentation to all interacable UI elements in the Canvas of a scene, <i>e.g Images, Raw Images, Text, Buttons, etc...</i>\n \nIn order to make a specific layout editable through the Layout Customizer, you simply have to attach a script to the UI object.\n \n<?list=■>1. In the UI element that you want to make editable, attach the script <?underline=>bl_ModifiableLayout.cs</underline>\n2. In the inspector of the just added script, modify the values if necessary.\n3. No step 3, that's :D</list>\n\n<?background=#000>NOTE:</background> <i>Make sure the UIElement where you attach the script is interactable (Raycast Target is On, is under a Canvas that has a Graphic Raycaster, is not blocked by other UI Element.</i>");
        DrawServerImage("img-2.png");
    }

    void EventsDoc()
    {
        DrawText("If you want to implement a custom behavior when the customizer menu is <b>open</b> or <b>closed</b>, e.g <i>hide your game menu, stopped the player, etc...</i> you can do so by add a callback when the customizer menu is open and when is closed.\n\nYou can add this listener either in the inspector of <b>bl_LayoutCustomizer</b>:");
        DrawServerImage("img-3.png");
        DrawText("Or by code with:");
        DrawCodeText("      bl_LayoutCustomizer.Instance.onOpen += MyCallback;\n      bl_LayoutCustomizer.Instance.onClose += MyAnotherCallback;");
    }

    void ModifiableMarkDoc()
    {
        DrawServerImage("img-4.png", TextAlignment.Center);
        DrawHyperlinkText("<b>Layout Mark</b> is the UI that overlay your game UI elements to indicate that it's a modifiable element, by default it's a outlined box that turns orange when the layout is selected, but of course, you can modify this, shape, color, etc...\n\nFor modify it, simply drag the prefab <link=asset:Assets/UILayoutCustomizer/Content/Prefabs/Instances/Layout Mark.prefab>Layout Mark</link> in an canvas in any scene ➔ make the pertinent changes ➔ save the changes to the prefab.");
    }

    void UIMappedDoc()
    {
        DrawServerImage("img-1.png", TextAlignment.Center);
        DrawHorizontalColumn("1", "Set all the layouts to they original position.");
        DrawHorizontalColumn("2", "Cancel the operation, reset the unsaved changes and close the menu.");
        DrawHorizontalColumn("3", "Reset the unsaved changes.");
        DrawHorizontalColumn("4", "Confirm, apply and save the modifications -> close the window.");
        DrawHorizontalColumn("5", "Modify the selected layout size.");
        DrawHorizontalColumn("6", "Modify the selected layout opacity/alpha.");
        DrawHorizontalColumn("7", "Move the selected layout a step to the button arrow direction.");
        DrawHorizontalColumn("8", "Hide/Show the customizer window (to preview the game UI).");
        DrawHorizontalColumn("9", "Reset the modifications of the selected layout.");
    }

    [MenuItem("MFPS/Addons/Layout Customizer/Documentation")]
    [MenuItem("MFPS/Tutorials/Layout Customizer")]
    private static void Open()
    {
        GetWindow<LayoutCustomizerDocumentation>();
    }
}