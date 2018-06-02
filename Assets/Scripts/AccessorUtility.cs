using System;
using System.Collections.Generic;

namespace AccessorUtility {

    /// <summary>
    /// 任意のオブジェクトのインスタンスをキーにした参照ディクショナリ
    /// </summary>
    /// <remarks>弱参照っぽい実装を Getter 内に施している</remarks>
    /// <typeparam name="TKey">キーにするインスタンスの型</typeparam>
    /// <typeparam name="TValue">インスタンスの型</typeparam>
    /// <typeparam name="TInstanceMap">インスタンス保存用のディクショナリの型</typeparam>
    public class BasicReferenceMap<TKey, TValue, TInstanceMap> : Dictionary<TKey, TInstanceMap> where TInstanceMap : Dictionary<Type, TValue>, new() {

        public new TInstanceMap this[TKey key] {
            get {
                TInstanceMap value;
                if (!this.TryGetValue(key, out value)) {
                    value = new TInstanceMap();
                    base[key] = value;
                }
                return value;
            }
            set {
                base[key] = value;
            }
        }

    }

    /// <summary>
    /// Getter を拡張したインスタンス保存用のディクショナリ
    /// </summary>
    /// <remarks>KeyNotFoundException を防いでいる</remarks>
    /// <typeparam name="TValue">値の型</typeparam>
    public class BasicInstanceMap<TValue> : Dictionary<Type, TValue> {

        public new TValue this[Type key] {
            get {
                TValue value;
                this.TryGetValue(key, out value);
                return value;
            }
            set {
                base[key] = value;
            }
        }

    }

    /// <summary>
    /// 任意のオブジェクトに型ベースのプロパティアクセサを提供するためのクラス
    /// </summary>
    /// <typeparam name="TKey">キーにするオブジェクトの型</typeparam>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <typeparam name="TReferenceMap">参照管理ディクショナリの型</typeparam>
    /// <typeparam name="TInstanceMap">インスタンス管理ディクショナリの型</typeparam>
    public class AccessorUtility<TKey, TValue, TReferenceMap, TInstanceMap> where TReferenceMap : BasicReferenceMap<TKey, TValue, TInstanceMap>, new() where TInstanceMap : BasicInstanceMap<TValue>, new() {

        /// <summary>
        /// インスタンスのディクショナリのディクショナリ
        /// </summary>
        /// <remarks>本当は ConditionalWeakTable を使いたいのだが、 .NET 4.0 以降の機能なので無理。</remarks>
        public static TReferenceMap instanceMapMap = new TReferenceMap();

    }

    /// <summary>
    /// 任意のオブジェクトに型ベースのプロパティアクセサを提供するためのクラス
    /// </summary>
    /// <remarks>参照管理ディクショナリを拡張しない場合はこちら</remarks>
    /// <typeparam name="TKey">キーにするオブジェクトの型</typeparam>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <typeparam name="TInstanceMap">インスタンス管理ディクショナリの型</typeparam>
    public class AccessorUtilityWithoutReferenceMap<TKey, TValue, TInstanceMap> : AccessorUtility<TKey, TValue, BasicReferenceMap<TKey, TValue, TInstanceMap>, TInstanceMap> where TInstanceMap : BasicInstanceMap<TValue>, new() {
    }

    /// <summary>
    /// 任意のオブジェクトに型ベースのプロパティアクセサを提供するためのクラス
    /// </summary>
    /// <remarks>インスタンス管理ディクショナリを拡張しない場合はこちら</remarks>
    /// <typeparam name="TKey">キーにするオブジェクトの型</typeparam>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <typeparam name="TReferenceMap">参照管理ディクショナリの型</typeparam>
    public class AccessorUtilityWithoutInstanceMap<TKey, TValue, TReferenceMap> : AccessorUtility<TKey, TValue, TReferenceMap, BasicInstanceMap<TValue>> where TReferenceMap : BasicReferenceMap<TKey, TValue, BasicInstanceMap<TValue>>, new() {
    }

    /// <summary>
    /// 任意のオブジェクトに型ベースのプロパティアクセサを提供するためのクラス
    /// </summary>
    /// <remarks>参照管理ディクショナリとインスタンス管理ディクショナリを拡張しない場合はこちら</remarks>
    /// <typeparam name="TKey">キーにするオブジェクトの型</typeparam>
    /// <typeparam name="TValue">値の型</typeparam>
    public class AccessorUtilityWithoutReferenceMapAndInstanceMap<TKey, TValue> : AccessorUtility<TKey, TValue, BasicReferenceMap<TKey, TValue, BasicInstanceMap<TValue>>, BasicInstanceMap<TValue>> {
    }

}
