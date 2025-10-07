import { useNavigate } from 'react-router-dom';
import useAuth from './UseAuth';
import { useEffect } from 'react';

const AdminGuard = ({ children }) => {
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();
  const role = localStorage.getItem("role");

  useEffect(() => {
    if (isAuthenticated && role != 1) {
      navigate('/auth/ErrorAuth');
    }
  }, [isAuthenticated, navigate]);

  return children;
};

export default AdminGuard;
