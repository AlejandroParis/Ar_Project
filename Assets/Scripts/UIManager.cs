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

    AudioSource ui_audio;

    [Header("Clips")]
    public AudioClip victory_sound;
    public AudioClip button_sound;
    public AudioClip reset_sound;

    enum CONTROL_BUTTONS { DEPLOY = 0, RUN, PAUSE};
    // Start is called before the first frame update
    void Start()
    {
        ui_audio = canvas.GetComponent<AudioSource>();
        DisableGUI();
    }

    internal void OnReachEnd()
    {
        ui_audio.PlayOneShot(victory_sound, 1.3f);
        win_panel.SetActive(true);
        win_panel.transform.GetChild(3).GetComponent<Button>().interactable = LevelManager.Instance.HasNextLevel();         
    }

    internal void OnPause()
    {
        ui_audio.PlayOneShot(button_sound);
    }

    public void OnPieceButtonClick(int type)
    {
        ui_audio.PlayOneShot(button_sound);
        PieceManager.Instance.piece_spawner.SetSpawnerType((Piece.PieceType)type);
    }

    internal void OnRun()
    {
        ui_audio.PlayOneShot(button_sound);
        piece_buttons.SetActive(false);
    }

    internal void OnRestart()
    {
        ui_audio.PlayOneShot(reset_sound, 2.0f);
    }

    internal void OnDeployment()
    {
        piece_buttons.SetActive(true);
        UpdateAvailablePiecesButtons();
    }

    public void NextLevelButton()
    {
        ui_audio.PlayOneShot(button_sound);
        LevelManager.Instance.LoadLevel(LevelManager.Instance.selected_level + 1);
        win_panel.SetActive(false);
    }

    public void RestartButton()
    {
        ui_audio.PlayOneShot(button_sound);
        win_panel.SetActive(false);
        GameManager.Instance.Restart();
    }

    public void MenuButton()
    {
        ui_audio.PlayOneShot(button_sound);
        win_panel.SetActive(false);
        DisableGUI();
        EnableLevelSelection();
    }

    public void ExitApp()
    {
        ui_audio.PlayOneShot(button_sound);
        Application.Quit();
    }

    public void UpdateAvailablePiecesButtons()
    {
        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Block) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Block).gameObject.GetComponent<Button>().interactable = true;
            piece_buttons.transform.GetChild((int)Piece.PieceType.Block).GetChild(0).GetComponent<Text>().text = LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Block).ToString();
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Block).gameObject.GetComponent<Button>().interactable = false;
            piece_buttons.transform.GetChild((int)Piece.PieceType.Block).GetChild(0).GetComponent<Text>().text = "0";
        }

        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Portal) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Portal).gameObject.GetComponent<Button>().interactable = true;
            piece_buttons.transform.GetChild((int)Piece.PieceType.Portal).GetChild(0).GetComponent<Text>().text = LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Portal).ToString();
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Portal).gameObject.GetComponent<Button>().interactable = false;
            piece_buttons.transform.GetChild((int)Piece.PieceType.Portal).GetChild(0).GetComponent<Text>().text = "0";
        }

        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.SpeedBoost) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.SpeedBoost).gameObject.GetComponent<Button>().interactable = true;
            piece_buttons.transform.GetChild((int)Piece.PieceType.SpeedBoost).GetChild(0).GetComponent<Text>().text = LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.SpeedBoost).ToString();
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.SpeedBoost).gameObject.GetComponent<Button>().interactable = false;
            piece_buttons.transform.GetChild((int)Piece.PieceType.SpeedBoost).GetChild(0).GetComponent<Text>().text = "0";
        }

        if (LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Trampoline) > 0)
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Trampoline).gameObject.GetComponent<Button>().interactable = true;
            piece_buttons.transform.GetChild((int)Piece.PieceType.Trampoline).GetChild(0).GetComponent<Text>().text = LevelManager.Instance.current_level.CheckPiecesAvailable(Piece.PieceType.Trampoline).ToString();
        }
        else
        {
            piece_buttons.transform.GetChild((int)Piece.PieceType.Trampoline).gameObject.GetComponent<Button>().interactable = false;
            piece_buttons.transform.GetChild((int)Piece.PieceType.Trampoline).GetChild(0).GetComponent<Text>().text = "0";
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
        ui_audio.PlayOneShot(button_sound);
        DisableLevelSelection();
        EnableGui();
        LevelManager.Instance.LoadLevel(index - 1);
    }
}
