using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager rManager;
    [SerializeField] private GameObject notes;
    private RhythmControls rControls;
    [SerializeField] private float multiplier, tempo, distance;
    private int atbCounter, atbBuildCounter, noteNbr;
    [SerializeField]private GameObject[] arrows = new GameObject[4];
    [SerializeField]private GameObject[] activators = new GameObject[4];
    [SerializeField] private AudioSource song;

    // Start is called before the first frame update
    private void Awake()
    {
        rManager = this;
        rControls = new RhythmControls();
        rControls.Rhythm.Switch.performed += SwitchToCombat;
        EnableRhythmControls();
        StartNotes();
        song.Play();
    }

    private void StartNotes()
    {
        for (int i = 0; i < 25; i++)
        {
            SpawnNote();
        }
    }

    public void SpawnNote()
    {
        int ranNum = Random.Range(0, 4);
        GameObject newArrow = Instantiate(arrows[ranNum], notes.transform);
        Vector3 pos = newArrow.transform.localPosition;
        pos.y += (noteNbr * distance);
        newArrow.transform.localPosition = pos;
        noteNbr++;
    }

    public void NoteHit()
    {
        ATBBuildup();
    }

    public void NoteMissed()
    {

    }

    public void BombHit()
    {
        Debug.Log("Ya hit a bomb!");
        if (atbCounter > 0)
        {
            SetATB(atbCounter - 1);
            UIManager.uiManager.UpdateATBNumber(atbCounter, atbCounter + 1);
        }
        UIManager.uiManager.StartShake();
    }

    public void PlaceBomb()
    {
        notes.transform.GetChild(4).GetComponent<NoteObj>().SetAsBomb();
    }

    public void SetATB(int atb)
    {
        atbCounter = atb;
        Debug.Log("Atb is at : " + atb.ToString());
    }

    public float GetTempo()
    {
        return tempo / 60;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }

    private void EnableRhythmControls()
    {
        for (int i = 0; i < activators.Length; i++)
        {
            activators[i].SetActive(true);
        }
        rControls.Rhythm.Enable();
    }

    public void ContinueRhythm()
    {
        EnableRhythmControls();
    }

    private void SwitchToCombat(InputAction.CallbackContext obj)
    {
        if(atbCounter >= 1)
        {
            DisableRhythmControls();
            BattleManagerRedux.bManager.StartCombat(atbCounter);
            UIManager.uiManager.ChangeSwitchDirection();
        }
    }

    private void DisableRhythmControls()
    {
        for (int i = 0; i < activators.Length; i++)
        {
            activators[i].SetActive(false);
        }
        rControls.Rhythm.Disable();
    }

    private void ATBBuildup()
    {
        if (atbCounter < 5)
        {
            atbBuildCounter++;
            if (atbBuildCounter == 14)
            {
                atbBuildCounter = 0;
                atbCounter++;
                UIManager.uiManager.UpdateATBNumber(atbCounter, atbCounter - 1);
            }
            UIManager.uiManager.UpdateATB(atbBuildCounter);
        }
    }
}