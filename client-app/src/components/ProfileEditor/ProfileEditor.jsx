import { useFieldArray } from "react-hook-form";
import styles from "./ProfileEditor.module.css";

export const ProfileEditor = ({ profileIndex, control, register, errors }) => {
    const { fields: propertyFields, append, remove } = useFieldArray({
        control,
        name: `profiles.${profileIndex}.properties`,
    });

    const profileErrors = errors.profiles?.[profileIndex];

    return (
        <div className={styles.profileEditor}>
            <div className={styles.header}>
                <h3 className={styles.title}>Редактирование профиля</h3>
                <span className={styles.badge}>Профиль: {profileIndex + 1}</span>
            </div>

            <div className={styles.fieldRow}>
                <div className={styles.fieldGroup}>
                    <label>Дни *</label>
                    <input
                        type="number"
                        className={`${styles.input} ${profileErrors?.days ? styles.inputError : ""}`}
                        {...register(`profiles.${profileIndex}.days`, { valueAsNumber: true })}
                    />
                    {profileErrors?.days && (
                        <span className={styles.error}>{profileErrors.days.message}</span>
                    )}
                </div>
            </div>

            <div className={styles.fieldRow}>
                <div className={styles.fieldGroup}>
                    <label>Цели въезда</label>
                    <input
                        className={styles.input}
                        placeholder="Работа, Туризм"
                        {...register(`profiles.${profileIndex}.purposes`)}
                    />
                    <span className={styles.hint}>Укажите значения через запятую</span>
                </div>

                <div className={styles.fieldGroup}>
                    <label>Гражданства</label>
                    <input
                        className={styles.input}
                        placeholder="Россия, Беларусь"
                        {...register(`profiles.${profileIndex}.citizenships`)}
                    />
                    <span className={styles.hint}>Укажите значения через запятую</span>
                </div>
            </div>

            <div className={styles.propertiesSection}>
                <div className={styles.sectionHeader}>
                    <div>
                        <h4 className={styles.sectionTitle}>Дополнительная информация</h4>
                        <p className={styles.sectionSubtitle}>
                            Добавьте произвольные пары “свойство — значение”
                        </p>
                    </div>
                    <span className={styles.propertyCount}>{propertyFields.length}</span>
                </div>

                {propertyFields.length === 0 && (
                    <div className={styles.emptyProps}>
                        Нет свойств. Нажмите кнопку ниже, чтобы добавить.
                    </div>
                )}

                {propertyFields.length > 0 && (
                    <div className={styles.propertyList}>
                        {propertyFields.map((field, idx) => {
                            const propertyError = profileErrors?.properties?.[idx];

                            return (
                                <div key={field.id} className={styles.propertyRow}>
                                    <div className={styles.propertyField}>
                                        <input
                                            className={`${styles.input} ${propertyError?.name ? styles.inputError : ""}`}
                                            placeholder="Название свойства"
                                            {...register(`profiles.${profileIndex}.properties.${idx}.name`)}
                                        />
                                        {propertyError?.name && (
                                            <span className={styles.error}>
                                                {propertyError.name.message}
                                            </span>
                                        )}
                                    </div>

                                    <div className={styles.propertyField}>
                                        <input
                                            className={`${styles.input} ${propertyError?.value ? styles.inputError : ""}`}
                                            placeholder="Значение"
                                            {...register(`profiles.${profileIndex}.properties.${idx}.value`)}
                                        />
                                        {propertyError?.value && (
                                            <span className={styles.error}>
                                                {propertyError.value.message}
                                            </span>
                                        )}
                                    </div>

                                    <button
                                        type="button"
                                        className={styles.removeProp}
                                        onClick={() => remove(idx)}
                                    >
                                        Удалить
                                    </button>
                                </div>
                            );
                        })}
                    </div>
                )}

                <button
                    type="button"
                    className={styles.addPropBtn}
                    onClick={() => append({ name: "", value: "" })}
                >
                    + Добавить свойство
                </button>
            </div>
        </div>
    );
};