# Circla_Scripts

## 概要

unityroomにて公開中の2レーン音ゲー"Circla"のソースコード部分です。

https://unityroom.com/games/circla

## 主な使用ライブラリ

- DOTween
- UniRx
- UniTask
- LucidAudio

## 設計

特にこのアーキテクチャを目指そうといった指針は無く（そもそも実力不足）、とりあえず以下のルールをできるだけ守ることを意識しました。

- Model、ロジック部分の分離
  - できるだけModelはViewと分けて記述する
  - ModelはMonoBehaviourをできるだけ継承しない
  - ロジックは`Models/`になるべく記述する
- UI部分はMVPでなるべく作成する
  - Model, View, Presenterが連結し合い、Rxの機能を利用してリアクティブにModel, Viewの変化が反映されるようにする
- InputはInputProviderに全部記述する
- できるだけSOLID原則を守る
  - OCPむずい
- シーン実行時の生成はEntryPointが担う
