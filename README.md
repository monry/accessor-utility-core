# What?

アクセサ (もどき) として任意のオブジェクトからアクセスさせる際の型ベースのデータストアを提供します。

# Why?

* `ConditionalWeakTable` が使いたい！
* しかし Unity は2017年7月現在、 .NET 3.5 系である
    * Experimental として .NET 4.6 を選択可能ではある
* 無いなら作っちまおう！

# Install

```shell
$ npm install github:umm-projects/accessor_utility_core.git
```

# Usage

* 正直、このパッケージだけでは殆ど何もできません。
* こいつが提供する `AccessorUtility` を継承したクラスをこさえてください。
* んで、それっぽい interface でも書いて、それの拡張クラスとしてアクセサもどきを実装します。

```csharp
using UnityEngine;
using AccessorUtility;

// 別に継承しなくても良いんだけど、毎回 Generic 書くのダルいので
public class SampleAccessor : AccessorUtility<MonoBehaviour, Component> {}

// interface として提供するコトで、ちょっと DI っぽく実装できます
public interface IRectTransformAccessor {
}

public static class RectTransformAccessorExtension {

    // この拡張メソッド内でデータストアにインスタンスを保持します
    // 型ベースなので、複数のキーを持たせて…、みたいなのは無理っす
    public static RectTransform RectTransform(this IRectTransformAccessor self) {
        // ココが強引なんだよなぁ…。
        MonoBehaviour instance = (MonoBehaviour)self;
        return SampleAccessor.instanceMapMap[instance][typeof(RectTransform)];
    }

}

public class Hoge : MonoBehaviour, IRectTransformAccessor {

    public void Start() {
        Debug.Log(this.RectTransform().sizeDelta);
    }

}
```

# License

Copyright (c) 2017 Tetsuya Mori

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)

