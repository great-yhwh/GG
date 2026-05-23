import { useState } from "react";

export const useRuleFetch = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [rule, setRule] = useState(null);

    const fetchRule = async (id) => {
        setLoading(true);
        setError(null);
        try {
            const response = await fetch(`/api/rules/${id}`);
            if (response.status === 404) {
                throw new Error("Правило не найдено");
            }
            if (!response.ok) {
                const text = await response.text();
                throw new Error(`${response.status}: ${text}`);
            }
            const data = await response.json();
            setRule(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return { fetchRule, loading, error, rule };
};