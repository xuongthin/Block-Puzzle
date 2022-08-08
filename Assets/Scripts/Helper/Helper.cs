using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class Helper
{
    /// <summary>
    /// This function return a rect that clamped 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="bound"></param>
    /// <returns></returns>
    public static Rect ClampRect(Rect target, Rect bound)
    {
        Vector2 position = target.position;

        if (target.xMin < bound.xMin)
        {
            position.x += bound.xMin - target.xMin;
        }
        else if (target.xMax > bound.xMax)
        {
            position.x += bound.xMax - target.xMax;
        }

        if (target.yMin < bound.yMin)
        {
            position.y += bound.yMin - target.yMin;
        }
        else if (target.yMax > bound.yMax)
        {
            position.y += bound.yMax - target.yMax;
        }

        target.position = position;
        return target;
    }

    /// <summary>
    /// return a rect that is outside of bound (for now, it just works with square-rect)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="bound"></param>
    /// <returns></returns>
    public static Rect UnclampRect(Rect target, Rect bound)
    {
        if (!target.Overlaps(bound))
            return target;

        Vector2 position = target.position;
        Vector2 invadeDirection = bound.center - target.center;
        if (Mathf.Abs(invadeDirection.x) > Mathf.Abs(invadeDirection.y))
        {
            position.x += ((target.size.x / 2 + bound.size.x / 2) - Mathf.Abs(invadeDirection.x)) * Mathf.Sign(-invadeDirection.x);
        }
        else
        {
            position.y += ((target.size.y / 2 + bound.size.y / 2) - Mathf.Abs(invadeDirection.y)) * Mathf.Sign(-invadeDirection.y);
        }
        target.position = position;
        return target;
    }

    public static void SetSpriteAndResize(this Image UIImage, Sprite sprite)
    {
        Vector2 size = UIImage.rectTransform.sizeDelta;
        UIImage.sprite = sprite;
        UIImage.SetNativeSize();
        Vector2 newSize = UIImage.rectTransform.sizeDelta;
        float modification = size.x / newSize.x;
        Vector2 reshapeSize = newSize * modification;
        UIImage.rectTransform.sizeDelta = reshapeSize;
    }

    /// <summary>
    /// Logs a message to Unity Console (only work in Editor)
    /// </summary>
    /// <param name="log"></param>
    public static void Log(this string log)
    {
#if UNITY_EDITOR
        Debug.Log(log);
#endif
    }

    public static string ColorCode(this Color color)
    {
        string o = color.r.ToString() + "," +
                   color.g.ToString() + "," +
                   color.b.ToString() + "," +
                   color.a.ToString();
        return o;
    }

    // public static void FlipHorizontal<T>(this T[,] mat)
    // {
    //     for (int j = 0; j < mat.GetLength(0); j++)
    //     {
    //         for (int i = 0; i < (mat.GetLength(1) + 1) / 2; i++)
    //         {
    //             // swap
    //             T temp = mat[j, i];
    //             mat[j, i] = mat[j, mat.GetLength(1) - 1 - i];
    //             mat[j, mat.GetLength(1) - 1 - i] = temp;
    //         }
    //     }
    // }

    public static T[,] FlipHorizontal<T>(this T[,] mat)
    {
        T[,] clone = (T[,])mat.Clone();

        for (int j = 0; j < clone.GetLength(0); j++)
        {
            for (int i = 0; i < (clone.GetLength(1) + 1) / 2; i++)
            {
                // swap
                T temp = clone[j, i];
                clone[j, i] = clone[j, clone.GetLength(1) - 1 - i];
                clone[j, clone.GetLength(1) - 1 - i] = temp;
            }
        }

        return clone;
    }

    public static void Rotate90<T>(this T[,] mat)
    {
        int N = mat.GetLength(0);
        for (int x = 0; x < N / 2; x++)
        {
            for (int y = x; y < N - x - 1; y++)
            {
                T temp = mat[x, y];

                mat[x, y] = mat[y, N - 1 - x];
                mat[y, N - 1 - x] = mat[N - 1 - x, N - 1 - y];
                mat[N - 1 - x, N - 1 - y] = mat[N - 1 - y, x];
                mat[N - 1 - y, x] = temp;
            }
        }
    }

    public static T[,] Rotate<T>(this T[,] mat)
    {
        T[,] clone = (T[,])mat.Clone();

        int N = clone.GetLength(0);
        for (int x = 0; x < N / 2; x++)
        {
            for (int y = x; y < N - x - 1; y++)
            {
                T temp = clone[x, y];

                clone[x, y] = clone[y, N - 1 - x];
                clone[y, N - 1 - x] = clone[N - 1 - x, N - 1 - y];
                clone[N - 1 - x, N - 1 - y] = clone[N - 1 - y, x];
                clone[N - 1 - y, x] = temp;
            }
        }

        return clone;
    }
}