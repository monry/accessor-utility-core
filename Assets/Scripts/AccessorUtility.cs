using System;
using System.Collections.Generic;

namespace AccessorUtility {

    /// <summary>
    /// 任意のオブジェクトのインスタンスをキーにした参照ディクショナリ
    /// </summary>
    /// <remarks>弱参照っぽい実装を Getter 内に施している</remarks>
    /// <typeparam name="TKey">キーにするインスタンスの型</typeparam>
    /// <typeparam name="TValue">インスタンス格納用のディクショナリ</typeparam>
    public class BasicRefernceMap<TKey, TValue> : Dictionary<TKey, TValue> where TValue : class, new() {

        public new TValue this[TKey key] {
            get {
                TValue value;
                this.TryGetValue(key, out value);
                if (value == default(TValue)) {
                    value = new TValue();
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
    public class AccessorUtility<TKey, TValue, TReferenceMap, TInstanceMap> where TReferenceMap : BasicRefernceMap<TKey, TInstanceMap>, new() where TInstanceMap : BasicInstanceMap<TValue>, new() {

        /// <summary>
        /// インスタンスのディクショナリのディクショナリ
        /// </summary>
        /// <remarks>本当は ConditionalWeakTable を使いたいのだが、 .NET 4.0 以降の機能なので無理。</remarks>
        internal static TReferenceMap instanceMapMap = new TReferenceMap();

    }

    /// <summary>
    /// 任意のオブジェクトに型ベースのプロパティアクセサを提供するためのクラス
    /// </summary>
    /// <remarks>インスタンス管理ディクショナリを拡張しない場合はこちら</remarks>
    /// <typeparam name="TKey">キーにするオブジェクトの型</typeparam>
    /// <typeparam name="TValue">値の型</typeparam>
    public class AccessorUtility<TKey, TValue> : AccessorUtility<TKey, TValue, BasicRefernceMap<TKey, BasicInstanceMap<TValue>>, BasicInstanceMap<TValue>> {
    }

}