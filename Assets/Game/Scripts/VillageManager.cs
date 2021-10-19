using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour, IObserver
{
    [SerializeField] private List<PointOfInterest> pois = new List<PointOfInterest>();
    [SerializeField] private List<Tile> tiles = new List<Tile>();
    [SerializeField] private List<TileData> tileDatas = new List<TileData>();

    private void Awake()
    {
        RegisterObserver();
    }

    private void OnDisable()
    {
        UnregisterObserver();
    }

    private void RegisterObserver()
    {
        foreach (PointOfInterest poi in pois) poi.Subscribe(this);
    }

    private void UnregisterObserver()
    {
        foreach (PointOfInterest poi in pois) poi.Unsubscribe(this);
    }

    private void UpggradeVillage(string tileID)
    {
        if (tiles.Exists(x => x.tileID.Equals(tileID)) && tileDatas.Exists(x => x.tileID.Equals(tileID)))
        {
            Tile tile = tiles.Find(x => x.tileID.Equals(tileID));
            TileData tileData = tileDatas.Find(x => x.tileID.Equals(tileID));
            tile.tile.sprite = tileData.tileImage;
        }
    }

    void IObserver.OnNotify(string sender, string messagge)
    {
        
    }

    void IObserver.OnNotify(string messagge)
    {
        UpggradeVillage(messagge);
    }

    [System.Serializable]
    public class TileData
    {
        public Sprite tileImage;
        public string tileName;
        public string tileID;
    }

    [System.Serializable]
    public class Tile
    {
        public SpriteRenderer tile;
        public string tileID;
    }
}
