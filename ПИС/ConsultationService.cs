using System;
using System.Collections.Generic;

public class ConsultationService
{
    // Ассоциации с коллекциями
    private CountryCollection countryCollection;
    private EntryPurposeCollection purposeCollection;
    private MigrantStatusCollection statusCollection;
    private ForeignCitizenCollection foreignCitizenCollection;

    // Ассоциация с правилом
    private PatentRule patentRule; 


    public ConsultationService()
    {
        // Инициализация коллекций
        countryCollection = new CountryCollection();
        purposeCollection = new EntryPurposeCollection();
        statusCollection = new MigrantStatusCollection();
        foreignCitizenCollection = new ForeignCitizenCollection();

        // Создать Правило
        patentRule = new PatentRule();

        // НАСТРАЙКА ПРАВИЛА (Service берет объекты из Collection и кладет в Rule)

        string[] targetCountries = { "Азербайджан", "Таджикистан", "Узбекистан", "Молдова", "Украина" };
        foreach (string name in targetCountries)
        {
            Country c = countryCollection.FindCitizenship(name);
            patentRule.AddCriteriaCountry(c);
        }

        // Настройка цели
        EntryPurpose p = purposeCollection.FindPurpose("Работа");
        patentRule.SetCriteriaPurpose(p);
    }

    /// <summary>
    /// Возвращает словарь, где ключ - название списка, значение - список строк
    /// </summary>
    public Dictionary<string, List<string>> RequestPatentConsultation()
    {
        // Получение списков стран, целей, статусов
        List<String> countries = countryCollection.getAllCountry();
        List<String> purposes = purposeCollection.getAllPurpose();
        List<String> statuses = statusCollection.getAllStatusCollection();

        // Коллекция создает объект, хранит его у себя и возвращает ID
        String visitorId = foreignCitizenCollection.createForeignCitizen();

        // Упаковка в словарь
        Dictionary<string, List<String>> result = new Dictionary<String, List<String>>();
        result.Add("countries", countries);
        result.Add("purposes", purposes);
        result.Add("statuses", statuses);

        // ID как список из одного элемента,чтобы тип данных был везде List<string>
        result.Add("visitorId", new List<String> { visitorId });

        return result;
    }


    public String createPatentMessage(String citizenshipStr, String purposeStr, String statusStr, String entryDate, String visitorId)
    {

        Country c = countryCollection.FindCitizenship(citizenshipStr);
        EntryPurpose p = purposeCollection.FindPurpose(purposeStr);
        MigrantStatus s = statusCollection.FindStatus(statusStr);

        ForeignCitizen fc = foreignCitizenCollection.GetCitizen(c, p, s, entryDate, visitorId);

        if (fc == null) return "Ошибка: Посетитель не найден.";

        String resultMessage = patentRule.PatentMessage(fc);

        return resultMessage;
    }
}