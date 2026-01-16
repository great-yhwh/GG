using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ForeignCitizenCollection
{

    // Список для хранения всех созданных граждан
    private List<ForeignCitizen> citizens;

    public ForeignCitizenCollection()
    {
        citizens = new List<ForeignCitizen>();
    }

    /// <summary>
    /// Создает нового гражданина, присваивает ID и сохраняет в список.
    /// @return visitorId
    /// </summary>
    public String createForeignCitizen()
    {
        // Создать объект Иностранного гражданина
        ForeignCitizen newCitizen = new ForeignCitizen();

        // Присвоить Айди
        string newId = Guid.NewGuid().ToString();
        newCitizen.id = newId;

        // Сохранить
        citizens.Add(newCitizen);

        // 4. Возвращаем ID, чтобы Сервис передал его Контроллеру -> Форме
        return newId;
    }

    
    public ForeignCitizen GetCitizen(Country citizenship, EntryPurpose purpose, MigrantStatus status, String entryDate, String visitorId)
    {
        // Поиск гражданина; вызов внутреннего метода
        ForeignCitizen foundCitizen = getByVisitorId(visitorId);

        if (foundCitizen == null)
        {
            return null;
        }

        foundCitizen.setCitizenship(citizenship);
        foundCitizen.setPurpose(purpose);
        foundCitizen.setStatus(status);
        foundCitizen.setEntryDate(entryDate);

        // Заполненный гражданин
        return foundCitizen;
    }

    public ForeignCitizen getByVisitorId(String visitorId)
    {
        foreach (ForeignCitizen fc in citizens)
        {
            if (fc.getId() == visitorId)
            {
                return fc;
            }
        }
        return null;
    }
}