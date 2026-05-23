const splitBy = (value = "", separator) =>
    value
        .split(separator)
        .map((s) => s.trim())
        .filter(Boolean);

export const transformFormData = (formValues) => {
    const {
        ruleName,
        targetDocs,
        guidanceDescription,
        refusal,
        orgNames,
        orgAddresses,
        profiles,
    } = formValues;

    const targetDocsList = splitBy(targetDocs || "", ";");
    const orgNamesList = splitBy(orgNames || "", ";");
    const orgAddressesList = splitBy(orgAddresses || "", ";");

    const daysList = profiles.map((p) => p.days);

    const purposeNamesList = profiles.map((p) =>
        splitBy(p.purposes || "", ",")
    );

    const citizenshipNamesList = profiles.map((p) =>
        splitBy(p.citizenships || "", ",")
    );

    const propertyNames = profiles.map((p) =>
        p.properties.map((prop) => prop.name)
    );

    const propertyValues = profiles.map((p) =>
        p.properties.map((prop) => prop.value)
    );

    const payload = {
        ruleName,
        targetDocs: targetDocsList,
        guidanceDescription: guidanceDescription || "",
        refusal: refusal || "",
        orgNames: orgNamesList,
        orgAddresses: orgAddressesList,
        daysList,
        purposeNamesList,
        citizenshipNamesList,
        propertyNames,
        propertyValues,
    };

    console.log("PAYLOAD:", payload);
    console.log("PAYLOAD JSON:", JSON.stringify(payload, null, 2));
    window.__payload = payload;

    return payload;
};