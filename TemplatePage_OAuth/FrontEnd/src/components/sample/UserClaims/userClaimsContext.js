import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de que la ruta sea correcta
import { appsettings } from "src/settings/appsettings";

const userClaimsContext = createContext();

export const UserClaimsProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para user claims
    const userClaimsStore = createCustomStore(
        `${appsettings.apiUrl}${appsettings.userClaimsTableUrl}`, 
        (values) => ({
            Id : values.id,
            UserId : values.userId,
            Names: values.names,
            LastName: values.lastName,
            SecondLastName: values.secondLastName,
            ClaimType: values.claimType,
            claimValue: values.claimValue, 
            // Agrega más campos según tu estructura de datos
        })
    );

    return (
        <userClaimsContext.Provider value={{ store: userClaimsStore, refreshKey, triggerRefresh }}>
            {children}
        </userClaimsContext.Provider>
    );
};

export const useUserClaims = () => useContext(userClaimsContext);
