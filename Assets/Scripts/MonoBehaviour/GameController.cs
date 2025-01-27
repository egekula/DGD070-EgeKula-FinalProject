using TMPro;
using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {
    private Systems _systems;
    private EntityViewCreator _entityCreator;
    public TextMeshProUGUI winText; 

    void Start() {
        Time.timeScale = 1; 
        _entityCreator = GetComponent<EntityViewCreator>();
        _systems = new Feature("Systems")
            .Add(new InputSystem(Contexts.sharedInstance))
            .Add(new MovementSystem(Contexts.sharedInstance))
            .Add(new BoundarySystem(Contexts.sharedInstance))
            .Add(new CollisionSystem(Contexts.sharedInstance))
            .Add(new PadTriggerSystem(Contexts.sharedInstance))
            .Add(new WinSystem(Contexts.sharedInstance));

        _entityCreator.CreatePlayer(Vector3.zero);
        _entityCreator.CreatePad(new Vector3(-8f, 0f, 4f), 0);
        _entityCreator.CreatePad(new Vector3(8f, 0f, 4f), 1);
        _entityCreator.CreatePad(new Vector3(-8f, 0f, -4f), 2);
        _entityCreator.CreatePad(new Vector3(8f, 0f, -4f), 3);
    }

    void Update() {
        _systems.Execute();
    }

    void OnDestroy() {
        _systems.TearDown();
    }

    public void ShowWinMessage() {
        winText.gameObject.SetActive(true);
        winText.text = "A WINRAR IS YOU";
        Time.timeScale = 0; 
    }
}