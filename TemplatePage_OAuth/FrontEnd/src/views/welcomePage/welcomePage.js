
import Breadcrumb from 'src/layouts/full/shared/breadcrumb/Breadcrumb';
import { Typography } from '@mui/material';
import { Box, Grid } from '@mui/material';
import DashboardCard from 'src/components/shared/DashboardCard';

const BCrumb = [
  {
    title: 'Home',
  },
  {
    title: 'Welcome',
  },
];

const SamplePage = () => {
  return (
    
    <Box>
    <Breadcrumb title="Welcome" items={BCrumb}/>
    <DashboardCard>
    <Grid container spacing={3}>
        <Grid item xs={12} lg={15}>
            
                <Typography variant="h1">Bienvenidos</Typography>
                
           
        </Grid> 
        <Grid item xs={12} lg={15}>
      <Typography variant="h6">
        Este proyecto fue realizado por Fernando Velazquez y Gerardo Farias como proyecto de prácticas profesionales.
      </Typography>
            <Typography variant="h6">
                Se desarrollo una App con funciones de autenticación, CRUD, visualizacion, entre otras. 
            </Typography>
        </Grid>   
    </Grid>
    </DashboardCard>
    </Box>
  );
};

export default SamplePage;
