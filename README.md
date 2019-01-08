# MEBY Dev Log
---
---
### 未完成
- 修正手把控制*無法使用*情形
- 手把讀取button Input Manager上 需補上是第幾個joystick 目前沒有可以確定joystick 是第幾個
- 調整角色間的數值
- 地圖的大小調整
- 藝術就是染色bug待修復
- Gotta gay gay設置位置bug
- 完成rebeeca動畫
- 所有角色染色動畫增加
- 修改最後結算畫面
- 調整camera跟角色大小
---
**181212**
- 修改IITem架構 增加保留其他使用者觸發後才作用的道具類別`IItem.cs`
  * 觸發型道具
  * 計時型道具
  * 即時型道具
- 新增道具**速速果實***fast fruit*`FastFruit.cs`
- 修改Moral Devil Idle 動畫
- 修正**Gotta gay gay**`TimeBomb.cs`在被安裝後還能被其他剛體推動的問題
- 補上註解
---
**181214**
- 手把使用logitech 無法偵測特定手把
---
**181215**
- 新增**噢~吐豆**`Bomb.cs`彈開角色的特性
- 修正角色在movePlatform無法感知到地板的存在
- 修改角色對地板的判定從圓形範圍變成點`PlayerMovement.cs`
- 修正角色地板的側邊判定
---
**181216**
- 增加火山的隨機性(有一定的機率可以打到下層的地形)火山的噴發物改成時間到後消逝 其中經過的所有地形有1/2的機率被染色
---
**181217**
- 道具**神奇嗨螺**初始`Conch.cs`
- conch 呼叫 mermaidMan設定初始位置 mermaidMan移動 並對路徑上的角色造成傷害 離開螢幕後 呼叫Conch重置ItemState  *Trigger Type*
---
**181219**
- `mermaidMan.cs` `Conch.cs`大致完成 可正常使用 尚未補上註解
- 完成道具**神奇嗨螺**
- 替`ItmeManager.cs`加上測試用function 設定testItem按下f1便可以測試
---
**181220**
- 修正角色在**MovePlatform**不會跟著platform移動(不可以直接對transform做移動 而必須透過rigidbody 物理引擎才可以計算相關變化)
---
**181222**
- 補上`Conch.cs` `MermaidMan.cs`Comment
---
**181225**
- 完成道具**迷路depon**
- 補上`Depon.cs` `PlayerMovemnt.cs` `IPlayer.cs`的註解
- 新增`SetStray`方法在`IPlayer`裡面 讓外部可以讓角色在移動時達到左右相反的目的
---
**181230**
- 完成道具**藝術就是染色**
---
**190101**
- 新增**ModeScene** 模式選擇
- 修改UI比例問題 *overlap by camera ui scale mode scale with screen size*
- **mode scene** ui 動畫補上
- **seleceted scene** ui 動畫 
- 補上註解
- 補上`ArtBomb.cs`comment
- 手把讀取button Input Manager上 需補上是第幾個joystick 目前沒有可以確定joystick 是第幾個
---
**190102**
- 完成攝影機的跟隨`MultipleTargetCamera.cs`[*Camera Code 參考資料 ](https://www.youtube.com/watch?v=aLpixrPvlB8)[*Bounds 類別 參考資料](https://docs.unity3d.com/ScriptReference/Bounds.html)
- 補上註解
---
**190103**
- 套上道具美術圖
---
**190106**
- 完成moral devil動畫
- 完成masanri動畫
- 完成pirate bear動畫
---
**190107**
- 修正moral devil 技能在空中施放的bug
- 新增地圖Volcano
- 調整道具大小
- 調整角色Scale
- Masanari Skill第一階段動畫調整
- PirateBear 攻擊動畫時間調整 0.45sec
- 調整跳躍物理運算 在每一次跳躍前將 velocity.y重製為0 再加上力
- 跳躍的add force中的forceMode 修改成forceMode2d.Impulse
- 藝術就是染色bug待修復
- paintball動畫 masanari已設定 尚未更新sprite
- 修改paintball使用規則 只可以在地上使用
---
**190108**
- 新增Masanari **Paint動畫**
- 新增PirateBear **Paint動畫**
- 新增Moral_Devil **Paint動畫**
- 更新UI設計
- 神奇嗨螺 海超人動畫問題
- 修正Pirate bear skill動畫問題
