using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMenuOption
{
    private const string UI_GAMEIMAGE_MENU_PATH = "GameObject/Framework/UI/GameImage";
    private const string UI_GAMEBUTTON_MENU_PATH = "GameObject/Framework/UI/GameButton";
    private const string UI_EMPTYGRAPHIC_MENU_PATH = "GameObject/Framework/UI/EmptyGraphic";

    [MenuItem(UI_GAMEIMAGE_MENU_PATH, priority = 0)]
    public static void CreateGameImage()
    {
        GameObject componentGo = CreateUIComponent(Path.GetFileName(UI_GAMEIMAGE_MENU_PATH));
        if (componentGo == null)
            return;
        Image gameImage = componentGo.AddComponent<Image>();
        gameImage.raycastTarget = false;
    }

    [MenuItem(UI_GAMEBUTTON_MENU_PATH, priority = 1)]
    public static void CreateGameButton()
    {
        GameObject componentGo = CreateUIComponent(Path.GetFileName(UI_GAMEBUTTON_MENU_PATH));
        if (componentGo == null)
            return;
        componentGo.AddComponent<GameButton>();
        componentGo.AddComponent<Image>();
    }

    [MenuItem(UI_EMPTYGRAPHIC_MENU_PATH, priority = 2)]
    public static void CreateEmptyGraphic()
    {
        GameObject componentGo = CreateUIComponent(Path.GetFileName(UI_EMPTYGRAPHIC_MENU_PATH));
        if (componentGo == null)
            return;
        componentGo.AddComponent<EmptyGraphic>();
        componentGo.AddComponent<CanvasRenderer>();
    }

    #region

    /// <summary>
    /// 创建UI组件
    /// </summary>
    private static GameObject CreateUIComponent(string componentName)
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogError("必须选择一个节点");
            return null;
        }

        GameObject componentGo = new GameObject(componentName);
        componentGo.layer = LayerMask.NameToLayer("UI");
        componentGo.AddComponent<RectTransform>();
        Transform parent;
        Canvas canvas = Selection.activeGameObject.GetComponentInParent<Canvas>(true);
        PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (canvas == null)
        {
            canvas = CreateCanvas();
            canvas.transform.SetParent(Selection.activeGameObject.transform, false);
            parent = canvas.transform;
            if (prefabStage == null)
            {
                if (Object.FindObjectOfType<EventSystem>() == null)
                {
                    CreateEventSystem();
                }
            }
        }
        else
        {
            parent = Selection.activeGameObject.transform;
        }
        componentGo.transform.SetParent(parent, false);
        componentGo.transform.localPosition = Vector3.zero;
        componentGo.transform.localScale = Vector3.one;

        Undo.RegisterFullObjectHierarchyUndo(componentGo, ""); //触发一次预制体的变更，为了保存预制体变更

        return componentGo;
    }

    /// <summary>
    /// 创建UI画布
    /// </summary>
    private static Canvas CreateCanvas()
    {
        GameObject canvasGo = new GameObject("Canvas");
        canvasGo.layer = LayerMask.NameToLayer("UI");
        canvasGo.AddComponent<RectTransform>();
        Canvas canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGo.AddComponent<CanvasScaler>();
        canvasGo.AddComponent<GraphicRaycaster>();
        return canvas;
    }

    private static EventSystem CreateEventSystem()
    {
        GameObject eventSystemGo = new GameObject("EventSystem");
        EventSystem eventSystem = eventSystemGo.AddComponent<EventSystem>();
        eventSystemGo.AddComponent<StandaloneInputModule>();
        return eventSystem;
    }

    #endregion
}