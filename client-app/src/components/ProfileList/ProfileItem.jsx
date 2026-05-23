import { PropertyList } from "../PropertyList/PropertyList";
import styles from "./ProfileItem.module.css";

export const ProfileItem = ({
                                index,
                                register,
                                control,
                                errors,
                                onRemove,
                                total,
                            }) => {
    return (
        <div
            className={styles.profileCard}
            style={{ animationDelay: `${index * 0.05}s` }}
        >
            <div className={styles.cardHeader}>
                <h3 className={styles.cardTitle}>
                    <span className={styles.profileNumber}>{index + 1}</span>
                    Профиль {index + 1}
                </h3>
                {total > 1 && (
                    <button
                        type="button"
                        className={styles.btnRemove}
                        onClick={onRemove}
                    >
                        Удалить
                    </button>
                )}
            </div>

            <div className={styles.fieldRow}>
                <div className={styles.fieldGroup}>
                    <label className={styles.label}>Дни</label>
                    <input
                        type="number"
                        className={`${styles.input} ${errors.profiles?.[index]?.days ? styles.inputError : ""}`}
                        placeholder="90"
                        {...register(`profiles.${index}.days`, {
                            valueAsNumber: true,
                        })}
                    />
                    {errors.profiles?.[index]?.days && (
                        <span className={styles.error}>
                            {errors.profiles[index].days.message}
                        </span>
                    )}
                </div>

                <div className={styles.fieldGroup}>
                    <label className={styles.label}>Цели въезда</label>
                    <input
                        className={`${styles.input} ${errors.profiles?.[index]?.purposes ? styles.inputError : ""}`}
                        placeholder="Работа"
                        {...register(`profiles.${index}.purposes`)}
                    />
                    {errors.profiles?.[index]?.purposes && (
                        <span className={styles.error}>
                            {errors.profiles[index].purposes.message}
                        </span>
                    )}
                    <span className={styles.hint}>Через запятую</span>
                </div>

                <div className={styles.fieldGroup}>
                    <label className={styles.label}>Гражданства</label>
                    <input
                        className={`${styles.input} ${errors.profiles?.[index]?.citizenships ? styles.inputError : ""}`}
                        placeholder="Беларусь"
                        {...register(`profiles.${index}.citizenships`)}
                    />
                    {errors.profiles?.[index]?.citizenships && (
                        <span className={styles.error}>
                            {errors.profiles[index].citizenships.message}
                        </span>
                    )}
                    <span className={styles.hint}>Через запятую</span>
                </div>
            </div>

            <PropertyList
                control={control}
                nestIndex={index}
                register={register}
                errors={errors}
            />
        </div>
    );
};