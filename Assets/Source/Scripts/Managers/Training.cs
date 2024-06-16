using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Training : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Phase[] _trainingPhases = {
        Phase.A, Phase.D, Phase.W,
        Phase.S, Phase.Q, Phase.E,
        Phase.LeftArrow, Phase.RightArrow,
        Phase.UpArrow, Phase.DownArrow,
        Phase.Space, Phase.Ctrl
    };

    private int _currentPhaseIndex = -1;
    private float _keyHoldStartTime;

    void Start()
    {
        SetNextPhase();
    }

    void Update()
    {
        if (_currentPhaseIndex >= 0 && _currentPhaseIndex < _trainingPhases.Length)
        {
            KeyCode currentKeyCode = GetKeyCodeForPhase(_trainingPhases[_currentPhaseIndex]);
            if (Input.GetKeyDown(currentKeyCode))
            {
                _keyHoldStartTime = Time.time;
            }
            else if (Input.GetKey(currentKeyCode) && Time.time - _keyHoldStartTime >= 1.5f)
            {
                SetNextPhase();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Docking");
        }
    }

    private void SetNextPhase()
    {
        _currentPhaseIndex++;
        if (_currentPhaseIndex < _trainingPhases.Length)
        {
            _text.text = $"Удерживайте: {_trainingPhases[_currentPhaseIndex].ToString()}";
        }
        else
        {
            _text.text = "Для перехода на следующий уровень нажмите Enter";
        }
    }

    private KeyCode GetKeyCodeForPhase(Phase phase)
    {
        switch (phase)
        {
            case Phase.A: return KeyCode.A;
            case Phase.D: return KeyCode.D;
            case Phase.W: return KeyCode.W;
            case Phase.S: return KeyCode.S;
            case Phase.Q: return KeyCode.Q;
            case Phase.E: return KeyCode.E;
            case Phase.LeftArrow: return KeyCode.LeftArrow;
            case Phase.RightArrow: return KeyCode.RightArrow;
            case Phase.UpArrow: return KeyCode.UpArrow;
            case Phase.DownArrow: return KeyCode.DownArrow;
            case Phase.Space: return KeyCode.Space;
            case Phase.Ctrl: return KeyCode.LeftControl;
            default: return KeyCode.None;
        }
    }

    public enum Phase
    {
        A, D, W, S, Q, E, LeftArrow, RightArrow, UpArrow, DownArrow, Space, Ctrl
    }
}
