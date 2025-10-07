import { useContext } from 'react';
import AuthContext from '../firebase/FirebaseContext';

const useAuth = () => useContext(AuthContext);

export default useAuth;
