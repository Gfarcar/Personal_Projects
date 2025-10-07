import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de tener la ruta correcta
import { appsettings } from "src/settings/appsettings";

const salariosContext = createContext();

export const SalariosTableProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para la nómina
    const salariosStore = createCustomStore(`${appsettings.apiUrl}${appsettings.salarioTableUrl}`, (values) => ({
        Monto: values.monto,                // Check if 'monto' is correct
        Fecha: values.fecha,                 // Check if 'fecha' is correct
        UserId: values.userId,               // Assuming this is correct
        Names: values.names || '',            // Default to empty string if undefined
        LastName: values.lastName || '',      // Default to empty string if undefined
        SecondLastName: values.secondLastName || '' // Default to empty string if undefined
    }));

    return (
        <salariosContext.Provider value={{ store: salariosStore, refreshKey, triggerRefresh }}>
            {children}
        </salariosContext.Provider>
    );
};

export const useSalarios = () => useContext(salariosContext);
