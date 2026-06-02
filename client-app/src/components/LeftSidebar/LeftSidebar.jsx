import { DndContext, closestCenter, PointerSensor, useSensor, useSensors } from "@dnd-kit/core";
import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import styles from "./LeftSidebar.module.css";

const SortableProfile = ({ profile, index, isActive, onSelect, onRemove }) => {
    const { attributes, listeners, setNodeRef, transform, transition, isDragging } = useSortable({ id: profile.id });
    const style = {
        transform: CSS.Transform.toString(transform),
        transition,
        opacity: isDragging ? 0.5 : 1,
    };
    return (
        <div ref={setNodeRef} style={style} className={`${styles.profileItem} ${isActive ? styles.active : ""}`}>
            <div className={styles.dragHandle} {...attributes} {...listeners}>⋮⋮</div>
            <span className={styles.profileName} onClick={() => onSelect(index)}>Профиль {index + 1}</span>
            <button
                type="button"
                className={styles.removeBtn}
                onClick={(e) => { e.stopPropagation(); onRemove(index); }}
            >
                ✕
            </button>
        </div>
    );
};

export const LeftSidebar = ({ profiles, currentProfileIndex, onSelectProfile, onAddProfile, onRemoveProfile, onMoveProfile }) => {
    const sensors = useSensors(useSensor(PointerSensor));

    const handleDragEnd = (event) => {
        const { active, over } = event;
        if (active.id !== over.id) {
            const oldIndex = profiles.findIndex(p => p.id === active.id);
            const newIndex = profiles.findIndex(p => p.id === over.id);
            onMoveProfile(oldIndex, newIndex);
        }
    };

    return (
        <div className={styles.sidebar}>
            <div className={styles.header}>
                <h3>Содержание</h3>
                <button type="button" onClick={onAddProfile} className={styles.addBtn}>+ Добавить профиль</button>
            </div>
            <div className={styles.scrollArea}>
                <DndContext sensors={sensors} collisionDetection={closestCenter} onDragEnd={handleDragEnd}>
                    {profiles.map((profile, idx) => (
                        <SortableProfile
                            key={profile.id}
                            profile={profile}
                            index={idx}
                            isActive={idx === currentProfileIndex}
                            onSelect={onSelectProfile}
                            onRemove={onRemoveProfile}
                        />
                    ))}
                </DndContext>
            </div>
        </div>
    );
};