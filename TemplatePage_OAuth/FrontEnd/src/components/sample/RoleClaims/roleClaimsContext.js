import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de que la ruta sea correcta
import { appsettings } from "src/settings/appsettings";

const roleClaimsContext = createContext();

export const RoleClaimsProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para role claims
    const roleClaimsStore = createCustomStore(
        `${appsettings.apiUrl}${appsettings.roleClaimsTableUrl}`, 
        (values) => ({
            Id: values.id,
            RoleId: values.roleId,
            ClaimType: values.claimType,
            ClaimValue: values.claimValue,
            // Agrega más campos según tu estructura de datos
        })
    );

    return (
        <roleClaimsContext.Provider value={{ store: roleClaimsStore, refreshKey, triggerRefresh }}>
            {children}
        </roleClaimsContext.Provider>
    );
};

export const useRoleClaims = () => useContext(roleClaimsContext);
