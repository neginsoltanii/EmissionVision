using UnityEngine;
using UnityEngine.UI;

public class ButtonShapeController : MonoBehaviour
{
    public Button yourButton; // 将你的按钮拖动到此变量
    private Vector2 initialSize;

    void Start()
    {
        // 获取按钮的 RectTransform 组件
        RectTransform buttonRect = yourButton.GetComponent<RectTransform>();

        // 保存按钮的初始尺寸
        initialSize = new Vector2(160f, 50f);

        // 添加按钮点击事件监听
        yourButton.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        // 确保按钮在每帧都保持其初始尺寸
        RectTransform buttonRect = yourButton.GetComponent<RectTransform>();
        if (buttonRect.sizeDelta != initialSize)
        {
            buttonRect.sizeDelta = initialSize;
        }
    }

    void OnButtonClick()
    {
        // 处理按钮点击事件
        // 确保按钮点击后尺寸不变
        RectTransform buttonRect = yourButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = initialSize;
    }
}
