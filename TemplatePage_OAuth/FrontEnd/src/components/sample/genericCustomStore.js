import CustomStore from 'devextreme/data/custom_store';
import axios from 'axios';
import React, { createContext, useState, useContext, useCallback } from 'react';

function createCustomStore(baseUrl, insertMapping = null) {
  
  const [refreshKey, setRefreshKey] = useState(0);

  const triggerRefresh = useCallback(() => {
    setRefreshKey(prev => prev + 1);
  }, []);



  return new CustomStore({
    key: 'id',
    load: () => {
      return axios.get(baseUrl)
        .then(response => response.data)
        .catch(error => {
          console.error('Error fetching data:', error);
          throw error;
        });
    },
    update: (key, values) => {
      const updateRequest = JSON.stringify(values).replace(/"/g, '\\"');
      const updateRequestWithQuotes = `"${updateRequest}"`;

      return axios.put(`${baseUrl}/update/${encodeURIComponent(key)}`, updateRequestWithQuotes, {
        headers: { 'Content-Type': 'application/json' }
      })
      .then(response => {
        triggerRefresh();
        return response.data;
      })
        .catch(error => {
          console.error('Error updating data:', error);
          throw error;
        });
    },
    insert: (values) => {
      const requestData = insertMapping ? insertMapping(values) : values;

      return axios.post(`${baseUrl}/create`, requestData, {
        headers: { 'Content-Type': 'application/json' }
      })
      .then(response => {
        triggerRefresh();
        console.log(requestData)
        return response.data;
      })
        .catch(error => {
          console.error('Error inserting data:', error);
          throw error;
        });
    },
    remove: (key) => {
      return axios.delete(`${baseUrl}/delete/${encodeURIComponent(key)}`, {
        headers: { 'Content-Type': 'application/json' }
      })
        .then(response => {
          console.log(`Successfully deleted item with key: ${key}`);
          triggerRefresh();
          return response.data;
        })
        .catch(error => {
          console.error('Error deleting data:', error);
          throw error;
        });
    }
  });
}

export default createCustomStore;
