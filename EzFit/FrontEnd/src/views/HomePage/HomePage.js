import React from 'react';
import { Box, Grid } from '@mui/material';
import DashboardCard from 'src/components/shared/DashboardCard';
import Breadcrumb from '../../layouts/full/shared/breadcrumb/Breadcrumb';
import { Typography } from '@mui/material';

const BCrumb = [
  {
    title: 'HomePage',
  }
];

const HomePage = () => {
  return (
    <Box>  
        <Breadcrumb title="HomePage" items={BCrumb}/>
        <DashboardCard>
        <Grid container spacing={3}>
            <Grid item xs={12} lg={15}>
                
                    <Typography variant="h1">Bienvenidos</Typography>
                    
                
            </Grid> 
            <Grid item xs={12} lg={15}>
                <Typography variant="h6">
                    Este proyecto fue realizado por Alvaro Trujillo, Gerardo Orduno y Gerardo Farias como proyecto para Plataformas Heterogeneas
                    y Diseno de Interfaces.
                </Typography>
            </Grid>   
        </Grid>
        </DashboardCard> 
   </Box>

  );
};

export default HomePage;
