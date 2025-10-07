import React from 'react';
import { Box, Grid } from '@mui/material';
import GridMenuItems from '../../components/sample/MenuItems/gridMenuItems';
import { MenuItemsProvider } from '../../components/sample/MenuItems/menuItemsContext';
const BCrumb = [
  {
    to: '/',
    title: 'Home',
  },
  {
    title: 'Menu Items',
  },
];

const MenuItems = () => {
  return (
    <MenuItemsProvider>
        <Box>
            <Grid container spacing={3}>
            <Grid item xs={12} lg={15}>
                <GridMenuItems />
            </Grid>
            </Grid>
        </Box>
    </MenuItemsProvider>
  );
};

export default MenuItems;
