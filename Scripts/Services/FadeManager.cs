using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス .
/// </summary>
public class FadeManager : MonoBehaviour
{
    #region Singleton

    private static FadeManager instance;

    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    /// <summary>
    /// デバッグモード .
    /// </summary>
    [FormerlySerializedAs("DebugMode")] public bool debugMode = false;

    /// <summary>フェード中の透明度</summary>
    private float _fadeAlpha;

    /// <summary>フェード中かどうか</summary>
    private bool _isFading;

    /// <summary>フェード色</summary>
    public Color fadeColor = Color.black;


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void OnGUI()
    {
        // Fade .
        if (_isFading)
        {
            //色と透明度を更新して白テクスチャを描画 .
            fadeColor.a = _fadeAlpha;
            GUI.color = fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }

        if (!debugMode) return;
        if (_isFading) return;
        //Scene一覧を作成 .
        //(UnityEditor名前空間を使わないと自動取得できなかったので決めうちで作成) .
        var scenes = new List<string> { "SampleScene" };
        //scenes.Add ("SomeScene1");
        //scenes.Add ("SomeScene2");


        //Sceneが一つもない .
        if (scenes.Count == 0)
        {
            GUI.Box(new Rect(10, 10, 200, 50), "Fade Manager(Debug Mode)");
            GUI.Label(new Rect(20, 35, 180, 20), "Scene not found.");
            return;
        }


        GUI.Box(new Rect(10, 10, 300, 50 + scenes.Count * 25), "Fade Manager(Debug Mode)");
        GUI.Label(new Rect(20, 30, 280, 20), "Current Scene : " + SceneManager.GetActiveScene().name);

        var i = 0;
        foreach (var sceneName in scenes)
        {
            if (GUI.Button(new Rect(20, 55 + i * 25, 100, 20), "Load Level"))
            {
                LoadScene(sceneName, 1.0f);
            }

            GUI.Label(new Rect(125, 55 + i * 25, 1000, 20), sceneName);
            i++;
        }
    }

    /// <summary>
    /// 画面遷移 .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    public void LoadScene(string scene, float interval = 1f)
    {
        StartCoroutine(TransScene(scene, interval));
    }

    /// <summary>
    /// シーン遷移用コルーチン .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene, float interval)
    {
        //だんだん暗く .
        _isFading = true;
        float time = 0;
        while (time <= interval)
        {
            _fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切替 .
        SceneManager.LoadScene(scene);

        //だんだん明るく .
        time = 0;
        while (time <= interval)
        {
            this._fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        _isFading = false;
    }
}