using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {

	public	List<Vehicle> vehicleBag;

	public List<TileScript> tiles;

	public Vehicle car, truck;

	public const float LEFT_ROW_AXIS = -1.030326f;
	public const float CENTER_ROW_AXIS = -0.09628651f;
	public const float RIGHT_ROW_AXIS = 0.8860641f;

	public const float START_ROAD_Y_AXIS = 2.864136f;
	public const float END_ROAD_Y_AXIS = -2.595164f;

	void Start () {
		if (car == null || truck == null) {
			throw new UnityException("car or truck prefabs cannot be null!.");
		}
		vehicleBag = new List<Vehicle> ();
	}

	public const int LEFT_COLUMN = 1;
	public const int CENTER_COLUMN = 2;
	public const int RIGHT_COLUMN = 3;

	public const float MIN_GAP_BETWEEN_VEHICLES = 2f;
	public const float MAX_GAP_BETWEEN_VEHICLES = 4f;

	float waitCarry = 0f, limitCarry = 0f;

	public void RenderMap(Vector3 p, float height) {
		foreach (TileScript tile in tiles) {
			if ( (p.x <= tile.GetRightX()) && (p.x >= tile.GetLeftX()) && (p.y <= tile.GetTopY()) && (p.y >= tile.GetBottomY()) ) {
				TileScript neighboor = null;
				switch (tile.column) {
					case LEFT_COLUMN:
						neighboor = GetTile(tile.row, tile.column + 1);
					break;
					case CENTER_COLUMN:
						neighboor = GetTile(tile.row, tile.column + 1);
					break;
					case RIGHT_COLUMN:
						neighboor = GetTile(tile.row, tile.column - 1);
					break;
				}
				if (neighboor == null)
					throw new UnityException("neighboor cannot be null!.");
				tile.SetMixColor(neighboor.color, (neighboor.column > tile.column));
				for (int yIndex = tile.row - 1; yIndex < 1; yIndex--) {
					TileScript iterTile = GetTile(yIndex, tile.column);
					iterTile.ChangeFullColor(neighboor.color);
				}
			}
		}
	}

	public TileScript GetTile(int row, int col) {
		return tiles.Find (tile => tile.column == col && tile.row == row);
	}

	void Update () {
		if (limitCarry <= 0f) {
			limitCarry = Random.Range(MIN_GAP_BETWEEN_VEHICLES, MAX_GAP_BETWEEN_VEHICLES);
		}
		if (waitCarry >= limitCarry) {
			Vehicle vehicle = null;
			int randomVehicle = Random.Range(Vehicle.CAR_TYPE, Vehicle.TRUCK_TYPE+1);
			switch (randomVehicle) {
				case Vehicle.CAR_TYPE:
					vehicle = car;
				break;
				case Vehicle.TRUCK_TYPE:
					vehicle = truck;
				break;
			} 
			vehicle.channel = LEFT_COLUMN;
			vehicle.color = Random.Range(Vehicle.RED, Vehicle.GREEN+1);
			Vehicle copyOfVehicle = Instantiate(vehicle, new Vector3(LEFT_ROW_AXIS, START_ROAD_Y_AXIS, 0f), Quaternion.identity) as Vehicle;
			vehicleBag.Add (copyOfVehicle);
			copyOfVehicle.paused = false;
			waitCarry = 0f;
			limitCarry = 0f;
		}
		waitCarry += Time.deltaTime;

	}
	

}
