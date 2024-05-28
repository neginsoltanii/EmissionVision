using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;
using UnityEngine.Networking;

public class DataFormatWorld
{
    public string countryName;
    public float co2emissions;
}

public class DataManager : MonoBehaviour
{
    public Dictionary<int, List<DataFormatWorld>> dataPerYear;
    public Dictionary<int, Dictionary<string,float>> dataPerYearAndCountry;

    // SINGLETON
    public static DataManager instance;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        // Attempt to load CSV files
        StartCoroutine(LoadCSVFiles());
    }

    IEnumerator LoadCSVFiles()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "co2Emission.csv");
        string result;

        // Check if the file is in a format that Unity can access
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load CSV file: " + www.error);
                yield break;
            }
            result = www.downloadHandler.text;
        }
        else
        {
            result = System.IO.File.ReadAllText(filePath);
        }

        List<Dictionary<string, object>> data = CSVReader.Read(result);
        OrganizeDataInYear(data);
    }

    void OrganizeDataInYear(List<Dictionary<string, object>> data)
    {
        string colnameCountry = "Country";
        string colnameYear = "Year";
        string colnameCO2 = "CO2EmissionRate (mt)";

        dataPerYear = new Dictionary<int, List<DataFormatWorld>>();
        dataPerYearAndCountry = new Dictionary<int, Dictionary<string, float>>();

        foreach (Dictionary<string, object> row in data)
        {
            string rowCountry = Convert.ToString(row[colnameCountry]);
            int rowYear = Convert.ToInt32(row[colnameYear]);

            float rowCO2;
            try
            {
                rowCO2 = float.Parse(row[colnameCO2].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                rowCO2 = -1f;
            }

            // Add data to the LIST of country,co2
            DataFormatWorld dataRow = new DataFormatWorld();
            dataRow.countryName = rowCountry;
            dataRow.co2emissions = rowCO2;

            if (!dataPerYear.ContainsKey(rowYear))
            {
                dataPerYear[rowYear] = new List<DataFormatWorld>();
            }
            dataPerYear[rowYear].Add(dataRow);


            /// Add data to the DICTIONARY per year AND per country

            if (!dataPerYearAndCountry.ContainsKey(rowYear))
            {
                dataPerYearAndCountry[rowYear] = new Dictionary<string, float>();
            }
            dataPerYearAndCountry[rowYear].Add(rowCountry, rowCO2); ;
        }
    }

    public List<DataFormatWorld> GetDataForYear(int year)
    {
        if (dataPerYear == null)
        {
            Debug.LogError("Data has not been initialized.");
            return null;
        }

        if (dataPerYear.ContainsKey(year))
        {
            return dataPerYear[year];
        }
        Debug.LogError("No data available for year " + year);
        return null;
    }

    public float GetCo2FromYearAndCountry(int year, string countryName)
    {
        if(dataPerYearAndCountry.ContainsKey(year))
        {
            float valueCo2;
            bool dataExists = dataPerYearAndCountry[year].TryGetValue(countryName, out valueCo2);

            if (dataExists)
                return valueCo2;
            else
                return -1f;
        }
        return -1f;
    }
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;

public class DataFormatWorld
{
    public string countryName;
    public float co2emissions;
}

public class DataManager : MonoBehaviour
{
    public Dictionary<int, List<DataFormatWorld>> dataPerYear;

    void Start()
    {
        // Attempt to load CSV files
        try
        {
            LoadCSVFiles();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load CSV files: " + e.Message);
        }
    }

    void LoadCSVFiles()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "co2Emission.csv");
        if (!System.IO.File.Exists(filePath))
        {
            Debug.LogError("CSV file not found at path: " + filePath);
            return;
        }

        string result = System.IO.File.ReadAllText(filePath);
        List<Dictionary<string, object>> data = CSVReader.Read(result);
        OrganizeDataInYear(data);
    }

    void OrganizeDataInYear(List<Dictionary<string, object>> data)
    {
        string colnameCountry = "Country";
        string colnameYear = "Year";
        string colnameCO2 = "CO2EmissionRate (mt)";

        dataPerYear = new Dictionary<int, List<DataFormatWorld>>();

        foreach (Dictionary<string, object> row in data)
        {
            string rowCountry = Convert.ToString(row[colnameCountry]);
            int rowYear = Convert.ToInt32(row[colnameYear]);

            float rowCO2;
            try
            {
                rowCO2 = float.Parse(row[colnameCO2].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                rowCO2 = -1f;
            }

            DataFormatWorld dataRow = new DataFormatWorld();
            dataRow.countryName = rowCountry;
            dataRow.co2emissions = rowCO2;

            if (!dataPerYear.ContainsKey(rowYear))
            {
                dataPerYear[rowYear] = new List<DataFormatWorld>();
            }

            dataPerYear[rowYear].Add(dataRow);
        }
    }

    public List<DataFormatWorld> GetDataForYear(int year)
    {
        if (dataPerYear == null)
        {
            Debug.LogError("Data has not been initialized.");
            return null;
        }

        if (dataPerYear.ContainsKey(year))
        {
            return dataPerYear[year];
        }
        Debug.LogError("No data available for year " + year);
        return null;
    }
}*/






//Old sscript:
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;

public class DataFormatWorld
{
    public string countryName;
    public float co2emissions;
}

public class DataManager : MonoBehaviour
{
    public Dictionary<int, List<DataFormatWorld>> dataPerYear;

    //public TMP_Text dataCountryUI;
    //public TMP_Text dataCo2emissionsUI;

    void Start()
    {
        //ResetDataUI();
        LoadCSVFiles();
    }
    

void LoadCSVFiles()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "co2Emission.csv");
        string result = System.IO.File.ReadAllText(filePath);

        List<Dictionary<string, object>> data = CSVReader.Read(result);
        OrganizeDataInYear(data);
    }

    void OrganizeDataInYear(List<Dictionary<string, object>> data)
    {
        string colnameCountry = "Country";
        string colnameYear = "Year";
        string colnameCO2 = "CO2EmissionRate (mt)";

        dataPerYear = new Dictionary<int, List<DataFormatWorld>>();

        foreach (Dictionary<string, object> row in data)
        {
            string rowCountry = Convert.ToString(row[colnameCountry]);
            int rowYear = Convert.ToInt32(row[colnameYear]);

            float rowCO2;
            try
            {
                rowCO2 = float.Parse(row[colnameCO2].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                rowCO2 = -1f;
            }

            DataFormatWorld dataRow = new DataFormatWorld();
            dataRow.countryName = rowCountry;
            dataRow.co2emissions = rowCO2;

            if (!dataPerYear.ContainsKey(rowYear))
            {
                dataPerYear[rowYear] = new List<DataFormatWorld>();
            }

            dataPerYear[rowYear].Add(dataRow);
        }
    }

    public List<DataFormatWorld> GetDataForYear(int year)
    {
        if (dataPerYear.ContainsKey(year))
        {
            return dataPerYear[year];
        }
        return null;
    }
}*/