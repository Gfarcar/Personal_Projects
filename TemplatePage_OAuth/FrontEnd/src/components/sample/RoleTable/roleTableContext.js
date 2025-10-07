import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de que la ruta sea correcta
import { appsettings } from "src/settings/appsettings";

const roleTableContext = createContext();

export const RoleTableProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para la tabla de roles
    const roleTableStore = createCustomStore(
        `${appsettings.apiUrl}${appsettings.roleTableUrl}`, 
        (values) => ({
            Id: values.id,
            Name: values.name,
            // Agrega más campos según tu estructura de datos
        })
    );

    return (
        <roleTableContext.Provider value={{ store: roleTableStore, refreshKey, triggerRefresh }}>
            {children}
        </roleTableContext.Provider>
    );
};

export const useRoleTable = () => useContext(roleTableContext);
