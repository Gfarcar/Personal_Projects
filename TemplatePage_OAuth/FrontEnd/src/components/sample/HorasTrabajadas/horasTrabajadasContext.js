import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de que la ruta sea correcta
import { appsettings } from "src/settings/appsettings";

const horasTrabajadasContext = createContext();

export const HorasTrabajadasProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para horas trabajadas
    const horasTrabajadasStore = createCustomStore(
        `${appsettings.apiUrl}${appsettings.horasTrabajadasTableUrl}`, 
        (values) => ({
            Names: values.names,
            LastName: values.lastName,
            SecondLastName: values.secondLastName,
            Horas: values.horas,
            Fecha: values.fecha
        })
    );

    return (
        <horasTrabajadasContext.Provider value={{ store: horasTrabajadasStore, refreshKey, triggerRefresh }}>
            {children}
        </horasTrabajadasContext.Provider>
    );
};

export const useHorasTrabajadas = () => useContext(horasTrabajadasContext);
