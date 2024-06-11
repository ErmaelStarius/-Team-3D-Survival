<div align = 'center'>
  
# Unknown Land

</br>
</br>
</br>

</div>

<div align = 'center'>


## 🧏‍♀️ 프로젝트 개요 💁‍♂️

Unity 3D기반으로 이루어진 생존형 게임

가공할 자원과 식량을 얻고, 적과 싸우며 생존해 나가는 게임이다.

</br>
</br>

### 개발 환경　　　　　　　개발 언어　　　　　　개발 엔진
   
<img src="https://img.shields.io/badge/Vscode-0076b8.svg?style=for-the-badge&logo=visualstudio&logoColor=efebe0"/>　　 　　　　　<img src="https://img.shields.io/badge/C sharp-4c2889.svg?style=for-the-badge&logo=Csharp&logoColor=efebe0"/>　　　　 　　 <img src="https://img.shields.io/badge/Unity-FFFFFF.svg?style=for-the-badge&logo=Unity&logoColor=000000"/>

</div>

</br>
</br>

<div align = 'center'>
  
### 🔍 팀원 소개 　　

</br>

|이름|구성|역할|
|:------:|:------:|:------:|
|최도규|팀 장| ㅁㅁㅁ |
|안후정|팀 원| 식사와 수분 관리, 음악 |
|강수지|팀 원| 온도 조절, 지역 환경 설정, 날씨 변경 |
|최민석|팀 원|인벤토리, 자원 생성 및 가공|
|이지훈|팀 원|적 생성 및 전투시스템|

</div>

   </br>
   </br>
   </br>
   </br>

### 🤔 주요 기능 단편코드

<details>
　　<summary> .... </summary>
<div markdown="1">       

```csharp
....
```
</div>
</details>

<details>
　　<summary> .... </summary>
<div markdown="1">       

```csharp
....
```
</div>
</details>

<details>
　　<summary> .... </summary>
<div markdown="1">       

```csharp
....
```
</div>
</details>

<details>
　　<summary> 건물 크래프팅 </summary>
<div markdown="1">       

```csharp
bool CanCraft()
{
    for (int i = 0; i < selectedItem.materials.Length; i++)
    {
        if (inventory.GetItemQuantity(selectedItem.materials[i].materialName) < selectedItem.materials[i].value)
        {
            return false;
        }
    }

    return true;
}

void ArchitectureCraft()
{
    if (!CanCraft()) return;

    ItemSlot slot = inventory.GetEmptyArchitectureSlot();

    if(slot == null) return;

    for (int i = 0; i < selectedItem.materials.Length; i++)
    {
        inventory.SubItemQuantity(selectedItem.materials[i].materialName, selectedItem.materials[i].value);
    }

    slot.item = selectedItem;
    slot.item.icon = selectedItem.icon;

    inventory.UpdateUI();
    
    ClearSelectedItemWindow();
}
```
</div>
</details>

<details>
　　<summary> Prefab 로딩 </summary>
<div markdown="1">       

```csharp
private void Start()
{
    resourceRock = Resources.Load<GameObject>("Resource_Rock");
    resourceTree = Resources.Load<GameObject>("Resource_Tree");

    Instantiate(resourceRock);
    Instantiate(resourceTree);
}
```
</div>
</details>

<details>
　　<summary> 📕 독립적인 리스폰 구현 </summary>
<div markdown="1">       

```csharp
IEnumerator Spawn(EnemyData enemyData)
    {
        Instantiate(enemyData.spawnPrefab, enemyData.transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(0.1f);
    }
```
</div>
</details>





</br>
</br>
</br>
</br>


  
### 　　　　　　　　　　　　📸 인게임 화면

</br>
</br>
</br>

<div align = 'center'>

|타이틀 화면|
|:------:|
|![타이틀 화면]( ... )|

</br>

|메인 화면| 
|:------:|
![메인 화면]( ... )|

</br>

|상호 작용|
|:------:|
|![게임 오버]( ... )|

</br>

|스테이지 클리어| 
|:------:|
|![스테이지 클리어]( ... )|

</div>
