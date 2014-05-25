using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadMap : MonoBehaviour {

	public List<TileScript> tiles;
	public GameScript gs;

	public List<TileScript> tilesInLeftColumn;
	public List<TileScript> tilesInCenterColumn;
	public List<TileScript> tilesInRightColumn;

	public void OnTileClicked(TileScript tileClicked) {
		int newColor = tileClicked.GetNextColor();
		switch (tileClicked.column) {
		case 1:

			foreach (TileScript t in tilesInLeftColumn) {
				t.ChangeFullColor(newColor);
			}
			foreach (Vehicle v in gs.vehicleBag.FindAll(vec => (vec.channel == 1) && (vec.color == newColor))) {
				gs.vehicleBag.Remove(v);
				Destroy (v.gameObject);
			}
			break;
		case 2:
			foreach (TileScript t in tilesInCenterColumn) {
				t.ChangeFullColor(newColor);
			}
			foreach (Vehicle v in gs.vehicleBag.FindAll(vec => (vec.channel == 2) && (vec.color == newColor))) {
				Debug.Log ("sarasasss");
				gs.vehicleBag.Remove(v);
				Destroy (v.gameObject);}
			break;
		case 3:
			foreach (TileScript t in tilesInRightColumn) {
				t.ChangeFullColor(newColor);
			}
			foreach (Vehicle v in gs.vehicleBag.FindAll(vec => (vec.channel == 3) && (vec.color == newColor))) {
				Debug.Log ("sarasasss");
				gs.vehicleBag.Remove(v);
				Destroy (v.gameObject);
			}
			break;
		}
	}



	public void RenderMap(Vector3 p, float height) {
		foreach (TileScript tile in tiles) {
			if ( (p.x <= tile.GetRightX()) && (p.x >= tile.GetLeftX()) && (p.y <= tile.GetTopY()) && (p.y >= tile.GetBottomY()) ) {
				/*TileScript neighboor = null;
				switch (tile.column) {
				case GameScript.LEFT_COLUMN:
					neighboor = GetTile(tile.row, tile.column + 1);
					break;
				case GameScript.CENTER_COLUMN:
					neighboor = GetTile(tile.row, tile.column + 1);
					break;
				case GameScript.RIGHT_COLUMN:
					neighboor = GetTile(tile.row, tile.column - 1);
					break;
				}
				if (neighboor == null)
					throw new UnityException("neighboor cannot be null!.");
				tile.SetMixColor(neighboor.color, (neighboor.column > tile.column));
				for (int yIndex = tile.row - 1; yIndex >= 1; yIndex--) {
					Debug.Log("color"+neighboor.color);
					TileScript iterTile = GetTile(yIndex, tile.column);
					iterTile.ChangeFullColor(neighboor.color);
				}*/
			}
		}
	}

	public TileScript GetTile(int row, int column) {
		return tiles.Find (var => var.row == row && var.column == column);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
