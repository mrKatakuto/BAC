using UnityEngine;
using TMPro;
using System.Collections;

public class CentralStationController : MonoBehaviour
{
    private bool solarActivated = false;
    private bool windmillActivated = false;
    private bool reactorActivated = false;

    public TextMeshProUGUI messageText; 

    public enum MessageType
    {
        ReactorActivated,
        WindmillActivated,
        SolarActivated
    }

    public void ActivateStation(string station)
    {
        bool justActivated = false;
        switch (station)
        {
            case "Solar":
                if (!solarActivated)
                {
                    solarActivated = true;
                    justActivated = true;
                    ShowMessage(MessageType.SolarActivated);
                }
                break;
            case "Windmill":
                if (!windmillActivated)
                {
                    windmillActivated = true;
                    justActivated = true;
                    ShowMessage(MessageType.WindmillActivated);
                }
                break;
            case "Reactor":
                if (!reactorActivated)
                {
                    reactorActivated = true;
                    justActivated = true;
                    ShowMessage(MessageType.ReactorActivated);
                }
                break;
        }

        if (justActivated)
        {
            CheckAllStations();
        }
    }

    public bool IsActivated(string station)
    {
        switch (station)
        {
            case "Solar":
                return solarActivated;
            case "Windmill":
                return windmillActivated;
            case "Reactor":
                return reactorActivated;
            default:
                return false;
        }
    }

    void CheckAllStations()
    {
        if (solarActivated && windmillActivated && reactorActivated)
        {
            Debug.Log("Alle Stationen wurden aktiviert. Finalevent wird gestartet.");
            //  Final-Event
        }
    }

    public void ShowMessage(MessageType type)
    {
        string message = GetMessageByType(type);
        StartCoroutine(DisplayMessage(message));
    }

    private string GetMessageByType(MessageType type)
    {
        switch (type)
        {
            case MessageType.ReactorActivated:
                return "Reaktor ist aktiviert!";
            case MessageType.WindmillActivated:
                return "Windmühlen sind aktiviert!";
            case MessageType.SolarActivated:
                return "Solarpanel ist aktiviert!";
            default:
                return "Aktivierung erfolgt!";
        }
    }

    IEnumerator DisplayMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3); // Nachricht für 3 Sekunden anzeigen
        messageText.gameObject.SetActive(false);
    }
}
