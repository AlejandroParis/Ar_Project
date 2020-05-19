using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject canvas;
    public GameObject piece_buttons;
    public GameObject level_selection;
    public GameObject run_button;
    public GameObject pause_button;
    public GameObject win_panel;

    enum CONTROL_BUTTONS { DEPLOY = 0, RUN, PAUSE};
    // Start is called before the first frame update
    void Start()
    {
        //canvas.transform.GetChild((int)CONTROL_BUTTONS.RUN).gameObject.SetActive(false);
        // canvas.transform.GetChild((int)CONTROL_BUTTONS.PAUSE).gameObject.SetActive(false);
        DisableGUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OnReachEnd()
    {
        win_panel.SetActive(true);
        win_panel.transform.GetChild(3).GetComponent<Button>().interactable = LevelManager.Instance.HasNextLevel();         
    }

    public void OnPieceButtonClick(int type)
    {
        PieceManager.Instance.piece_spawner.SetSpawnerType((Piece.PieceType)type);
    }

    internal void OnRun()
    {
        piece_buttons.SetActive(false);
    }

    internal void OnDeployment()
    {
        piece_buttons.SetActive(true);
        UpdateAvailablePiecesButtons();
    }

    public void NextLevelButton()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.selected_level + 1);
        win_panel.SetActive(false);
    }

    public void RestartButton()
    {
        win_panel.SetActive(false);
        GameManager.Instance.Restart();
    }

    public void MenuButton()
    {
        win_panel.SetActive(false);
        DisableGUI();
        EnableLevelSelection();
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void UpdateAvailablePiecesButtons()
    {
        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Block) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Block).gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Block).gameObject.GetComponent<Button>().interactable = false;
        }

        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Portal) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Portal).gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Portal).gameObject.GetComponent<Button>().interactable = false;
        }

        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.SpeedBoost) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.SpeedBoost).gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.SpeedBoost).gameObject.GetComponent<Button>().interactable = false;
        }

        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Trampoline) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Trampoline).gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Trampoline).gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void DisableLevelSelection()
    {
        level_selection.SetActive(false);
    }

    public void EnableLevelSelection()
    {
        level_selection.SetActive(true);
    }

    public void DisableGUI()
    {
        piece_buttons.SetActive(false);
        run_button.SetActive(false);
        pause_button.SetActive(false);
    }

    public void EnableGui()
    {
        piece_buttons.SetActive(true);
        run_button.SetActive(true);
        pause_button.SetActive(true);
    }

    public void SelectLevel(int index)
    {
        DisableLevelSelection();
        EnableGui();
        LevelManager.Instance.LoadLevel(index - 1);
    }
}
