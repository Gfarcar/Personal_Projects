/* eslint-disable react-hooks/rules-of-hooks */
import { decodeToken } from 'react-jwt';

import axios from 'src/utils/axios';
import { useDispatch } from 'react-redux';



import { clearRole } from 'src/store/customizer/CustomizerSlice'; // Importa la acción para limpiar el rol


/*const isValidToken = (accessToken) => {
  if (!accessToken) {
    return false;
  }

  const decoded = decodeToken(accessToken);

  const currentTime = Date.now() / 1000;
  console.log(decoded.exp);
  console.log(currentTime)

  return decoded.exp > currentTime;
};*/

const isValidToken = (accessToken) => {
  if (!accessToken) {
    return false;
  }

  const decoded = decodeToken(accessToken);

  const currentTime = Math.floor(Date.now() / 1000); // Current time in seconds
  console.log(decoded.exp);
  console.log(currentTime);

  if (decoded.exp < currentTime) {
    localStorage.removeItem('accessToken'); 
    localStorage.removeItem('role'); 
    localStorage.removeItem('refreshToken');
  
   // dispatch(clearRole()); // Clean up the role in Redux
    return false;
  }

  return true; // Return true if the token is still valid
};


const setSession = (accessToken) => {
  if (accessToken) {
    localStorage.setItem('accessToken', accessToken);
    axios.defaults.headers.common.Authorization = `Bearer ${accessToken}`;
  } else {
    localStorage.removeItem('accessToken');
    delete axios.defaults.headers.common.Authorization;
  }
};


const refreshAccessToken = async () => {
  const refreshToken = localStorage.getItem("refreshToken"); 
  if(!refreshToken){
    throw new Error("No refresh token available"); 
  }

  try{
    const response = await axios.post("api/auth/refresh", {refreshToken}); 
    const { accessToken } = response.data;
    setSession(accessToken);
    return accessToken;
  }catch(error){
    console.error('Failed to refresh token', error);
    logout(); // O manejar el error como sea necesario
  }
};
const logout = async (dispatch) => {
  setSession(null);
  localStorage.removeItem('accessToken'); 
  localStorage.removeItem('role'); 
  localStorage.removeItem('refreshToken');

  dispatch(clearRole()); // Limpia el rol en Redux
  dispatch({ type: 'LOGOUT' }); // Despacha la acción de logout si la tienes definida en tu slice

  delete axios.defaults.headers.common.Authorization;
};


// Interceptor de Axios para manejar expiración de tokens
axios.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    if (error.response && error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const newAccessToken = await refreshAccessToken();
      if (newAccessToken) {
        axios.defaults.headers.common.Authorization = `Bearer ${newAccessToken}`;
        return axios(originalRequest); // Reintenta la solicitud original
      }
    }
    return Promise.reject(error);
  }
);

export { isValidToken, setSession, logout, refreshAccessToken };
