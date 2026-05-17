using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 汎用的シングルトンパターン基底クラス
/// </summary>
/// <typeparam name="T">クラス型を設定</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// クラスインスタンスの保存用
    /// </summary>
    private static T instance;

    /// <summary>
    /// クラスインスタンスを受け取るプロパティ
    /// </summary>
    public static T Instance
    {
        // インスタンスの設定
        get
        {
            // インスタンスが既に設定されているなら
            if (instance) return instance;

            // インスタンスに同じ型のオブジェクトを探してきて代入
            instance = FindAnyObjectByType<T>();

            // 設定できたらそのまま返す
            if (instance) return instance;

            // インスタンスが設定できなかったらエラー処理
            Debugger.LogError(typeof(T) + "型のオブジェクトは見つかりませんでした");
            return null;
        }
    }

    private void Awake()
    {
        // インスタンスが既にあり、自分じゃなかったら
        if (instance && instance != this)
        {
            // 削除する
            Destroy(gameObject);
        }
        else
        {
            // 一応キャストする
            instance = this as T;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}