using System;
using System.Collections.Generic;

public class PatentController
{
    private ConsultationService service;

    public PatentController()
    {
        service = new ConsultationService();
    }

    public Dictionary<String, List<String>> RequestPatentConsultation()
    {
        return service.RequestPatentConsultation();
    }

    public String createPatentMessage(String citizenshipStr, String purposeStr, String statusStr, String entryDate)
    {
        return null;        // Позже
    }
}