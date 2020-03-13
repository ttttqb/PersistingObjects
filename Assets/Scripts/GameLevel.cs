using UnityEngine;

public partial class GameLevel : PersistableObject {

	[SerializeField]
	SpawnZone spawnZone;
    public static GameLevel Current { get; private set; }

	[UnityEngine.Serialization.FormerlySerializedAs("persistentObjects")]
	[SerializeField]
	GameLevelObject[] levelObjects;

	[SerializeField]
	int populationLimit;

	public int PopulationLimit {
		get {
			return populationLimit;
		}
	}

	// void Start () {
	// 	Game.Instance.SpawnZoneOfLevel = spawnZone;
	// }
    // public Vector3 SpawnPoint {
	// 	get {
	// 		return spawnZone.SpawnPoint;
	// 	}
	// }

    void OnEnable () {
		Current = this;
        if (levelObjects == null) {
			levelObjects = new GameLevelObject[0];
		}
	}

	public void SpawnShapes () {
		spawnZone.SpawnShapes();
	}

	public override void Save (GameDataWriter writer) {
		writer.Write(levelObjects.Length);
		for (int i = 0; i < levelObjects.Length; i++) {
			levelObjects[i].Save(writer);
		}
	}

	public override void Load (GameDataReader reader) {
		int savedCount = reader.ReadInt();
		for (int i = 0; i < savedCount; i++) {
			levelObjects[i].Load(reader);
		}
	}

	public void GameUpdate () {
		for (int i = 0; i < levelObjects.Length; i++) {
			levelObjects[i].GameUpdate();
		}
	}


}