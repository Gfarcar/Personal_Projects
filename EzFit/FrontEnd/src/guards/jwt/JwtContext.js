import { createContext, useEffect, useReducer } from 'react';
import { useDispatch } from 'react-redux';
import { setRole } from 'src/store/customizer/CustomizerSlice';
import axios from 'src/utils/axios';
import { isValidToken, setSession, refreshAccessToken } from './Jwt';
import Spinner from "src/views/spinner/Spinner";
import { clearRole } from 'src/store/customizer/CustomizerSlice'; // Importa la acción para limpiar el rol

const initialState = {
  isAuthenticated: false,
  isInitialized: false,
  user: null,
  roles: [],
  claims: [],
};


const handlers = {
  INITIALIZE: (state, action) => {
    const { isAuthenticated, user, roles, claims } = action.payload;

    return {
      ...state,
      isAuthenticated,
      isInitialized: true,
      user,
      roles,
      claims,
    };
  },
  LOGIN: (state, action) => {
    const { user, roles, claims } = action.payload;

    return {
      ...state,
      isAuthenticated: true,
      user,
      roles,
      claims,
    };
  },
  LOGOUT: (state) => ({
    ...state,
    isAuthenticated: false,
    user: null,
    roles: [],
    claims: []

  }),
  REGISTER: (state, action) => {
    const { user, roles, claims } = action.payload;

    return {
      ...state,
      isAuthenticated: true,
      user,
      roles,
      claims,
    };
  },
};

const reducer = (state, action) =>
  handlers[action.type] ? handlers[action.type](state, action) : state;

const AuthContext = createContext({
  ...initialState,
  platform: 'JWT',
  signup: () => Promise.resolve(),
  signin: () => Promise.resolve(),
  logout: () => Promise.resolve(),
});

function AuthProvider({ children }) {
  const [state, dispatch] = useReducer(reducer, initialState);
  const reduxDispatch = useDispatch();

  useEffect(() => {
    const initialize = async () => {
      try {
        const accessToken = window.localStorage.getItem('accessToken');

        if (accessToken && isValidToken(accessToken)) {
          setSession(accessToken);

          const response = await axios.post(
            'http://localhost:5000/users/my-account',
            { RefreshToken: window.localStorage.getItem('refreshToken') },
            {
              headers: {
                'Authorization': `Bearer ${accessToken}`,
                'Content-Type': 'application/json'
              }
            }
          );
          const { user, roles, claims } = response.data;
          reduxDispatch(setRole(roles[0]));
          window.localStorage.setItem('role', roles);

          dispatch({
            type: 'INITIALIZE',
            payload: {
              isAuthenticated: true,
              user,
              roles,
              claims,
            },
          });
        } else {
          dispatch({
            type: 'INITIALIZE',
            payload: {
              isAuthenticated: false,
              user: null,
              roles: [],
              claims: []
            },
          });
        }
      } catch (err) {
        console.error(err);
        dispatch({
          type: 'INITIALIZE',
          payload: {
            isAuthenticated: false,
            user: null,
            roles: [],
            claims: []
          },
        });
      }
    };

    initialize();
  }, [reduxDispatch]);


  const signin = async (email, password) => {
    const response = await axios.post('http://localhost:5000/users/login', {   //Cambiar
      email,
      password,
    });
    const { accessToken, refreshToken} = response.data;
    setSession(accessToken);
    window.localStorage.setItem('refreshToken', refreshToken); // Save refreshToken

    if (accessToken && isValidToken(accessToken)) {
      setSession(accessToken);

      const response = await axios.post(
        'http://localhost:5000/users/my-account',
        { RefreshToken: window.localStorage.getItem('refreshToken') },
        {
          headers: {
            'Authorization': `Bearer ${accessToken}`,
            'Content-Type': 'application/json'
          }
        }
      );
      const { user, roles, claims } = response.data;
      reduxDispatch(setRole(roles[0]));
      console.log(roles);
      window.localStorage.setItem('role', roles);

      dispatch({
        type: 'INITIALIZE',
        payload: {
          isAuthenticated: true,
          user,
          roles,
          claims,
        },
      });
    }
    dispatch({
      type: 'LOGIN',
      payload: {
        accessToken,
        refreshToken
      },
    });
  };

  const signup = async (email, password, nombre) => {
    const response = await axios.post('http://localhost:5000/users/signup', { //Cambair
      email,
      password,
      username: nombre,
    });
    const { accessToken, refreshToken, user, roles, claims } = response.data;


    window.localStorage.setItem('accessToken', accessToken);
    window.localStorage.setItem('refreshToken', refreshToken); // Guarda el refreshToken

    dispatch({
      type: 'REGISTER',
      payload: {
        user,
        roles,
        claims,

      },
    });
  };

  const logout = async () => {
    setSession(null);
    localStorage.removeItem('accessToken'); 
    localStorage.removeItem('role'); 
    localStorage.removeItem('refreshToken'); // Elimina también el refreshToken
  //  reduxDispatch(clearRole()); // Despacha la acción para limpiar el rol en Redux
    dispatch({ type: 'LOGOUT' });
    //setSession(null);
  };

  //ATENDER
  if (!state.isInitialized) {
    return <div>Loading...</div>; // Or your custom loading component
  }

  return (
    <AuthContext.Provider
      value={{
        ...state,
        method: 'jwt',
        signin,
        logout,
        signup,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export { AuthContext, AuthProvider };
