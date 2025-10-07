import React, { useState, useEffect, useContext } from 'react';
import { Box, Avatar, Typography, IconButton, Tooltip, useMediaQuery, CircularProgress } from '@mui/material';
import { useSelector } from 'react-redux';
import img1 from 'src/assets/images/profile/user-1.jpg';
import { IconPower } from '@tabler/icons';
import {useNavigate } from 'react-router-dom';
import useAuth from 'src/guards/authGuard/UseAuth';
import useMounted from 'src/guards/authGuard/UseMounted';
import { doc, onSnapshot } from 'firebase/firestore';
import { Db } from '../../../../../guards/firebase/Firebase';
import AuthContext from '/src/guards/firebase/firebaseContext';



export const Profile = () => {
  const customizer = useSelector((state) => state.customizer);
  const lgUp = useMediaQuery((theme) => theme.breakpoints.up('lg'));
  const hideMenu = lgUp ? customizer.isCollapse && !customizer.isSidebarHover : '';
  const { logout } = useAuth();
  const navigate = useNavigate(); // Inicializa useNavigate
  const mounted = useMounted();
  const { user } = useContext(AuthContext);
  const [userInfo, setUserInfo] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!user) return;

    const userDocRef = doc(Db, 'info', user.id);

    // Real-time listener for document changes
    const unsubscribe = onSnapshot(userDocRef, (docSnap) => {
      if (docSnap.exists()) {
        setUserInfo(docSnap.data());
      } else {
        setUserInfo(null);
      }
      setLoading(false);
    });

    // Cleanup the listener
    return () => unsubscribe();
  }, [user]);

  if (loading) {
    return (
      <Box textAlign="center" mt={4}>
        <CircularProgress />
      </Box>
    );
  }
 
 
 
  const handleClose2 = () => {
    setAnchorEl2(null);
  };

  const handleLogout = async () => {
    try {
      await logout();
      navigate('/');
      if (mounted.current) {
        handleClose2();
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Box
      display={'flex'}
      alignItems="center"
      gap={2}
      sx={{ m: 3, p: 2, bgcolor: `${'secondary.light'}` }}
    >
      {!hideMenu ? (
        <>
          <Avatar alt="Remy Sharp" src={img1} />

          <Box>
            <Typography variant="h6"  color="textPrimary">{userInfo?.name ?? 'Not Assigned' }</Typography>
            <Typography variant="caption" color="textSecondary">User</Typography>
          </Box>
          <Box sx={{ ml: 'auto' }}>
            <Tooltip title="Logout" placement="top">
              <IconButton color="primary" onClick={handleLogout}  aria-label="logout" size="small">
                <IconPower size="20" />
              </IconButton>
            </Tooltip>
          </Box>
        </>
      ) : (
        ''
      )}
    </Box>
  );
};
