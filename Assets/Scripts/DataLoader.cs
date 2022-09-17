using UnityEngine;
using UnityEngine.AddressableAssets;//Addressablesを使うのに必要
using UnityEngine.UI;//UIを使うのに必要

public class DataLoader : MonoBehaviour
{
    [SerializeField] Image Img;

    async void Start()
    {
        //Spriteをロードする
        var sprite = await Addressables.LoadAssetAsync<Sprite>("Luffy.png").Task;

        //UIに反映
        Img.sprite = sprite;

        //使い終わったらメモリから開放する
        //Addressables.Release(sprite); <<今回は使わない
    }
}