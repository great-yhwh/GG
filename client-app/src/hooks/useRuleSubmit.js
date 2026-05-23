import { useState } from "react";

export const useRuleSubmit = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [result, setResult] = useState(null);

    const submitRule = async (payload, onServerError) => {
        setLoading(true);
        setError(null);

        try {
            console.log("Отправляем на бэк:", JSON.stringify(payload, null, 2));

            const response = await fetch("/api/rules", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload),
            });

            if (!response.ok) {
                const text = await response.text();
                let errorMsg = `${response.status}: ${text}`;
                if (response.status === 400) errorMsg = text;
                if (response.status === 500) errorMsg = "Ошибка сохранения в БД";
                throw new Error(errorMsg);
            }

            const data = await response.json();
            setResult(data);
        } catch (err) {
            setError(err.message);
            if (onServerError) onServerError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return { submitRule, loading, error, result };
};