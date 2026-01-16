using System;
using System.Collections.Generic;

public class ConsultationService
{
    // Ассоциации с коллекциями
    private CountryCollection countryCollection;
    private EntryPurposeCollection purposeCollection;
    private MigrantStatusCollection statusCollection;
    private ForeignCitizenCollection foreignCitizenCollection;

    public ConsultationService()
    {
        // Инициализация коллекций
        countryCollection = new CountryCollection();
        purposeCollection = new EntryPurposeCollection();
        statusCollection = new MigrantStatusCollection();
        foreignCitizenCollection = new ForeignCitizenCollection();
    }

    /// <summary>
    /// Возвращает словарь, где ключ - название списка, значение - список строк
    /// </summary>
    public Dictionary<string, List<string>> RequestPatentConsultation()
    {
        // Получение списков стран, целей, статусов ...
        List<String> countries = countryCollection.getAllCountry();
        List<String> purposes = purposeCollection.getAllPurpose();
        List<String> statuses = statusCollection.getAllStatusCollection();

        // Коллекция создает объект, хранит его у себя и возвращает нам ID
        String visitorId = foreignCitizenCollection.createForeignCitizen();

        // Упаковка в словарь
        Dictionary<string, List<String>> result = new Dictionary<String, List<String>>();
        result.Add("countries", countries);
        result.Add("purposes", purposes);
        result.Add("statuses", statuses);

        // Передаем ID как список из одного элемента (чтобы тип данных был везде List<string>)
        result.Add("visitorId", new List<String> { visitorId });

        return result;
    }

    public string createPatentMessage(String c, String p, String s, String d)
    {
        return null; // позже
    }
}