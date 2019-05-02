// ********************************************************
// 描述：UI扩展类
// 作者：ShadowRabbit 
// 创建时间：2019-05-01 21:27:05
// ********************************************************
using System.Collections;
using UnityEngine;

public static class UIextension
{
    /// <summary>
    /// 透明度渐变
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="alpha">目标透明度</param>
    /// <param name="duration">规定的时间内</param>
    /// <returns></returns>
    public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup, float alpha, float duration)
    {
        float time = 0f;
        float originalAlpha = canvasGroup.alpha;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
            yield return new WaitForEndOfFrame();
        }
        canvasGroup.alpha = alpha;
    }
}
