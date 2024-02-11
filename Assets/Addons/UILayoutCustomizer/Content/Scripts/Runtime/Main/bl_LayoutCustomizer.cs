using System;
using System.Collections.Generic;
using UnityEngine;
using MFPS.Addon.LayoutCustomizer;
using UnityEngine.Events;

public class bl_LayoutCustomizer : bl_LayoutBase
{
    #region Public members
    [Header("Settings")]
    public string setupKey = "default";

    [Header("Events")]
    [SerializeField] private UEvent m_onCustomizerOpen;
    [SerializeField] private UEvent m_onCustomizerClosed;
    [SerializeField] private UEvent m_onChangesApplied;

    [Header("References")]
    public bl_LayoutMark markUITemplate = null;
    public bl_LayoutPropertiesWindow propertiesWindow;
    public GameObject content;
    public RectTransform marksPanel;
    #endregion

    #region Public properties
    public Action onOpen, onClose, onChangesApplied;
    public bl_ModifiableLayout SelectedLayout { get; set; } = null;
    public bool IsSetupLoaded { get; private set; } = false;
    public LayoutSetupData LayoutData { get; private set; }
    #endregion

    [Serializable] public class UEvent : UnityEvent { }
    private List<bl_ModifiableLayout> layoutMarks = new List<bl_ModifiableLayout>();


    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        LoadSetup();
        propertiesWindow?.Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    void LoadSetup()
    {
        var data = LayoutSetupData.LoadSetup(this.setupKey);
        if (data == null) LayoutData = new LayoutSetupData();
        else LayoutData = data;

        layoutMarks.ForEach(x =>
        {
            if (x != null)
            {
                x.Init();
            }
        });

        IsSetupLoaded = true;
        content.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="layout"></param>
    /// <returns></returns>
    public static bool LoadLayoutDataFor(bl_ModifiableLayout layout, out LayoutRectData rectData)
    {
        rectData = null;
        if (Instance == null || Instance.LayoutData == null) return false;

        rectData = Instance.LayoutData.GetDataFor(layout.layoutKey);
        return rectData != null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mark"></param>
    public static void AddLayoutMarkInstance(bl_ModifiableLayout mark)
    {
        if (Instance == null) return;
        if (Instance.layoutMarks.Contains(mark)) return;

        Instance.layoutMarks.Add(mark);
    }

    /// <summary>
    /// Active the edit mode
    /// </summary>
    public void ActiveCustomizer()
    {
        PrepareAllMarks();
        propertiesWindow?.Hide();
        SetActiveMarks(true);
        FetchMarksRectTransforms();
    }

    /// <summary>
    /// Apply all the changes made in the layouts and save them.
    /// </summary>
    public void ApplyModifications()
    {
        LayoutData = new LayoutSetupData();
        layoutMarks.ForEach(x =>
        {
            if (x != null)
            {
                x.FetchRectTransform();
                LayoutData.AddData(x);
            }
        });
        LayoutSetupData.SaveFor(setupKey, LayoutData);
        m_onChangesApplied?.Invoke();
        onChangesApplied?.Invoke();
        SetActiveMarks(false);
    }

    /// <summary>
    /// Reset any modification made since the edit mode start and close the menu.
    /// </summary>
    public void Cancel()
    {
        ResetModifications();
        SetActiveMarks(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResetModifications()
    {
        layoutMarks.ForEach(x =>
        {
            if (x != null) x.ResetToOriginal();
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetAllToDefault()
    {
        layoutMarks.ForEach(x =>
        {
            if (x != null)
            {
                x.ResetToOriginal();
            }
        });
        propertiesWindow?.ResetLayout();
    }

    /// <summary>
    /// Active/Deactive all the UI Marks
    /// </summary>
    /// <param name="active"></param>
    public void SetActiveMarks(bool active)
    {
        if (active)
        {
            onOpen?.Invoke();
            m_onCustomizerOpen?.Invoke();
        }
        else
        {
            onClose?.Invoke();
            m_onCustomizerClosed?.Invoke();
        }
        content.SetActive(active);
        layoutMarks.ForEach(x =>
        {
            if (x != null)
            {
                x.SetActiveMark(active);
                x.LayoutMark.SetActiveSectionUI(false);
            }
        });
    }

    /// <summary>
    /// This is should called only once
    /// Instance all the marks UI in the modifiable layouts
    /// </summary>
    public void PrepareAllMarks()
    {
        layoutMarks.ForEach(x =>
        {
            if (x != null) x.Prepare();
        });
    }

    /// <summary>
    /// Fetch/Obtains all the current rectTransform vectors and stored in the cache
    /// </summary>
    public void FetchMarksRectTransforms()
    {
        layoutMarks.ForEach(x =>
        {
            if (x != null) x.FetchRectTransform();
        });
    }

    /// <summary>
    /// Called when a modifiable layout has been touch/clicked
    /// </summary>
    /// <param name="mark"></param>
    public void OnMarkSelected(bl_LayoutMark mark)
    {
        layoutMarks.ForEach(x =>
        {
            if (x != null) x.LayoutMark.SetActiveSectionUI(false);
        });

        if (SelectedLayout == mark.LayoutEntity)
        {
            SelectedLayout = null;
            mark.SetActiveSectionUI(false);
        }
        else
        {
            mark.SetActiveSectionUI(true);
        }
        propertiesWindow?.PrepareForLayout(mark.LayoutEntity);
        propertiesWindow?.Show();
    }

    private static bl_LayoutCustomizer _instance;
    public static bl_LayoutCustomizer Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<bl_LayoutCustomizer>();
            return _instance;
        }
    }
}