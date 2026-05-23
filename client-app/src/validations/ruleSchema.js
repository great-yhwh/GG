import { z } from "zod";

const propertySchema = z.object({
    name: z.string().min(1, "Название свойства обязательно"),
    value: z.string().min(1, "Значение свойства обязательно"),
});

const profileSchema = z.object({
    days: z.number({ required_error: "Количество дней обязательно" })
        .int()
        .positive("Должно быть > 0"),
    purposes: z.string().default(""),
    citizenships: z.string().default(""),
    properties: z.array(propertySchema).default([]),
});

const splitSemi = (str = "") =>
    str.split(";").map(s => s.trim()).filter(Boolean);

export const ruleSchema = z.object({
    ruleName: z.string().trim().min(1, "Название правила обязательно"),
    targetDocs: z.string().optional().default(""),
    guidanceDescription: z.string().optional().default(""),
    refusal: z.string().optional().default(""),
    orgNames: z.string().optional().default(""),
    orgAddresses: z.string().optional().default(""),
    profiles: z.array(profileSchema).min(1, "Добавьте хотя бы один профиль"),
}).superRefine((data, ctx) => {
    const orgNames = splitSemi(data.orgNames);
    const orgAddresses = splitSemi(data.orgAddresses);

    if (orgNames.length !== orgAddresses.length) {
        ctx.addIssue({
            path: ["orgAddresses"],
            message: "Количество адресов должно совпадать с количеством организаций",
            code: "custom",
        });
    }
});