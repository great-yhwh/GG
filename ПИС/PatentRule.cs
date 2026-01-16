using System;
using System.Collections.Generic;

public class PatentRule
{

    public List<Country> criteriaCountries;
    public EntryPurpose criteriaPurpose;
    public MigrantStatus criteriaStatus;

    public List<GovernmentOrganization> organizations;
    public String messagePatentNeed = "Вам необходимо оформить трудовой патент...";
    public String messagePatentNoNeed = "Вам НЕ требуется патент...";
    public int criteriaDateDays = 30;

    public PatentRule()
    {
        criteriaCountries = new List<Country>();
        organizations = new List<GovernmentOrganization>();
    }

  
    public void AddCriteriaCountry(Country c)
    {
        if (c != null) criteriaCountries.Add(c);
    }

    public void SetCriteriaPurpose(EntryPurpose p)
    {
        criteriaPurpose = p;
    }

    public void SetOrganizations(List<GovernmentOrganization> orgs)
    {
        this.organizations = orgs;
    }

    public String PatentMessage(ForeignCitizen foreignCitizen)
    {
        Country c = foreignCitizen.getCitizenship();
        EntryPurpose p = foreignCitizen.getPurpose();
        String dateStr = foreignCitizen.getEntryDate();

        if (c == null || p == null) return "Ошибка данных";

        bool countryMatch = criteriaCountries.Contains(c);
        bool purposeMatch = (p == criteriaPurpose);

        DateTime entryDate;
        bool dateParsed = DateTime.TryParse(dateStr, out entryDate);
        int daysPassed = dateParsed ? (DateTime.Now - entryDate).Days : 0;

        if (countryMatch && purposeMatch)
        {

            String result = messagePatentNeed;

            if (daysPassed > criteriaDateDays)
            {
                result += "\nВНИМАНИЕ: Срок в 30 дней истек! Возможен штраф.";
            }
            else
            {
                result += "\nОсталось дней на подачу: " + (criteriaDateDays - daysPassed);
            }

            if (organizations.Count > 0)
            {
                result += "\n\nКуда обратиться:";
                foreach (GovernmentOrganization org in organizations)
                {
                    result += "\n- " + org.getFullInfo();
                }
            }

            return result;
        }
        return messagePatentNoNeed;
    }
}