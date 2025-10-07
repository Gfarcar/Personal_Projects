import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de que la ruta sea correcta
import { appsettings } from "src/settings/appsettings";

const MenuPermissionsContext = createContext();

export const MenuPermissionsProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para horas trabajadas
    const MenuPermissionsStore = createCustomStore(
        `${appsettings.apiUrl}${appsettings.menuPermissions}`, 
        (values) => ({
            RoleId: values.roleId,
            MenuItemId: values.menuItemId,
            CanRender: values.canRender
        })
    );

    return (
        <MenuPermissionsContext.Provider value={{ store: MenuPermissionsStore, refreshKey, triggerRefresh }}>
            {children}
        </MenuPermissionsContext.Provider>
    );
};

export const useMenuPermissions = () => useContext(MenuPermissionsContext);
