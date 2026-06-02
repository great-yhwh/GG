import { useState } from "react";
import { useForm, useFieldArray, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import { LeftSidebar } from "../LeftSidebar/LeftSidebar";
import { ProfileEditor } from "../ProfileEditor/ProfileEditor";
import { RuleResultCard } from "../RuleResultCard/RuleResultCard";
import { useRuleSubmit } from "../../hooks/useRuleSubmit";
import { ruleSchema } from "../../validations/ruleSchema.js";
import styles from "./RuleConstructor.module.css";

const createDefaultProfile = () => ({
    days: 90,
    purposes: "",
    citizenships: "",
    properties: [],
});

const defaultValues = {
    ruleName: "",
    targetDocs: "",
    guidanceDescription: "",
    refusal: "",
    orgNames: "",
    orgAddresses: "",
    profiles: [createDefaultProfile()],
};

const basicFieldRows = [
    [
        {
            name: "ruleName",
            label: "Название правила *",
            placeholder: "",
        },
        {
            name: "targetDocs",
            label: "Целевые документы",
            placeholder: "Разрешение на работу; Виза",
            hint: "Укажите значения через ;",
        },
    ],
    [
        {
            name: "guidanceDescription",
            label: "Руководство",
            placeholder: "",
        },
        {
            name: "refusal",
            label: "Отказ",
            placeholder: "",
        },
    ],
    [
        {
            name: "orgNames",
            label: "Организации",
            placeholder: "МВД; ФМС",
            hint: "Укажите значения через ;",
        },
        {
            name: "orgAddresses",
            label: "Адреса организаций",
            placeholder: "ул. Ленина 1; ул. Пушкина 2",
            hint: "Количество адресов должно совпадать с количеством организаций",
        },
    ],
];

const splitAndTrim = (value = "", separator) =>
    value
        .split(separator)
        .map((item) => item.trim())
        .filter(Boolean);

const transformToLegacy = (data) => ({
    ruleName: data.ruleName,
    targetDocs: splitAndTrim(data.targetDocs, ";"),
    guidanceDescription: data.guidanceDescription || "",
    refusal: data.refusal || "",
    orgNames: splitAndTrim(data.orgNames, ";"),
    orgAddresses: splitAndTrim(data.orgAddresses, ";"),
    daysList: data.profiles.map((profile) => profile.days),
    purposeNamesList: data.profiles.map((profile) =>
        splitAndTrim(profile.purposes, ",")
    ),
    citizenshipNamesList: data.profiles.map((profile) =>
        splitAndTrim(profile.citizenships, ",")
    ),
    propertyNames: data.profiles.map((profile) =>
        profile.properties.map((property) => property.name)
    ),
    propertyValues: data.profiles.map((profile) =>
        profile.properties.map((property) => property.value)
    ),
});

export const RuleConstructor = () => {
    const methods = useForm({
        resolver: zodResolver(ruleSchema),
        defaultValues,
    });

    const {
        control,
        register,
        handleSubmit,
        formState: { errors },
    } = methods;

    const {
        fields: profiles,
        append,
        remove,
        move,
    } = useFieldArray({
        control,
        name: "profiles",
    });

    const [currentProfileIndex, setCurrentProfileIndex] = useState(0);
    const { submitRule, loading, error: submitError, result } = useRuleSubmit();

    const onSubmit = async (data) => {
        const payload = transformToLegacy(data);
        await submitRule(payload);
    };

    const handleAddProfile = () => {
        const newIndex = profiles.length;
        append(createDefaultProfile());
        setCurrentProfileIndex(newIndex);
    };

    const handleRemoveProfile = (idx) => {
        if (profiles.length <= 1) return;

        remove(idx);

        if (idx === currentProfileIndex) {
            setCurrentProfileIndex(idx > 0 ? idx - 1 : 0);
            return;
        }

        if (idx < currentProfileIndex) {
            setCurrentProfileIndex((prev) => prev - 1);
        }
    };

    const handleMoveProfile = (oldIndex, newIndex) => {
        if (
            oldIndex === newIndex ||
            newIndex < 0 ||
            newIndex >= profiles.length
        ) {
            return;
        }

        move(oldIndex, newIndex);

        if (currentProfileIndex === oldIndex) {
            setCurrentProfileIndex(newIndex);
        } else if (
            oldIndex < currentProfileIndex &&
            newIndex >= currentProfileIndex
        ) {
            setCurrentProfileIndex((prev) => prev - 1);
        } else if (
            oldIndex > currentProfileIndex &&
            newIndex <= currentProfileIndex
        ) {
            setCurrentProfileIndex((prev) => prev + 1);
        }
    };

    return (
        <FormProvider {...methods}>
            <form onSubmit={handleSubmit(onSubmit)} className={styles.container}>
                <LeftSidebar
                    profiles={profiles}
                    currentProfileIndex={currentProfileIndex}
                    onSelectProfile={setCurrentProfileIndex}
                    onAddProfile={handleAddProfile}
                    onRemoveProfile={handleRemoveProfile}
                    onMoveProfile={handleMoveProfile}
                />

                <div className={styles.rightPane}>
                    <div className={styles.basicFields}>
                        {basicFieldRows.map((row, rowIndex) => (
                            <div key={rowIndex} className={styles.fieldRow}>
                                {row.map(({ name, label, placeholder, hint }) => {
                                    const fieldError = errors[name];

                                    return (
                                        <div key={name} className={styles.fieldGroup}>
                                            <label htmlFor={name}>{label}</label>
                                            <input
                                                id={name}
                                                {...register(name)}
                                                placeholder={placeholder}
                                            />
                                            {hint && !fieldError && (
                                                <span className={styles.hint}>{hint}</span>
                                            )}
                                            {fieldError && (
                                                <span className={styles.error}>
                                                    {fieldError.message}
                                                </span>
                                            )}
                                        </div>
                                    );
                                })}
                            </div>
                        ))}
                    </div>

                    {profiles[currentProfileIndex] && (
                        <ProfileEditor
                            profileIndex={currentProfileIndex}
                            control={control}
                            register={register}
                            errors={errors}
                        />
                    )}

                    <button
                        type="submit"
                        className={styles.submitBtn}
                        disabled={loading}
                    >
                        {loading ? "Создание..." : "Создать правило"}
                    </button>

                    {submitError && (
                        <div className={styles.serverError}>{submitError}</div>
                    )}

                    {result && <RuleResultCard rule={result} />}
                </div>
            </form>
        </FormProvider>
    );
};