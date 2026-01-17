using System;
using System.Collections.Generic;

public class PatentRule
{

    public List<Country> criteriaCountries;
    public EntryPurpose criteriaPurpose;
    public List<MigrantStatus> criteriaStatus;

    public List<GovernmentOrganization> organizations;
    public String messagePatentNeed = "Вам необходимо оформить трудовой патент";
    public String messagePatentNoNeed = "Вам НЕ требуется патент";
    public int criteriaDateDays = 30;

    public PatentRule()
    {
        criteriaCountries = new List<Country>();
        criteriaStatus = new List<MigrantStatus>();
        organizations = new List<GovernmentOrganization>();
    }

  
    public void AddCriteriaCountry(Country c)
    {
        if (c != null) criteriaCountries.Add(c);
    }

    public void AddCriteriaStatus(MigrantStatus s)
    {
        if (s != null) criteriaStatus.Add(s);
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
        MigrantStatus s = foreignCitizen.getStatus();
        String dateStr = foreignCitizen.getEntryDate();

        if (c == null || p == null) return "Ошибка данных";

        bool countryMatch = criteriaCountries.Contains(c);
        bool purposeMatch = (p == criteriaPurpose);
        bool statusMatch = criteriaStatus.Contains(s);

        DateTime entryDate;
        bool dateParsed = DateTime.TryParse(dateStr, out entryDate);
        int daysPassed = dateParsed ? (DateTime.Now - entryDate).Days : 0;

        if (countryMatch && purposeMatch && statusMatch)
        {

            String result = messagePatentNeed;

            if (daysPassed > criteriaDateDays)
            {
                result += Environment.NewLine + "ВНИМАНИЕ: Срок в 30 дней истек! Возможен штраф.";
            }
            else
            {
                result += Environment.NewLine + "Осталось дней на подачу: " + (criteriaDateDays - daysPassed);
            }

            if (organizations.Count > 0)
            {
                result += Environment.NewLine + Environment.NewLine + "Куда обратиться:";
                foreach (GovernmentOrganization org in organizations)
                {
                    result += Environment.NewLine + " - " + org.getFullInfo();
                }
            }

            return result;
        }
        return messagePatentNoNeed;
    }
}