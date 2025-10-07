import React, { useState, useEffect, useContext } from 'react';
import { doc, getDoc, onSnapshot } from 'firebase/firestore';
import { Db } from '../../../guards/firebase/Firebase';
import AuthContext from '/src/guards/firebase/firebaseContext';
import { Typography, Box, CircularProgress } from '@mui/material';
import DashboardCard from 'src/components/shared/DashboardCard';

const UserInfo = () => {
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

  return (
    <DashboardCard title="User Info">
      {userInfo ? (
        <Box>
          <Typography variant="h6">Current User Information:</Typography>
          <Typography>Name : {userInfo.name}</Typography>
          <Typography>Last Name: {userInfo.lastName}</Typography>
          <Typography>Weight: {userInfo.weight}</Typography>
          <Typography>Height: {userInfo.height}</Typography>
          <Typography>BMI (IMC): {userInfo.IMC}</Typography>
          
        </Box>
      ) : (
        <Typography variant="h6" color="textSecondary">
          No information created yet.
        </Typography>
      )}
    </DashboardCard>
  );
};

export default UserInfo;
