import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from '../genericCustomStore'; // Asegúrate de tener la ruta correcta
import { appsettings } from "src/settings/appsettings";

const nominaContext = createContext();

export const NominaTableProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para la nómina
    const nominaStore = createCustomStore(`${appsettings.apiUrl}${appsettings.nominaTableUrl}`, (values) => ({
        Mes: values.mes,
        Cantidad: values.cantidad,
    }));

    return (
        <nominaContext.Provider value={{ store: nominaStore, refreshKey, triggerRefresh }}>
            {children}
        </nominaContext.Provider>
    );
};

export const useNominaTable = () => useContext(nominaContext);
