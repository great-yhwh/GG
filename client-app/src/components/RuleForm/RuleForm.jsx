import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { ruleSchema } from "../../validations/ruleSchema";
import { ProfileList } from "../ProfileList/ProfileList";
import { GuidanceBlock } from "../GuidanceBlock/GuidanceBlock";
import { transformFormData } from "../../utils/transformFormData";
import { useRuleSubmit } from "../../hooks/useRuleSubmit";
import styles from "./RuleForm.module.css";
import {RuleResultCard} from "../RuleResultCard/RuleResultCard.jsx";

const defaultValues = {
    ruleName: "",
    targetDocs: "",
    guidanceDescription: "",
    refusal: "",
    orgNames: "",
    orgAddresses: "",
    profiles: [{ days: 90, purposes: "", citizenships: "", properties: [] }],
};

export const RuleForm = () => {
    const methods = useForm({
        resolver: zodResolver(ruleSchema),
        defaultValues,
    });
    const {
        register,
        handleSubmit,
        formState: { errors },
        setError,
    } = methods;
    const { submitRule, loading, error: submitError, result } = useRuleSubmit();

    const onSubmit = async (formData) => {
        const requestPayload = transformFormData(formData);
        await submitRule(requestPayload, (serverError) => {
            if (serverError.includes("пустое")) {
                setError("ruleName", { message: serverError });
            } else {
                setError("root.serverError", { message: serverError });
            }
        });
    };

    return (
        <FormProvider {...methods}>
            <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
                {/* Header */}
                <div className={styles.header}>
                    <h1 className={styles.title}>Создание правила</h1>
                    <p className={styles.subtitle}>
                        Заполните форму для создания нового правила проверки
                    </p>
                </div>

                {/* Основная информация */}
                <div className={styles.section}>
                    <h2 className={styles.sectionTitle}>
                        <span
                            className={`${styles.sectionIcon} ${styles.sectionIconPurple}`}
                        >
                            📂
                        </span>
                        Основная информация
                    </h2>

                    <div className={styles.fieldGroup}>
                        <label className={styles.label}>
                            Название правила
                            <span style={{ color: "var(--danger)" }}> *</span>
                        </label>
                        <input
                            className={`${styles.input} ${errors.ruleName ? styles.inputError : ""}`}
                            placeholder="Проверка визовых документов"
                            {...register("ruleName")}
                        />
                        {errors.ruleName && (
                            <span className={styles.error}>
                                {errors.ruleName.message}
                            </span>
                        )}
                    </div>

                    <div className={styles.fieldGroup}>
                        <label className={styles.label}>
                            Целевые документы
                            <span className={styles.labelOptional}>
                                (опционально)
                            </span>
                        </label>
                        <input
                            className={styles.input}
                            placeholder="Разрешение на работу"
                            {...register("targetDocs")}
                        />
                        <span className={styles.hint}>
                            Разделяйте документы точкой с запятой
                        </span>
                    </div>
                </div>

                {/* Руководство */}
                <GuidanceBlock register={register} errors={errors} />

                {/* Организации */}
                {/* Организации — используем .fieldRow для двух колонок */}
                <div className={styles.section}>
                    <div className={styles.sectionTitle}>
                        <div className={styles.sectionIcon}>🏢</div>
                        <span>Организации</span>
                    </div>

                    {/* Именно здесь оборачиваем два поля в .fieldRow */}
                    <div className={styles.fieldRow}>
                        <div className={styles.fieldGroup}>
                            <label className={styles.label}>
                                Названия организаций <span className={styles.labelOptional}>(опционально)</span>
                            </label>
                            <input
                                {...register("orgNames")}
                                className={`${styles.input} ${errors.orgNames ? styles.inputError : ""}`}
                                placeholder="МВД"
                            />
                            <div className={styles.hint}>Разделяйте точкой с запятой</div>
                        </div>

                        <div className={styles.fieldGroup}>
                            <label className={styles.label}>
                                Адреса организаций <span className={styles.labelOptional}>(опционально)</span>
                            </label>
                            <input
                                {...register("orgAddresses")}
                                className={`${styles.input} ${errors.orgAddresses ? styles.inputError : ""}`}
                                placeholder="ул. Ленина 1"
                            />
                            <div className={styles.hint}>Разделяйте точкой с запятой</div>
                        </div>
                    </div>
                </div>

                {/* Профили */}
                <ProfileList
                    control={methods.control}
                    register={register}
                    errors={errors}
                />

                {/* Ошибки */}
                {errors.root?.serverError && (
                    <div className={styles.serverError}>
                        {errors.root.serverError.message}
                    </div>
                )}
                {submitError && (
                    <div className={styles.serverError}>{submitError}</div>
                )}

                {/* Кнопка отправки */}
                <button
                    type="submit"
                    disabled={loading}
                    className={styles.btnPrimary}
                >
                    {loading ? (
                        <>
                            <span className={styles.spinner}></span>
                            Создание...
                        </>
                    ) : (
                        <>Создать правило</>
                    )}
                </button>

                {result && <RuleResultCard rule={result} />}

                <div className={styles.footer}>
                    Футер
                </div>
            </form>
        </FormProvider>
    );
};