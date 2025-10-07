import React, { useState, useEffect, useContext } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import {
  Box,
  Menu,
  Avatar,
  Typography,
  Divider,
  Button,
  IconButton,
  CircularProgress
} from '@mui/material';
import * as dropdownData from './data';

import { IconMail } from '@tabler/icons';
import { Stack } from '@mui/system';
import ProfileImg from 'src/assets/images/profile/user-1.jpg';
import useAuth from 'src/guards/authGuard/UseAuth';
import useMounted from 'src/guards/authGuard/UseMounted';
import { doc, getDoc, onSnapshot } from 'firebase/firestore';
import { Db } from '../../../../guards/firebase/Firebase';
import AuthContext from '/src/guards/firebase/firebaseContext';

import DashboardCard from 'src/components/shared/DashboardCard';


const Profile = () => {
//  const dispatch = useDispatch(); // Inicializa useDispatch
  const { logout } = useAuth();
  const navigate = useNavigate(); // Inicializa useNavigate
  const mounted = useMounted();
  const [anchorEl2, setAnchorEl2] = useState(null);
  const { user } = useContext(AuthContext);
  const [userInfo, setUserInfo] = useState(null);
  const [loading, setLoading] = useState(true);

  const handleClick2 = (event) => {
    setAnchorEl2(event.currentTarget);
  };
  const handleClose2 = () => {
    setAnchorEl2(null);
  };

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
    <Box>
      <IconButton
        size="large"
        aria-label="show 11 new notifications"
        color="inherit"
        aria-controls="msgs-menu"
        aria-haspopup="true"
        sx={{
          ...(typeof anchorEl2 === 'object' && {
            color: 'primary.main',
          }),
        }}
        onClick={handleClick2}
      >
        <Avatar
          src={ProfileImg}
          alt={ProfileImg}
          sx={{
            width: 35,
            height: 35,
          }}
        />
      </IconButton>
      {/* ------------------------------------------- */}
      {/* Message Dropdown */}
      {/* ------------------------------------------- */}
      <Menu
        id="msgs-menu"
        anchorEl={anchorEl2}
        keepMounted
        open={Boolean(anchorEl2)}
        onClose={handleClose2}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        sx={{
          '& .MuiMenu-paper': {
            width: '360px',
            p: 4,
          },
        }}
      >
        <Typography variant="h5">User Profile</Typography>
        <Stack direction="row" py={3} spacing={2} alignItems="center">
          <Avatar src={ProfileImg} alt={ProfileImg} sx={{ width: 95, height: 95 }} />
          <Box>
            <Typography variant="subtitle2" color="textPrimary" fontWeight={600}>
              {userInfo?.name ?? 'Not Assigned' }
            </Typography>
            <Typography variant="subtitle2" color="textSecondary">
              User
            </Typography>
            <Typography
              variant="subtitle2"
              color="textSecondary"
              display="flex"
              alignItems="center"
              gap={1}
            >
              <IconMail width={15} height={15} />
              {user.email}
            </Typography>
          </Box>
        </Stack>
        <Divider />
        <Box mt={2}>
          <Button onClick={handleLogout} variant="outlined" color="primary" fullWidth>
            Logout
          </Button>
        </Box>
      </Menu>
    </Box>
  );
};

export default Profile;
