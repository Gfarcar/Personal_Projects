import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de que la ruta sea correcta
import { appsettings } from "src/settings/appsettings";

const userRolesContext = createContext();

export const UserRolesProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para user roles
    const userRolesStore = createCustomStore(
        `${appsettings.apiUrl}${appsettings.userRolesTableUrl}`, 
        (values) => ({
            Id: values.id,
            UserId : values.userId,
            Names: values.names,
            LastName: values.lastName,
            SecondLastName: values.secondLastName,
            RoleId: values.roleId,
            RoleName: values.roleName
            // Agrega más campos según tu estructura de datos
        })
    );

    return (
        <userRolesContext.Provider value={{ store: userRolesStore, refreshKey, triggerRefresh }}>
            {children}
        </userRolesContext.Provider>
    );
};

export const useUserRoles = () => useContext(userRolesContext);
