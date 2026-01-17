using System;

public class ForeignCitizen
{

    // Публичные поля
    private String id;
    private String entryDate;

    // Приватные поля для хранения объектов по ассоциациам
    private Country citizenship;
    private EntryPurpose purpose;
    private MigrantStatus status;

    public ForeignCitizen()
    {
    }

    /// <summary>
    /// @return
    /// </summary>
    public String getId()
    {
        return this.id;
    }

    /// <summary>
    /// @param citizenship
    /// </summary>
    public void setCitizenship(Country citizenship)
    {
        this.citizenship = citizenship;
    }

    /// <summary>
    /// @param purpose
    /// </summary>
    public void setPurpose(EntryPurpose purpose)
    {
        this.purpose = purpose;
    }

    /// <summary>
    /// @param status
    /// </summary>
    public void setStatus(MigrantStatus status)
    {
        this.status = status;
    }

    /// <summary>
    /// @param entryDate
    /// </summary>
    public void setEntryDate(String entryDate)
    {
        this.entryDate = entryDate;
    }

    /// <summary>
    /// @return
    /// </summary>
    public Country getCitizenship()
    {
        return this.citizenship;
    }

    /// <summary>
    /// @return
    /// </summary>
    public EntryPurpose getPurpose()
    {
        return this.purpose;
    }

    /// <summary>
    /// @return
    /// </summary>
    public MigrantStatus getStatus()
    {
        return this.status;
    }

    /// <summary>
    /// @return
    /// </summary>
    public String getEntryDate()
    {
        return this.entryDate;
    }
}