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
        // 1. Создаем объект
        ForeignCitizen newCitizen = new ForeignCitizen();

        // 2. Генерируем уникальный ID
        string newId = Guid.NewGuid().ToString();
        newCitizen.id = newId;

        // 3. Сохраняем в коллекцию
        citizens.Add(newCitizen);

        // 4. Возвращаем ID, чтобы Сервис передал его Контроллеру -> Форме
        return newId;
    }

    /// <summary>
    /// Находит гражданина по ID и заполняет его данными.
    /// Это реализация шагов 11-17 из диаграммы коммуникации.
    /// </summary>
    public ForeignCitizen GetCitizen(Country citizenship, EntryPurpose purpose, MigrantStatus status, String entryDate, String visitorId)
    {
        // 1. Ищем гражданина (вызов внутреннего метода)
        ForeignCitizen foundCitizen = getByVisitorId(visitorId);

        if (foundCitizen == null)
        {
            return null;
        }

        foundCitizen.setCitizenship(citizenship);
        foundCitizen.setPurpose(purpose);
        foundCitizen.setStatus(status);
        foundCitizen.setEntryDate(entryDate);

        // 3. Возвращаем заполненного гражданина
        return foundCitizen;
    }

    /// <summary>
    /// Внутренний поиск (имитация запроса к БД: SELECT * FROM citizens WHERE id = visitorId)
    /// @param visitorId 
    /// @return
    /// </summary>
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