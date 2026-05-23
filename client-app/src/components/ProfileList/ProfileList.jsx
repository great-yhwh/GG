import { useFieldArray } from "react-hook-form";
import { ProfileItem } from "./ProfileItem";
import styles from "./ProfileList.module.css";

export const ProfileList = ({ control, register, errors }) => {
    const { fields, append, remove } = useFieldArray({
        control,
        name: "profiles",
    });

    return (
        <div className={styles.profileSection}>
            <div className={styles.header}>
                <h2 className={styles.sectionTitle}>
                    <span className={styles.sectionIcon}>👤</span>
                    Профили
                    <span className={styles.badge}>{fields.length}</span>
                </h2>
            </div>

            {fields.length === 0 && (
                <div className={styles.emptyState}>
                    <span className={styles.emptyIcon}>📭</span>
                    Нет добавленных профилей. Добавьте хотя бы один.
                </div>
            )}

            {fields.map((field, idx) => (
                <ProfileItem
                    key={field.id}
                    index={idx}
                    register={register}
                    control={control}
                    errors={errors}
                    onRemove={() => remove(idx)}
                    total={fields.length}
                />
            ))}

            <button
                type="button"
                className={styles.btnAdd}
                onClick={() =>
                    append({
                        days: 0,
                        purposes: "",
                        citizenships: "",
                        properties: [],
                    })
                }
            >
                ➕ Добавить профиль
            </button>
        </div>
    );
};