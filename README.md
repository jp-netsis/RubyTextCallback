# Ruby(Furigana) Text Callback

UnityのUIElementのTextElement継承オブジェクトに振り仮名(ふりがな、フリガナ、ルビ)タグを追加します。

Add furigana (furigana, furigana, ruby) tags to the TextElement inherited object in Unity's UIElement.

チェックしたUnityバージョンは以下の通りです。

I checked Unity Version are below.

UnityVer:2022.3.13f1 

# Changes

### ver 0.1

[Ja]
追加 : `RubyTextElementCallback`

[En]
Added : `RubyTextElementCallback`

# Features
### Realtime Ruby Text
あなたは`<ruby=にほんご>日本語</ruby>`タグもしくは省略した`<r=にほんご>日本語</r>`タグを使用できます。
また、半角ダブルクォーテーションで囲っても動作します。
`<ruby="にほんご">日本語</ruby>`タグも`<r="にほんご">日本語</r>`タグもOKです。

You can use `<ruby=ice>fire</ruby>` tag or `<r=ice>fire</r>` tag.  Both are the same.
It can also work with double quotes.
`<ruby="ice">fire</ruby>` tag or `<r="ice">fire</r>` tag.

# How To Use

[Ja]

GitHubからインストールをすることが可能です。

[En]

There is a way to install from GitHub.

[Install]

Unity > Window > PackageManager > + > Add package from git url... > Add the following

`https://github.com/jp-netsis/RubyTextCallback.git?path=/Assets/RubyTextCallback#v0.1.0`

[Demo]

パッケージマネージャにデモプロジェクトが入っています。

The demo project is in the package manager.

## Usage Description

`<ruby=かんじ>漢字</ruby>`

### RubyShowType

[Ja]

RUBY_ALIGNMENT : ルビに合わせて文字を表示します

BASE_ALIGNMENT : 元の文字に合わせて文字を表示します

BASE_NO_OVERRAP_RUBY_ALIGNMENT : 基本は元の文字に合わせて文字を表示しますが、ルビが重なりあうときはずらします。また、枠内をルビがはみ出す場合も補正します。

[En]

RUBY_ALIGNMENT : display characters according to ruby

BASE_ALIGNMENT : display characters according to the original

BASE_NO_OVERRAP_RUBY_ALIGNMENT : Basically, the text is aligned with the original text, but when ruby characters overlap, they are shifted. If the ruby characters exceed the frame, it will be corrected.

### rubyLineHeight

この機能により、rubyを使用しない場合でも、同じ隙間を持つことができます。
この文字列を空にすることで、この機能はスキップされます。

This function allows you to have the same gap even if you don't use ruby.
Empty this string to skip this feature.

# Known Issues

RubyTextElementCallbackは開発中ですが、RubyTexxtMeshProと同じアルゴリズムを使用します。

This feature is under development, but uses the same algorithm as RubyTexxtMeshPro.

# Other

TextElement がルビタグに対応したらこのプロジェクトは削除します。

Once TextElement supports ruby tags, this project will be deleted.

# Contribution

すべての貢献を歓迎します。必ずプロジェクトのコードスタイルに従ってください。

All contributions are welcomed. Just make sure you follow the project's code style.  

Contact: jenomoto@netsis.jp

