import React, { createContext, useState, useContext, useCallback } from 'react';
import createCustomStore from 'src/components/sample/genericCustomStore'; // Asegúrate de que la ruta sea correcta
import { appsettings } from "src/settings/appsettings";

const menuItemsContext = createContext();

export const MenuItemsProvider = ({ children }) => {
    const [refreshKey, setRefreshKey] = useState(0);

    const triggerRefresh = useCallback(() => {
        setRefreshKey(prev => prev + 1);
    }, []);

    // Crear el store personalizado para horas trabajadas
    const MenuItemsStore = createCustomStore(
        `${appsettings.apiUrl}${appsettings.menuItems}`, 
        (values) => ({
            Title: values.title,
            Href: values.href,
        })
    );

    return (
        <menuItemsContext.Provider value={{ store: MenuItemsStore, refreshKey, triggerRefresh }}>
            {children}
        </menuItemsContext.Provider>
    );
};

export const useMenuItems = () => useContext(menuItemsContext);
