import React, { createContext, useState, useContext, useCallback } from 'react';
import CustomStore from 'devextreme/data/custom_store';
import axios from 'axios';
import { appsettings } from "src/settings/appsettings";

const StoreContext = createContext();

export const StoreProvider = ({ children }) => {
  const [refreshKey, setRefreshKey] = useState(0);

  const triggerRefresh = useCallback(() => {
    setRefreshKey(prev => prev + 1);
  }, []);

  const store = new CustomStore({
    key: 'id',
    load: () => {
      return axios.get(`${appsettings.apiUrl}${appsettings.usersTableUrl}`)
      .then(response => response.data)
      .catch(error => {
        console.error('Error fetching data:', error); // Mejorar la visibilidad del error
        throw error;
      });
    },

    insert: (values) => {
      const requestData = {
        Names : values.names,
        LastName : values.lastName,
        SecondLastName: values.secondLastName,
        Email: values.email,
        password: "Mexicali#11" // Establecer un valor predeterminado para el password
      };
  
      return axios.post(`${appsettings.apiUrl}${appsettings.usersTableUrl}/create`, requestData, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .then(response => {
          triggerRefresh();
          return response.data;
        })
        .catch(error => {
          console.error('Error inserting data:', error);
          throw error;
        });

    },
    update: (key, values) => {
      return axios.put(`${appsettings.apiUrl}${appsettings.usersTableUrl}/update/${key}`, values, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .then(response => {
          triggerRefresh();
          return response.data;
        })
        .catch(error => {
          console.error("Error during update:", error);
          throw error;
        });
    },
    
    remove: (key) => {
      return axios.delete(`${appsettings.apiUrl}/delete/${key}`)
        .then(response => {
          triggerRefresh();
          return response.data;
        })
        .catch(error => {
          console.error("Error during deletion:", error);
          throw error;
        });
    }
  });

  return (
    <StoreContext.Provider value={{ store, refreshKey }}>
      {children}
    </StoreContext.Provider>
  );
};

export const useStore = () => useContext(StoreContext);