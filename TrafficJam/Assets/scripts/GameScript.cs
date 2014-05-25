using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {

	public RoadMap road; //Object to manage the map
	public	List<Vehicle> vehicleBag; //Cats bag where al the cars are in
	public Vehicle car, truck;//Prefabs

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

	float waitCarry = 0f, limitCarry = 0f,
		waitCarry2 = 0f, limitCarry2 = 0f,
		waitCarry3 = 0f, limitCarry3 = 0f;

	void Update () {
		//Column 1
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
			oldSpawn = copyOfVehicle;
			waitCarry = 0f;
			limitCarry = 0f;
		}
		waitCarry += Time.deltaTime;

		//Column 2
		if (limitCarry2 <= 0f) {
			limitCarry2 = Random.Range(MIN_GAP_BETWEEN_VEHICLES, MAX_GAP_BETWEEN_VEHICLES);
		}
		
		if (waitCarry2 >= limitCarry2) {
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
			vehicle.channel = CENTER_COLUMN;
			vehicle.color = Random.Range(Vehicle.RED, Vehicle.GREEN+1);
			Vehicle copyOfVehicle = Instantiate(vehicle, new Vector3(CENTER_ROW_AXIS, START_ROAD_Y_AXIS, 0f), Quaternion.identity) as Vehicle;
			vehicleBag.Add (copyOfVehicle);
			copyOfVehicle.paused = false;
			oldSpawn = copyOfVehicle;
			waitCarry2 = 0f;
			limitCarry2 = 0f;
		}
		waitCarry2 += Time.deltaTime;

		//Column 3
		if (limitCarry3 <= 0f) {
			limitCarry3 = Random.Range(MIN_GAP_BETWEEN_VEHICLES, MAX_GAP_BETWEEN_VEHICLES);
		}
		
		if (waitCarry3 >= limitCarry3) {
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
			vehicle.channel = RIGHT_COLUMN;
			vehicle.color = Random.Range(Vehicle.RED, Vehicle.GREEN+1);
			Vehicle copyOfVehicle = Instantiate(vehicle, new Vector3(RIGHT_ROW_AXIS, START_ROAD_Y_AXIS, 0f), Quaternion.identity) as Vehicle;
			vehicleBag.Add (copyOfVehicle);
			copyOfVehicle.paused = false;
			oldSpawn = copyOfVehicle;
			waitCarry3 = 0f;
			limitCarry3 = 0f;
		}
		waitCarry3 += Time.deltaTime;
	}

	Vehicle oldSpawn;


}
