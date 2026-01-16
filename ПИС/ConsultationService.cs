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

        // Создаем Правило
        patentRule = new PatentRule();

        // НАСТРАИВАЕМ ПРАВИЛО (Service берет объекты из Collection и кладет в Rule)

        string[] targetCountries = { "Азербайджан", "Таджикистан", "Узбекистан", "Молдова", "Украина" };
        foreach (string name in targetCountries)
        {
            Country c = countryCollection.FindCitizenship(name);
            patentRule.AddCriteriaCountry(c); // Заполняем критерий
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


    public String createPatentMessage(String citizenshipStr, String purposeStr, String statusStr, String entryDate, String visitorId)
    {

        // 1. Ищем объекты в коллекциях (Lookup)
        Country c = countryCollection.FindCitizenship(citizenshipStr);
        EntryPurpose p = purposeCollection.FindPurpose(purposeStr);
        MigrantStatus s = statusCollection.FindStatus(statusStr);

        // 2. Получаем и обновляем гражданина (через коллекцию)
        // Метод GetCitizen внутри ForeignCitizenCollection найдет по ID и сделает set...
        ForeignCitizen fc = foreignCitizenCollection.GetCitizen(c, p, s, entryDate, visitorId);

        if (fc == null) return "Ошибка: Посетитель не найден.";

        // Вызываем правило
        String resultMessage = patentRule.PatentMessage(fc);

        // Возвращаем результат
        return resultMessage;
    }
}