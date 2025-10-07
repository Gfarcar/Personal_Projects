import React from 'react';
import { Box, Grid } from '@mui/material';
import GridMenuPermissions from '../../components/sample/MenuPermissions/gridMenuPermissions';
import { MenuPermissionsProvider } from '../../components/sample/MenuPermissions/menuPermissionsContext';
const BCrumb = [
  {
    to: '/',
    title: 'Home',
  },
  {
    title: 'Menu Permissions',
  },
];

const MenuPermissions = () => {
  return (
    <MenuPermissionsProvider>
        <Box>
            <Grid container spacing={3}>
            <Grid item xs={12} lg={15}>
                <GridMenuPermissions />
            </Grid>
            </Grid>
        </Box>
    </MenuPermissionsProvider>
  );
};

export default MenuPermissions;
