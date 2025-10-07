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
    }
  });
}

export default createCustomStore;
