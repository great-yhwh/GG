
using System;

public class GovernmentOrganization {

    private String Name;
    private String Address;

    public GovernmentOrganization(String rawLine) {
        String[] parts = rawLine.Split(';');
        this.Name = parts[0].Trim();
        this.Address = parts[1].Trim();
    }
 
    /// <summary>
    /// @return
    /// </summary>
    public String getName() {
        return this.Name;
    }

    public String getAddress()
    {
        return this.Address;
    }

    public String getFullInfo()
    {
        if (String.IsNullOrEmpty(Address)) return Name;
        return Name + " (Адрес: " + Address + ")";
    }

}