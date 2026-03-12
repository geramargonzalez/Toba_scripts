using System.Collections;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataCtrl : MonoBehaviour
{
    public static DataCtrl instance = null;
    public GameData data;
    public bool devMode;
    string dataFilePath;
    BinaryFormatter bf;

    public BD dataBase;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // Bug fix #1: inicializar dataBase en Awake, antes de OnEnable
            dataBase = new BD();
            dataBase.inicializar();
        }
        bf = new BinaryFormatter();
        dataFilePath = Application.persistentDataPath + "/game.dat";
    }

    private void Start()
    {
        data.niveles = dataBase.niveles;
    }

    // Bug fix #11: FileStream dentro de using para garantizar cierre
    public void RefreshData()
    {
        if (File.Exists(dataFilePath))
        {
            using (FileStream fs = new FileStream(dataFilePath, FileMode.Open))
                data = (GameData)bf.Deserialize(fs);
        }
    }

    void OnEnable()  { CheckDB(); }
    void OnDisable() { SaveData(); }

    public void SaveData()
    {
        SalvarNiveles(data.niveles);
        using (FileStream fs = new FileStream(dataFilePath, FileMode.Create))
            bf.Serialize(fs, data);
    }

    // Copiar para la batallas de las tablas
    public void SaveData(GameData pdata)
    {
        SalvarNiveles(pdata.niveles);
        using (FileStream fs = new FileStream(dataFilePath, FileMode.Create))
            bf.Serialize(fs, pdata);
    }

    // Guarda solo la GameData
    public void SaveDataNoBD(GameData pdata)
    {
        using (FileStream fs = new FileStream(dataFilePath, FileMode.Create))
            bf.Serialize(fs, pdata);
    }

    public bool IsUnlocked(int levelNumber) { return data.niveles[levelNumber].unlocked; }
    public int  GetStars(int levelNumber)   { return data.niveles[levelNumber].bonesStars; }

    public void CheckDB()
    {
        if (!File.Exists(dataFilePath))
        {
#if UNITY_ANDROID
            CopyDB();
#endif
        }
        else
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                string dstFile = Path.Combine(Application.streamingAssetsPath, "game.dat");
                File.Delete(dstFile);
                File.Copy(dataFilePath, dstFile);
            }

            // Bug fix #2: devMode controla el borrado de progreso en movil
            if (devMode && SystemInfo.deviceType == DeviceType.Handheld)
            {
                File.Delete(dataFilePath);
                CopyDB(); // async; RefreshData() llamado internamente
            }
            else
            {
                RefreshData();
            }
        }
    }

    // Bug fix #3: reemplazar WWW (deprecated) + busy-wait por UnityWebRequest coroutine
    public void CopyDB() { StartCoroutine(CopyDBCoroutine()); }

    IEnumerator CopyDBCoroutine()
    {
        string srcFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
        using (UnityEngine.Networking.UnityWebRequest req = UnityEngine.Networking.UnityWebRequest.Get(srcFile))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                File.WriteAllBytes(dataFilePath, req.downloadHandler.data);
                RefreshData();
            }
            else
            {
                Debug.LogError("CopyDB failed: " + req.error);
            }
        }
    }

    // Bug fix #7: ResetAllLevel() se llama ANTES de abrir el FileStream
    public void ResetData()
    {
        ResetAllLevel();
        using (FileStream fs = new FileStream(dataFilePath, FileMode.Create))
            bf.Serialize(fs, data);
    }

    public void SetearNumeroNivel() { SetearNivelACtual(); }

    public void SalvarNiveles(Nivel[] niveles)
    {
        dataBase.GuardarNiveles(niveles);
    }

    public void guardarFallosYAciertos(Nivel[] niveles)
    {
        foreach (Nivel nivel in niveles)
            dataBase.guardarFallosYAciertos(nivel);
    }

    public void EditarEstadoNivel(Nivel nivel)
    {
        dataBase.EditarEstadoNivel(nivel, 1);
    }

    public void GuardarPosicionInicial()
    {
        data.x = 3.3f;
        data.y = -26.0f;
        data.z = 0f;
    }

    // Setea el tiempo de cada pantalla
    public float ResetTime()
    {
        // Bug fix #5: else if para evitar evaluaciones innecesarias
        if      (data.nivel == 0) data.tiempoActual = 300;
        else if (data.nivel == 1) data.tiempoActual = 250;
        else if (data.nivel == 2) data.tiempoActual = 200;
        else if (data.nivel >= 4) data.tiempoActual = 300;
        return data.tiempoActual;
    }

    // Sube el nivel del juego
    public void subirNivel()
    {
        if ((data.nivel + 1) > data.nivelMaximo)
        {
            SetearNivelACtual();
            data.nivel++;
            data.nivelMaximo = data.nivel;
            data.yaJugo = false;
            data.niveles[data.nivelMaximo].unlocked = true;
        }
        SaveData(data);
    }

    // Seteo las estadisticas del nivel
    public void SetearNivelACtual()
    {
        // Bug fix #4: guard contra division por cero (nivel 3 y 5 retornan 0)
        int cantEnemigos = cantidadEnemigoPorNivel();
        if (cantEnemigos > 0)
            data.niveles[data.nivelMaximo].promedio = data.niveles[data.nivelMaximo].promedio / cantEnemigos;
        data.niveles[data.nivelMaximo].fallosPorNivel = data.fallos;

        if (data.nivel == 3) data.niveles[data.nivelMaximo].aciertosPorNivel = 8;
        if (data.nivel == 5) data.niveles[data.nivelMaximo].aciertosPorNivel = 12;
    }

    void SetearNumeroDeNiveles()
    {
        for (int i = 0; i < data.niveles.Length; i++)
            data.niveles[i].nivel = i;
    }

    // Mejora #9: unificacion de los dos branches identicos
    public void UnLockedNivel()
    {
        for (int i = 0; i < data.niveles.Length; i++)
        {
            bool isUnlocked = data.niveles[i].nivel <= data.nivel;
            data.niveles[i].unlocked              = isUnlocked;
            data.niveles[i].status                = 0;
            data.niveles[i].cantVecesJugadas      = 0;
            data.niveles[i].bonesStars            = 0;
            data.niveles[i].puntosPorNivel         = 0;
            data.niveles[i].aciertosPorNivel       = 0;
            data.niveles[i].fallosPorNivel         = 0;
            data.niveles[i].fallosMultiplicacion   = 0;
            data.niveles[i].fallosSuma             = 0;
            data.niveles[i].fallosResta            = 0;
            data.niveles[i].fallosDivision         = 0;
            data.niveles[i].aciertosMultiplicacion = 0;
            data.niveles[i].aciertosSuma           = 0;
            data.niveles[i].aciertosResta          = 0;
            data.niveles[i].aciertosDivision       = 0;
            data.niveles[i].promedio               = 0;
        }
        dataBase.GuardarNiveles(data.niveles);
    }

    public int NivelLogrado() { return data.nivel; }

    // Reseteo todos los niveles
    public void ResetAllLevel()
    {
        data.nivel                       = 0;
        data.nivelMaximo                 = 0;
        data.vidas                       = 5;
        data.bones                       = 0;
        data.puntos                      = 0;
        data.fallos                      = 0;
        data.promedioGrl                 = 0;
        data.cantidadOperacionesPorNivel = 0;
        data.sumasGrl                    = 0;
        data.restaGrl                    = 0;
        data.divisionGrl                 = 0;
        data.aciertosMultiGrl            = 0;
        data.aciertosGrl                 = 0;
        data.sumasFallosGrl              = 0;
        data.restaFallosGrl              = 0;
        data.divisionFallosGrl           = 0;
        data.multiFallosGrl              = 0;
        data.fallosGrl                   = 0;

        UnLockedNivel();

        data.tiempoActual     = ResetTime();
        data.yaJugo           = false;
        // Bug fix #6: restaurar tutorial y primeraVez en un reset completo
        data.primeraVez       = true;
        data.tutorial         = true;
        data.audioOn          = true;
        data.posActualEnemigo = 0;
    }

    // Reseteo el nivel por Perder
    public void ResetLevelGameOver(GameData gdata)
    {
        if (gdata.nivel <= gdata.nivelMaximo)
        {
            gdata.vidas            = 5;
            gdata.bones            = 0;
            gdata.puntos           = 0;
            gdata.fallos           = 0;
            gdata.tiempoActual     = ResetTime();
            gdata.yaJugo           = false;
            gdata.posActualEnemigo = 0;
        }
        SaveDataNoBD(gdata);
    }

    // Bug fix #5: if -> else if en los tres metodos de cantidad
    public int cantidadEnemigoPorNivel()
    {
        int cantEnem = 0;

        if      (data.nivel == 0) cantEnem = 8;
        else if (data.nivel == 1) cantEnem = 7;
        else if (data.nivel == 2) cantEnem = 9;
        else if (data.nivel == 3) cantEnem = 8;
        else if (data.nivel == 4) cantEnem = 13;
        else if (data.nivel == 5) cantEnem = 12;
        else if (data.nivel == 6) cantEnem = 14;
        else if (data.nivel == 7) cantEnem = 16;

        data.cantidadTrolls = cantEnem;
        return cantEnem;
    }

    public int cantidadOrquitosPorNivel()
    {
        int cantEnem = 0;

        if      (data.nivel == 0) cantEnem = 10;
        else if (data.nivel == 1) cantEnem = 21;
        else if (data.nivel == 2) cantEnem = 21;
        else if (data.nivel == 4) cantEnem = 23;
        else if (data.nivel == 6) cantEnem = 15;
        else if (data.nivel == 7) cantEnem = 22;

        return cantEnem;
    }

    public void PuntosPorStars(int stars)
    {
        if      (stars == 1) data.puntos += 2500;
        else if (stars == 2) data.puntos += 5000;
        else if (stars == 3) data.puntos += 10000;
    }

    public int SetStarsAwarded(int levelNumber, int stars)
    {
        return data.niveles[levelNumber].bonesStars = stars;
    }

    public void Unlocked(int levelNumber)
    {
        data.niveles[levelNumber].unlocked = true;
    }

    public int ConvertirCantidadAnimales()
    {
        int cantAnim = 0;

        if      (data.nivel == 0) cantAnim = 8;
        else if (data.nivel == 1) cantAnim = 13;
        else if (data.nivel == 2) cantAnim = 11;
        else if (data.nivel == 4) cantAnim = 13;
        else if (data.nivel == 6) cantAnim = 10;
        else if (data.nivel == 7) cantAnim = 17;

        return cantAnim;
    }
}