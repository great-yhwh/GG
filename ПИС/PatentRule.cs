using System;
using System.Collections.Generic;

public class PatentRule
{

    public List<Country> criteriaCountries;
    public EntryPurpose criteriaPurpose;
    // public MigrantStatus criteriaStatus;

    public String messagePatentNeed = "Вам необходимо оформить трудовой патент...";
    public String messagePatentNoNeed = "Вам НЕ требуется патент...";
    public int criteriaDateDays = 30;

    public PatentRule()
    {
        criteriaCountries = new List<Country>();
    }

  
    public void AddCriteriaCountry(Country c)
    {
        if (c != null) criteriaCountries.Add(c);
    }

    public void SetCriteriaPurpose(EntryPurpose p)
    {
        criteriaPurpose = p;
    }

    public String PatentMessage(ForeignCitizen foreignCitizen)
    {
        // Логика проверки остается той же (сравнение объектов)
        Country c = foreignCitizen.getCitizenship();
        EntryPurpose p = foreignCitizen.getPurpose();
        String dateStr = foreignCitizen.getEntryDate();

        if (c == null || p == null) return "Ошибка данных";

        // Сравнение объектов (тут PatentRule обращается к методам Country)
        bool countryMatch = criteriaCountries.Contains(c);
        bool purposeMatch = (p == criteriaPurpose);

        // ... проверка даты ...
        DateTime entryDate;
        bool dateParsed = DateTime.TryParse(dateStr, out entryDate);
        int daysPassed = dateParsed ? (DateTime.Now - entryDate).Days : 0;

        if (countryMatch && purposeMatch)
        {
            if (daysPassed > criteriaDateDays) return messagePatentNeed + " (Срок истек)";
            return messagePatentNeed;
        }
        return messagePatentNoNeed;
    }
}