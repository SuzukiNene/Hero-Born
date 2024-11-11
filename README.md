# 書籍
- Unity 3Dゲーム開発ではじめるC#プログラミング
	- https://www.amazon.co.jp/Unity-3D%E3%82%B2%E3%83%BC%E3%83%A0%E9%96%8B%E7%99%BA%E3%81%A7%E3%81%AF%E3%81%98%E3%82%81%E3%82%8BC-%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0-impress-gear/dp/4295012459/ref=tmm_pap_swatch_0?_encoding=UTF8&qid=&sr=

# 原著サンプルコード
- Learning C# by Developing Games with Unity 2020
	- https://github.com/PacktPublishing/Learning-C-8-by-Developing-Games-with-Unity-2020?tab=readme-ov-file

# スクリーンショット
![Hero-Born](https://github.com/user-attachments/assets/441e1960-fb41-4e66-9057-99099734af29)

# 操作方法
- WASDキーまたは上下左右の矢印キーで、前後の移動と左右の方向転換を行います。
- マウス左ボタン押下で、弾を発射します。
- Spaceキーでジャンプします。
- フィールドの四隅にある回転するアイテムをすべて取得すると、勝ち。
- 敵(Enemy)に見つかると追いかけてくる。追いつかれるとHPが減り、０になれば負け。

# オリジナルからの変更点
- Enemyをプレハブ化して、試合開始時に塔の上に出現するようにした。
　また、弾を受けてEnemyが消えた時に、新たなEnemyが再度出現を繰り返すようにした。
- Enemyは、Playerを見つけた時にPlayerが居た「座標」に対して向かってくるだけだったが、
　OnTriggerStay()を実装して、逃げるPlayerを追うように変更した。
- Enemyは、発見したPlayerを追っている間は、ボディーの色を変更するようにした。
- Enemyは、追っていたPlayerが検知範囲から逃げ出した時、直前に目指していたパトロールルートに戻るようにした。
- Playerは、ジャンプ処理はFixedUpdate()ではなくUpdate()の時点で実行するようにした。
- Playerの撃つ弾が、物体をすり抜けてしまう場合があったので、BulletのRigidbody->Collision Detection=Continuousに変更した。

# 開発環境
- Unity 2020.3.48f1

EOF
