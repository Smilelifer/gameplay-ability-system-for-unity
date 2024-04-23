using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class TestTileMap : MonoBehaviour
{
    Grid grid;
    Tilemap tilemap;
    TilemapRenderer tilemapRenderer;
    SpriteRenderer spriteRenderer;
    SortingGroup sortingGroup;
    SpriteAtlas spriteAtlas;
    // Start is called before the first frame update

    [Button("获取瓦片尺寸")]
    public void GetTilemapSize(Tilemap tilemap)
    {
        Debug.Log("Tilemap Size: " + tilemap.size.ToString());       
    }

    public Sprite[] sprites;
    [Button]
    public void GetAllSprite(SpriteAtlas spriteAtlas)
    {
        spriteAtlas.GetSprites(sprites);
    }

    [OnValueChanged("OnTargetChanged")]
    public Transform target;
    [SerializeField]
    public Vector3 testVector3;

    private void OnTargetChanged()
    {
        testVector3 = target.position;
    }
}
