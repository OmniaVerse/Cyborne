using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLayoutCustomizer : MonoBehaviour
{
    public bl_LayoutCustomizer layoutCustomizer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            layoutCustomizer.ActiveCustomizer();
        }
    }
}