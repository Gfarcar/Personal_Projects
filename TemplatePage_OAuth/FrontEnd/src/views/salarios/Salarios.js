import React from 'react';
import { Box, Grid } from '@mui/material';
import GridSalarios from '../../components/sample/Salarios/gridSalarios';

import { SalariosTableProvider } from '../../components/sample/Salarios/salariosContext';
import ChartSalarios from '../../components/sample/Salarios/chartSalarios';
import Breadcrumb from 'src/layouts/full/shared/breadcrumb/Breadcrumb';
const BCrumb = [
  {
    to: '/',
    title: 'Home',
  },
  {
    title: 'Salarios',
  },
];

const Salarios = () => {
  return (

      
      <SalariosTableProvider>
        <Breadcrumb title="Salarios" items={BCrumb}/>
          <Box>
              <Grid container spacing={3}>
              <Grid item xs={12} lg={15}>
                  <GridSalarios />
              </Grid>
              <Grid item xs={12} lg={15}>
                  <ChartSalarios />
              </Grid>
              </Grid>
          </Box>
      </SalariosTableProvider>
  );
};

export default Salarios;
