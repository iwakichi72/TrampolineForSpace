# TrampolineForSpace


## ゲーム概要
トランポリンで宇宙を目指すゲームです。

1. トランポリンの反動を利用して、`JumpPower `をタイミングよく解放すると、より高くジャンプすることができます。  
⇒上昇時に`JumpPower `を解放すればジャンプの加速度が上がりますが、下降時に解放してしまうと加速度が下がってしまうので注意が必要です。  
制限時間以内に出来るだけ高くジャンプしましょう。
1. 空中にアイテム（星）が配置してあり、取るとジャンプの加速度を上げてくれます。  
ただ逆に下降時に取ってしまうと加速度を下げてしまうので、これにも注意が必要です。  
各色のトランポリンを移動しながら、積極的にアイテムを取得していきましょう。
1. タイムアップもしくはゴールをすると、「飛んだ高さ・取得アイテム数・ゴールしたか否か」でスコアが計算されて表示されます。  
スコアはランキング形式でデータベースに記憶されており、ランキング圏内であれば名前とスコアが記録されます。

## 操作方法
1. JUMP!ボタン：押し込むと`JumpPower `が溜まり、離すと`JumpPower `の大きさだけ高くジャンプする。
1. MOVE!ボタン：各色トランポリンの位置に移動する。

## 使用環境
- Unity (2019.4.7f1)
- XAMPP ver3.2.4 / Apache ver2.4.43 /MySQL ver15.1 / PHP ver7.2.32
- CakePHP 3, phpMyAdmin