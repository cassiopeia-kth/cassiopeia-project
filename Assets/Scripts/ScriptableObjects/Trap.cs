using UnityEngine;

[CreateAssetMenu(menuName = "Trap")]

public class Trap : ScriptableObject {

public string trapName;

public Sprite sprite;

public int quantity;

public bool stackable;

public enum TrapType
{
HADES,
POSEIDON,
ZEUS,
HERMES,
SPIKE,
FIRE
}

public TrapType trapType;
}
