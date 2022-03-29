using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int BASE_POINTS = 500;
    private static int throwsCount;
    private static int kegelsStandingCount;
    private static int score;

    private static List<Kegel> kegelsToDelete;

    void Start()
    {
        Setup();
    }


    public void onThrow()
    {

    }

    void Setup()
    {
        throwsCount = 0;
        kegelsStandingCount = 10;
        score = 0;
    }

    public static void OnKegelFall(Kegel kegel)
    {
        kegelsStandingCount -= 1;
        kegelsToDelete.Add(kegel);
        score += BASE_POINTS / throwsCount;
    }

    private static void DeleteKegels()
    {
        foreach (var k in kegelsToDelete)
            Destroy(k);
    }

    public static void SetupNewThrow()
    {
        DeleteKegels();
    }
}
