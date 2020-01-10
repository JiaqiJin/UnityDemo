using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour
{

    private Text toolTiopText;
    private Text contextText;
    private CanvasGroup canvasGroup;

    public float smooth = 1;

    private float targetAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        toolTiopText = GetComponent<Text>();
        contextText = transform.Find("Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smooth * Time.deltaTime);
            if(Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    public void Hide()
    {
        targetAlpha = 0;
    }

    public void Show(string text)
    {
        toolTiopText.text = text;
        contextText.text = text;
        targetAlpha = 1;
    }

    public void SetLocalPos(Vector2 position)
    {
        transform.localScale = position;
    }
}
