import React from 'react';
import { Box, Grid } from '@mui/material';
import GridUserRoles from '../../components/sample/UserRoles/gridUserRoles';
import { UserRolesProvider } from '../../components/sample/UserRoles/userRolesContext';
const BCrumb = [
  {
    to: '/',
    title: 'Home',
  },
  {
    title: 'UserRoles',
  },
];

const UserRoles = () => {
  return (
    <UserRolesProvider>
        <Box>
            <Grid container spacing={3}>
            <Grid item xs={12} lg={15}>
                <GridUserRoles />
            </Grid>
            </Grid>
        </Box>
    </UserRolesProvider>
  );
};

export default UserRoles;
