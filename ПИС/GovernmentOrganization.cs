
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

public class GovernmentOrganization {

    public String Name;

    public String Address;

    public GovernmentOrganization(String rawLine) {
        if (rawLine.Contains(";"))
        {
            String[] parts = rawLine.Split(';');
            this.Name = parts[0].Trim();
            this.Address = parts[1].Trim();
        }
        else
        {
            this.Name = rawLine;
            this.Address = "";
        }
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