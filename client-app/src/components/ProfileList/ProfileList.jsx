import { useFieldArray } from "react-hook-form";
import { DndContext, closestCenter, PointerSensor, useSensor, useSensors } from "@dnd-kit/core";
import { SortableContext, verticalListSortingStrategy, arrayMove } from "@dnd-kit/sortable";
import { ProfileItem } from "./ProfileItem";
import styles from "./ProfileList.module.css";

export const ProfileList = ({ control, register, errors }) => {
    const { fields, append, remove, move } = useFieldArray({
        control,
        name: "profiles",
    });

    const sensors = useSensors(useSensor(PointerSensor));

    const handleDragEnd = (event) => {
        const { active, over } = event;
        if (active.id !== over.id) {
            const oldIndex = fields.findIndex((f) => f.id === active.id);
            const newIndex = fields.findIndex((f) => f.id === over.id);
            move(oldIndex, newIndex);
        }
    };

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

            <DndContext sensors={sensors} collisionDetection={closestCenter} onDragEnd={handleDragEnd}>
                <SortableContext items={fields.map(f => f.id)} strategy={verticalListSortingStrategy}>
                    {fields.map((field, idx) => (
                        <ProfileItem
                            key={field.id}
                            id={field.id}
                            index={idx}
                            register={register}
                            control={control}
                            errors={errors}
                            onRemove={() => remove(idx)}
                            total={fields.length}
                        />
                    ))}
                </SortableContext>
            </DndContext>

            <button
                type="button"
                className={styles.btnAdd}
                onClick={() =>
                    append({
                        days: 90,
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